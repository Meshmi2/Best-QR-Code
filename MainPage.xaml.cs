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
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace App18
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public object element
        {
            get;
            private set;
        }

        public MainPage()
        {
            this.InitializeComponent();
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
                    //convert bitmap to image
                    var img = new Windows.UI.Xaml.Controls.Image();
                    img.Source = bitmap;
                    img.Width = 200;
                    img.Height = 200;
                    img.Margin = new Thickness(10, 10, 10, 10);
                    // Add image to BitmapCache
                    var bitmapCache = new BitmapCache();
                    img.CacheMode = bitmapCache;
                    
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
        private async void Button_Click_1(object sender, RoutedEventArgs e)

        {
            // Create a new file picker
            FileSavePicker savePicker = new FileSavePicker
            {
            
                           SuggestedStartLocation = PickerLocationId.PicturesLibrary
                       };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("PNG", new List<string>() { ".png" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Image";
            // Prompt the user to select a file
            StorageFile file = await savePicker.PickSaveFileAsync();
            // Verify the user selected a file
            if (file != null)
            {
            
          // Encode the image to the selected file on disk
           using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
          {
          BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
          WriteableBitmap bitmap = imgQRCode.Source as WriteableBitmap;
          Stream pixelStream = bitmap.PixelBuffer.AsStream();
          byte[] pixels = new byte[pixelStream.Length];
          await pixelStream.ReadAsync(pixels, 0, pixels.Length);
          encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96.0, 96.0, pixels);
          await encoder.FlushAsync();                            
                }
       }}
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
        
            // Clear Image
            imgQRCode.Source = null;
            imgQRCode.Visibility = Visibility.Collapsed;
            txtInput.Text = string.Empty;
        }
    }
}
