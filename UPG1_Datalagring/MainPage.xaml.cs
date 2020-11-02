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
        //StorageFolder storageFolder;
        public MainPage()
        {
            this.InitializeComponent();

            cbFileType.Items.Add("txt");
            cbFileType.Items.Add("xml");
            cbFileType.Items.Add("json");
            cbFileType.Items.Add("csv");
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
            //var test = file.FileType;

            //textblock.Text = test;

            if (file != null){

                if (file.FileType == ".txt")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);

                    try
                    {
                        contentList.Add(new Content($"{text}"));
                    }
                    catch { }
                }
                else if (file.FileType == ".csv")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);

                    try
                    {
                        contentList.Add(new Content($"{text}"));
                    }
                    catch { }
                }

                else if (file.FileType == ".xml")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(text);

                    foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                    {
                        try
                        {
                            foreach (XmlNode child in node.ChildNodes)
                                contentList.Add(new Content(node.InnerText));
                        }
                        catch { }
                    }
                }
                else if (file.FileType == ".json")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    var obj = JsonConvert.DeserializeObject<dynamic>(text);
                    try
                    {
                        contentList.Add(new Content($"My Name is {obj.Name}, I am: {obj.Age} years old and live in {obj.City}"));
                    }
                    catch { }
                }
                else
                {
                    this.textblock.Text = "Operation cancelled.";
                }

            }
        }
        private async void CreateFileBtn_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(tbName.Text, Convert.ToInt32(tbAge.Text), tbCity.Text);

            tbName.Text = "";
            tbAge.Text = "";
            tbCity.Text = "";

            Object selectedItem = cbFileType.SelectedItem;
            string fileType = Convert.ToString(selectedItem);
            string fileName = "";

            switch (fileType)
            {
                case "txt":
                    {
                        fileName = "AlexTxt.txt";
                        //skapa filen
                        await UWPService.CreateTxtFile(fileName);

                        //skriv till filen
                        var txtFilePath = @$"C:\Users\alexa\OneDrive\Dokument\{fileName}";
                        StorageFile file = await StorageFile.GetFileFromPathAsync(txtFilePath);
                        await UWPService.WriteTxtFile(file, person);

                        break;
                    } 
                case "xml":
                    {
                        fileName = "AlexXml.xml";
                        //skapa filen
                        await UWPService.CreateTxtFile(fileName);

                        //skriv till filen
                        var txtFilePath = @$"C:\Users\alexa\OneDrive\Dokument\{fileName}";
                        StorageFile file = await StorageFile.GetFileFromPathAsync(txtFilePath);
                        await UWPService.WriteXmlFile(file, person);

                        break;
                    }    
                case "json":
                    {
                        fileName = "AlexJson.json";
                        //skapa filen
                        await UWPService.CreateTxtFile(fileName);

                        //skriv till filen
                        var txtFilePath = @$"C:\Users\alexa\OneDrive\Dokument\{fileName}";
                        StorageFile file = await StorageFile.GetFileFromPathAsync(txtFilePath);
                        await UWPService.WriteJsonFile(file, person);

                        break;
                    }         
                case "csv":
                    {
                        fileName = "AlexCsv.csv";
                        //skapa filen
                        await UWPService.CreateTxtFile(fileName);

                        //skriv till filen
                        var txtFilePath = @$"C:\Users\alexa\OneDrive\Dokument\{fileName}";
                        StorageFile file = await StorageFile.GetFileFromPathAsync(txtFilePath);
                        await UWPService.WriteCsvFile(file, person);

                        break;
                    }                                
            }

        }
        private void CbFileType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}