using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace ASampleApp.ViewModels.Helper
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        //STRING -> IMAGE SOURCE
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			String myString = (String)value;

            if(string.IsNullOrEmpty(myString))
            {
                //base 64string of 1x1 blank png
                myString = "R0lGODlhAQABAIAAAP///////yH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
				//https://webmasters.stackexchange.com/questions/92445/blank-image-what-to-use-base64-vs-1x1-jpeg-image/92447
                //Or you can simply:  
                return null; 
			}


			if (targetType != typeof(ImageSource))
				throw new Exception("Covert - Base64ToImageSource expected ImageSource targetType.");

			//https://forums.xamarin.com/discussion/23049/how-to-show-images-from-a-list-base64-encoded-string
            Byte[] imageBase64 = System.Convert.FromBase64String(myString);
			var myImageSource = ImageSource.FromStream(() => { return new MemoryStream(imageBase64); });

            return myImageSource;
        }

        //IMAGE SOURCE -> STRING
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
                        throw new NotImplementedException();
   //         var myImageSource = (ImageSource)value;
			//if (targetType != typeof(string))
			//	throw new Exception("Convert back - Base64ToImageSource expected String targetType.");

   //         //            https://stackoverflow.com/questions/17352061/fastest-way-to-convert-image-to-byte-array
   //         var memoryStream = new MemoryStream();

			//var myImage = new Image() { };
            //myImage.Source = myImageSource;





   //         var stream = myImage.GetStream();
			//var bytes = new byte[stream.Length];
			//stream.ReadAsync(bytes, 0, (int)stream.Length);
			//string base64 = System.Convert.ToBase64String(bytes);

            //https://forums.xamarin.com/discussion/84232/convert-image-to-byte-array-and-convert-byte-array-to-image-in-xamarin-forms

			//return string;


            //https://forums.xamarin.com/discussion/94432/image-from-base64-in-forms
		}
    }
}
