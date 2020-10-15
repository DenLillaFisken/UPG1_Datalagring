using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UPG1_Datalagring.Models;
using UPG1_Datalagring.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        //StorageFile storageFile;

        public MainPage()
        {
            this.InitializeComponent();

            OpenFilePickerAsync().GetAwaiter();


            //CreateFileAsync().GetAwaiter();
            //WriteToFileAsync().GetAwaiter();
        }
        public ContentList contentList = new ContentList();

        private async void openFileExplorerBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".xml");
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".json");
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                // Application now has read/write access to the picked file
                this.textblock.Text = "Picked file: " + file.ContentType;

                if (file.ContentType == "application/vnd.ms-excel")
                {
                    //ReadFromFile(file);
                    this.textblock.Text = file.DisplayName;

                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    this.textblock.Text = text;

                    try
                    {
                        contentList.Add(new Content($"Texten i filen är följande: {text}"));
                    }
                    catch { }
                }
                //else if (file.ContentType == "text/xml")
                //{
                //    this.textblock.Text = "Detta är en xml fil.";
                //}
                //else if (file.ContentType == "application/json")
                //{
                //    this.textblock.Text = "Detta är en json fil.";
                //}

                //else
                //{
                //    this.textblock.Text = "Något gick fel";
                //}
            }
            else
            {
                this.textblock.Text = "Operation cancelled.";
            }


        }

        private async Task OpenFilePickerAsync()
        {
            
            

        }
        public void ReadFromFile(StorageFile file, char delimiter = ';')
        {


        }

    }
}




