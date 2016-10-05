using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5
{
    public interface IPicture
    {
        //  void SavePictureToDisk(string filename, byte[] imageData,string dirName);
        void SavePictureToDisk(ImageSource source, string imageName);


    }
}
