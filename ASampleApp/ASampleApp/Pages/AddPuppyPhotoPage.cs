using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace ASampleApp
{
	public class AddPuppyPhotoPage : BaseContentPage<AddPhotoPhotoViewModel>
	{
		Label _firstLabel;
		Entry _firstEntry;
		Entry _secondEntry;
		//Entry _photoURLEntry;
		Button _firstButton;

		Button _takePhoto;
		Image _dogImage;

		MediaFile _file;



		public AddPuppyPhotoPage ()
		{
			this.Title = "Add Dog Photo";

			_firstLabel = new Label ();
			_firstEntry = new Entry () { Placeholder = "Dog Name" };
			_secondEntry = new Entry () { Placeholder = "Fur color" };
			//_photoURLEntry = new Entry () { Placeholder = "Dog URL" };
			_firstButton = new Button () { Text = "Submit" };


			_takePhoto = new Button () { Text = "Take Photo" };
			_dogImage = new Image () { };


			_firstLabel.SetBinding (Label.TextProperty, nameof (MyViewModel.FirstLabel), BindingMode.TwoWay);
			_firstEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.FirstEntryText));
			_secondEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.SecondEntryText));
			//_photoURLEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.PhotoURLEntry));
			_firstButton.SetBinding (Button.CommandProperty, nameof (MyViewModel.MyFavoriteCommand));
			_takePhoto.SetBinding (Button.CommandProperty, nameof (MyViewModel.MySecondFavoriteCommand));


			Content = new StackLayout () {
				Children = {
					_firstLabel,
					_firstEntry,
					_secondEntry,
					_firstButton,

					_takePhoto,
					_dogImage


				}

			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			_takePhoto.Clicked += OnTakePhotoButton_Clicked;


		}

		async void OnTakePhotoButton_Clicked (object sender, EventArgs e)
		{
			//throw new NotImplementedException ();
			await CrossMedia.Current.Initialize ();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
				DisplayAlert ("No Camera", ":( No camera available.", "OK");
				return;
			}

			_file = await CrossMedia.Current.TakePhotoAsync (new Plugin.Media.Abstractions.StoreCameraMediaOptions {

				PhotoSize = PhotoSize.Small,
				//CustomPhotoSize = 50,
				Directory = "Sample",
				Name = "test.jpg"
			});

			if (_file == null)
				return;

			MyViewModel.PhotoURLEntry = _file.Path;
			await DisplayAlert ("File Location", _file.Path, "OK");

			//_dogImage.Source = ImageSource.FromStream(() =>
			//{
			//    var stream = file.GetStream();
			//    file.Dispose();
			//    return stream;
			//});


			//or:
			_dogImage.Source = ImageSource.FromFile (_file.Path);
			_file.Dispose ();
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			_takePhoto.Clicked -= OnTakePhotoButton_Clicked;


		}


	}
}


//using System;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using Plugin.Media;
//using Plugin.Media.Abstractions;

//namespace ASampleApp
//{
//	public class AddPuppyPhotoPage : BaseContentPage<AddPhotoPhotoViewModel>
//	{

//		Button _takePhoto;
//		Image _dogImage;

//		public AddPuppyPhotoPage ()
//		{
//			_takePhoto = new Button () { Text = "Take Photo" };

//			_dogImage = new Image () {


//			};


//			Content = new StackLayout () {
//				Children = {
//					_takePhoto,
//					_dogImage


//				}

//			};



//			_takePhoto.Clicked += async (sender, args) => {
//				await CrossMedia.Current.Initialize ();

//				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
//					DisplayAlert ("No Camera", ":( No camera available.", "OK");
//					return;
//				}

//				var file = await CrossMedia.Current.TakePhotoAsync (new Plugin.Media.Abstractions.StoreCameraMediaOptions {

//					PhotoSize = PhotoSize.Small,
//					//CustomPhotoSize = 50,
//					Directory = "Sample",
//					Name = "test.jpg"
//				});

//				if (file == null)
//					return;

//				await DisplayAlert ("File Location", file.Path, "OK");
//				//_dogImage.Source = ImageSource.FromStream(() =>
//				//{
//				//    var stream = file.GetStream();
//				//    file.Dispose();
//				//    return stream;
//				//});
//				//or:
//				_dogImage.Source = ImageSource.FromFile (file.Path);
//				file.Dispose ();
//			};
//		}
//	}
//}

