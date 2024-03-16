using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using  Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using Windows.Graphics.Printing;
using Windows.Storage.Pickers;
using Windows.Storage;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using ZXing.QrCode.Internal;
using System.Drawing;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace App18
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }



        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Save image

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            savePicker.FileTypeChoices.Add("Image", new List<string>() { ".png", ".jpg" });
            savePicker.SuggestedFileName = "MyQRCode";


            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Windows.ApplicationModel.DataTransfer.DataTransferManager.ShowShareUI();
        }

        private void GenerateQRCode(object sender, RoutedEventArgs e)
        {
            {
                string text = txtInput.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    BarcodeWriter writer = new BarcodeWriter
                    {
                        Format = BarcodeFormat.QR_CODE,
                        Options = new EncodingOptions { GS1Format = true, Width = 200, Height = 200 }

                    };

                    WriteableBitmap bitmap = writer.Write(text);
                    imgQRCode.Source = bitmap;
                    imgQRCode.Visibility = Visibility.Visible;
                    imgQRCode.Width = 200;
                    imgQRCode.Height = 200;
                }
                else
                {
                    // Show an error message
                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Please enter text to generate QR code",
                        CloseButtonText = "Ok"
                    };
                    dialog.ShowAsync();
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        
            // Clear Image
            imgQRCode.Source = null;
            imgQRCode.Visibility = Visibility.Collapsed;
            txtInput.Text = string.Empty;
        }
    }
}
