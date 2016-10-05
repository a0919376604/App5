using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Java.IO;
using System.Threading.Tasks;
using Android.Provider;
using Android.Net;
using App5;
using App5.Droid;
using Xamarin.Forms.Platform.Android;
using System.IO;
using Android.Graphics;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_Droid))]

namespace App5.Droid
{
    
    class Picture_Droid : IPicture
    {
       /* private File CreateAlbum(string dirName)
        {
            File AppDir = new File(
                Android.OS.Environment.DirectoryDcim, dirName);
            if (!AppDir.Exists())
            {
                AppDir.Mkdirs();
            }
            return AppDir;
        }

        /*public void SavePictureToDisk(string filename, byte[] imageData,string dirName)
        {

            var dir = CreateAlbum(dirName);

            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = filename + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string filePath = System.IO.Path.Combine(pictures, name);
            System.Console.WriteLine(filePath);
            try
            {
             //   System.IO.File.WriteAllBytes(filePath, imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Uri.FromFile(new File(filePath)));
                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }

        }*/
        public async void SavePictureToDisk(ImageSource source, string imageName)
        {
            try
            {
                var renderer = new StreamImagesourceHandler();
                var photo = await renderer.LoadImageAsync(source, Forms.Context);
                var documentsDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string pngFilename = System.IO.Path.Combine(documentsDirectory, imageName + ".png");
                using (FileStream fs = new System.IO.FileStream(pngFilename, FileMode.OpenOrCreate))
                {
                    photo.Compress(Bitmap.CompressFormat.Png, 100, fs);
                }
            }
            catch (Exception ex)
            {
                string exMessageString = ex.Message;
            }
        }
    }
}