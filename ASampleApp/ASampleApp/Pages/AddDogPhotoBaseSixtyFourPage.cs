using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

using ASampleApp.ViewModels;
using Xamarin.Forms;

using Plugin.Media;
using Plugin.Media.Abstractions;
using ASampleApp.ViewModels.Helper;

namespace ASampleApp.Pages
{
    public class AddDogPhotoBaseSixtyFourPage: BaseContentPage<AddDogPhotoBaseSixtyFourViewModel>
{
		Label _firstLabel;
		Entry _firstEntry;
		Entry _secondEntry;
		//Entry _photoURLEntry;
		Button _firstButton;

		Button _takePhoto;
		Image _dogImage;

        Image _tempDogImage;
		MediaFile _file;



		public AddDogPhotoBaseSixtyFourPage()
		{
			this.Title = "Add Dog Photo";

			_firstLabel = new Label();
			_firstEntry = new Entry() { Placeholder = "Dog Name" };
			_secondEntry = new Entry() { Placeholder = "Fur color" };
			//_photoURLEntry = new Entry () { Placeholder = "Dog URL" };
			_firstButton = new Button() { Text = "Submit" };


			_takePhoto = new Button() { Text = "Take Photo" };
			_dogImage = new Image() { };
            _tempDogImage = new Image(){};

			_firstLabel.SetBinding(Label.TextProperty, nameof(MyViewModel.FirstLabel), BindingMode.TwoWay);
			_firstEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.FirstEntryText));
			_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));
			//_photoURLEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.PhotoURLEntry));
			_firstButton.SetBinding(Button.CommandProperty, nameof(MyViewModel.MyFavoriteCommand));
			_takePhoto.SetBinding(Button.CommandProperty, nameof(MyViewModel.MySecondFavoriteCommand));
//			_dogImage.SetBinding(Image.SourceProperty, nameof(MyViewModel.PhotoURLEntry));
			_dogImage.SetBinding(Image.SourceProperty, nameof(MyViewModel.PhotoSourceBaseSixtyFourEntry), BindingMode.OneWay, new Base64ToImageSourceConverter());

			//DELETE
			//          _tempDogImage.SetBinding(Image.SourceProperty, nameof(MyViewModel.PhotoSourceBaseSixtyFourEntry));

			Content = new StackLayout()
			{
				Children = {
					_firstLabel,
					_firstEntry,
					_secondEntry,
					_firstButton,

					_takePhoto,
					_dogImage,

					// _tempDogImage,

				}

			};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//_takePhoto.Clicked += OnTakePhotoButton_Clicked;
			MyViewModel.TakePhotoFailed += MyViewModel_TakePhotoFailed;
			MyViewModel.TakePhotoSucceeded += MyViewModel_TakePhotoSucceeded;


		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			//_takePhoto.Clicked -= OnTakePhotoButton_Clicked;
			MyViewModel.TakePhotoFailed -= MyViewModel_TakePhotoFailed;
			MyViewModel.TakePhotoSucceeded -= MyViewModel_TakePhotoSucceeded;

		}

		void MyViewModel_TakePhotoFailed(object sender, AddDogPhotoBaseSixtyFourViewModel.AlertEventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>
										   await DisplayAlert(e.Title, e.Message, "OK"));
		}

		private void MyViewModel_TakePhotoSucceeded(object sender, AddDogPhotoBaseSixtyFourViewModel.PhotoSavedSuccessAlertEventArgs e)
		{
			Device.BeginInvokeOnMainThread(async () =>
										   await DisplayAlert(e.Title, e.Message, "OK"));

		}

		async void OnTakePhotoButton_Clicked(object sender, EventArgs e)
		{
			//throw new NotImplementedException ();
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}

			_file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{

				PhotoSize = PhotoSize.Small,
                CompressionQuality = 10,
				//CustomPhotoSize = 50,
				Directory = "Sample",
				Name = "test.jpg"
			});

        
			if (_file == null)
				return;

////file -> Stream -> Byte[] -> Image.Source via Byte[]
////https://forums.xamarin.com/discussion/81344/how-to-convert-image-from-plugin-media-to-base64

			var stream = _file.GetStream();
			var bytes = new byte[stream.Length];
			await stream.ReadAsync(bytes, 0, (int)stream.Length);
			string base64 = System.Convert.ToBase64String(bytes);

            ////https://forums.xamarin.com/discussion/23049/how-to-show-images-from-a-list-base64-encoded-string
			//Byte[] imageBase64 = System.Convert.FromBase64String(base64);
			////			_tempDogImage.Source = ImageSource.FromStream(() => { return new MemoryStream(imageBase64);});
            //_tempDogImage.Source = ImageSource.FromStream(() => { return new MemoryStream(Convert.FromBase64String(base64)); });
            //_tempDogImage.Source = ImageSource.FromFile(_file.Path);


			MyViewModel.PhotoSourceBaseSixtyFourEntry = base64;
            //int oneInt = 1;
            //stream.Dispose();

            //_tempDogImage.Source = base64;
			//MyViewModel.PhotoSourceBaseSixtyFourEntry = base64;
			//var formatString = System.String.Format("{0}", base64);
			//Debug.WriteLine(formatString);

			await DisplayAlert("File Location", _file.Path, "OK");

			//_dogImage.Source = ImageSource.FromStream(() =>
			//{
			//    var stream = file.GetStream();
			//    file.Dispose();
			//    return stream;
			//});

			//or:
			_dogImage.Source = ImageSource.FromFile(_file.Path);
            //_dogImage.Source = ImageSource.FromFile(_file.Path);
            stream.Dispose();
			_file.Dispose();
		}


	}
}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//namespace ASampleApp
//{
//	public class AddPuppyPhotoPage : BaseContentPage<AddPuppyPhotoViewModel>
//	{
//		Label _firstLabel;
//		Entry _firstEntry;
//		Entry _secondEntry;
//		//Entry _photoURLEntry;
//		Button _firstButton;

//		Button _takePhoto;
//		Image _dogImage;

//		MediaFile _file;



//		public AddPuppyPhotoPage()
//		{
//			this.Title = "Add Dog Photo";

//			_firstLabel = new Label();
//			_firstEntry = new Entry() { Placeholder = "Dog Name" };
//			_secondEntry = new Entry() { Placeholder = "Fur color" };
//			//_photoURLEntry = new Entry () { Placeholder = "Dog URL" };
//			_firstButton = new Button() { Text = "Submit" };


//			_takePhoto = new Button() { Text = "Take Photo" };
//			_dogImage = new Image() { };


//			_firstLabel.SetBinding(Label.TextProperty, nameof(MyViewModel.FirstLabel), BindingMode.TwoWay);
//			_firstEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.FirstEntryText));
//			_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));
//			//_photoURLEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.PhotoURLEntry));
//			_firstButton.SetBinding(Button.CommandProperty, nameof(MyViewModel.MyFavoriteCommand));
//			_takePhoto.SetBinding(Button.CommandProperty, nameof(MyViewModel.MySecondFavoriteCommand));


//			Content = new StackLayout()
//			{
//				Children = {
//					_firstLabel,
//					_firstEntry,
//					_secondEntry,
//					_firstButton,

//					_takePhoto,
//					_dogImage


//				}

//			};
//		}

//		protected override void OnAppearing()
//		{
//			base.OnAppearing();
//			_takePhoto.Clicked += OnTakePhotoButton_Clicked;


//		}

//		async void OnTakePhotoButton_Clicked(object sender, EventArgs e)
//		{
//			//throw new NotImplementedException ();
//			await CrossMedia.Current.Initialize();

//			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//			{
//				DisplayAlert("No Camera", ":( No camera available.", "OK");
//				return;
//			}

//			_file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
//			{

//				PhotoSize = PhotoSize.Small,
//				//CustomPhotoSize = 50,
//				Directory = "Sample",
//				Name = "test.jpg"
//			});

//			if (_file == null)
//				return;

//			MyViewModel.PhotoURLEntry = _file.Path;
//			await DisplayAlert("File Location", _file.Path, "OK");

//			//_dogImage.Source = ImageSource.FromStream(() =>
//			//{
//			//    var stream = file.GetStream();
//			//    file.Dispose();
//			//    return stream;
//			//});


//			//or:
//			_dogImage.Source = ImageSource.FromFile(_file.Path);
//			_file.Dispose();
//		}

//		protected override void OnDisappearing()
//		{
//			base.OnDisappearing();
//			_takePhoto.Clicked -= OnTakePhotoButton_Clicked;

//		}
//	}
//}


//using System;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using Plugin.Media;
//using Plugin.Media.Abstractions;

//namespace ASampleApp
//{
//  public class AddPuppyPhotoPage : BaseContentPage<AddPhotoPhotoViewModel>
//  {

//      Button _takePhoto;
//      Image _dogImage;

//      public AddPuppyPhotoPage ()
//      {
//          _takePhoto = new Button () { Text = "Take Photo" };

//          _dogImage = new Image () {


//          };


//          Content = new StackLayout () {
//              Children = {
//                  _takePhoto,
//                  _dogImage


//              }

//          };



//          _takePhoto.Clicked += async (sender, args) => {
//              await CrossMedia.Current.Initialize ();

//              if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
//                  DisplayAlert ("No Camera", ":( No camera available.", "OK");
//                  return;
//              }

//              var file = await CrossMedia.Current.TakePhotoAsync (new Plugin.Media.Abstractions.StoreCameraMediaOptions {

//                  PhotoSize = PhotoSize.Small,
//                  //CustomPhotoSize = 50,
//                  Directory = "Sample",
//                  Name = "test.jpg"
//              });

//              if (file == null)
//                  return;

//              await DisplayAlert ("File Location", file.Path, "OK");
//              //_dogImage.Source = ImageSource.FromStream(() =>
//              //{
//              //    var stream = file.GetStream();
//              //    file.Dispose();
//              //    return stream;
//              //});
//              //or:
//              _dogImage.Source = ImageSource.FromFile (file.Path);
//              file.Dispose ();
//          };
//      }
//  }
//}

