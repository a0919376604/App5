using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App5.Droid
{
    class DroidShowMessage:IShowMessage
    {
        private static Activity _activity = null;
        public static void initial(Activity activity)
        {
            _activity = activity;
        }
        public void show(string str)
        {  
             Toast.MakeText(_activity,str, ToastLength.Long).Show();
            
        }

    }
}