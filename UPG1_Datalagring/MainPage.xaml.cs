using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UPG1_Datalagring.Models;
using UPG1_Datalagring.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UPG1_Datalagring
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StorageFolder storageFolder;
        public MainPage()
        {
            this.InitializeComponent();
        }
         
        public ContentList contentList = new ContentList();
        public async void openFileExplorerBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.FileTypeFilter.Add(".xml");
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".json");
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();

            if (file != null){

                if (file.ContentType == "application/vnd.ms-excel")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);

                    try
                    {
                        contentList.Add(new Content($"Texten i filen är följande: {text}"));
                    }
                    catch { }
                }

                else if (file.ContentType == "text/xml")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(text);

                    foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                    {
                        string textoutput = node.InnerText; //or loop through its children as well
                        try
                        {
                            contentList.Add(new Content($"{textoutput}"));
                        }
                        catch { }
                    }
                }
                else if (file.ContentType == "application/json")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    var obj = JsonConvert.DeserializeObject<dynamic>(text);
                    try
                    {
                        contentList.Add(new Content($"Message: {obj.message}"));
                    }
                    catch { }
                }
                else
                {
                    this.textblock.Text = "Operation cancelled.";
                }

            }
        }

        private void createXmlBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createCsvBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createJsonBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}