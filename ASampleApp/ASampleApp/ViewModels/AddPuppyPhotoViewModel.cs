using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ASampleApp.CosmosDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ASampleApp
{
	public class AddPuppyPhotoViewModel : BaseViewModel
	{
		string _firstLabel;
		string _firstEntryText;
		string _secondEntryText;
		string _photoURLEntry;
		string _photoSourceEntry;
        MediaFile _file;

        public EventHandler<AlertEventArgs> TakePhotoFailed;
        public class AlertEventArgs : EventArgs{
            public string Title { get; set; }
			public string Message { get; set; }
		}

        public EventHandler<PhotoSavedSuccessAlertEventArgs> TakePhotoSucceeded;
        public class PhotoSavedSuccessAlertEventArgs : EventArgs
        {
            public string Title { get; set; }
            public string Message { get; set; }

        }

		public ICommand MyFavoriteCommand { get; set; }
		public ICommand MySecondFavoriteCommand { get; set; }

		public string FirstLabel {
			get { return _firstLabel; }
			set { SetProperty (ref _firstLabel, value); }
		}

		public string FirstEntryText {
			get { return _firstEntryText; }
			set { SetProperty (ref _firstEntryText, value); }

		}

		public string SecondEntryText {
			get { return _secondEntryText; }
			set { SetProperty (ref _secondEntryText, value); }
		}

		public string PhotoURLEntry {
			get { return _photoURLEntry; }
			set {
				SetProperty (ref _photoURLEntry, value);
				this.PhotoSourceEntry = _photoURLEntry;
			}
		}

		public string PhotoSourceEntry {
			get { return _photoSourceEntry; }
			set { SetProperty (ref _photoSourceEntry, value); }
		}





		public AddPuppyPhotoViewModel ()
		{
			MyFavoriteCommand = new Command (OnMyFavoriteAction);
            MySecondFavoriteCommand = new Command(OnMySecondFavoriteAction);
		}

		void OnMyFavoriteAction ()
		{

            //point 1
			//App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);

            //point 2
            App.DogPhotoRep.AddNewDogPhotoSourcePhoto(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry);
			AddLastDogToCosmosDBAsync();

            string _lastNameString = App.DogPhotoRep.GetLastDogPhoto ().Name;
			string _lastNameStringAdd = System.String.Format ("{0} added to the list!", _lastNameString);
			this.FirstLabel = _lastNameStringAdd;

            //ADD THE LAST DOG TO THE ViewModel
			var tempLastDog = App.DogPhotoRep.GetLastDogPhoto();
            App.MyDogListPhotoPage.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);



			return;
		}

		private async void AddLastDogToCosmosDBAsync()
		{
            var myDog = App.DogPhotoRep.GetLastDogPhoto();
			var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
            await CosmosDB.CosmosDBServicePhoto.PostCosmosDogAsync(myCosmosDog);
		}

        private async void OnMySecondFavoriteAction()
        {
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
                TakePhotoFailed?.Invoke(this, new AlertEventArgs{Title="No Camera",Message="no camera"});

				return;
			}

			_file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{

				PhotoSize = PhotoSize.Small,
				//CustomPhotoSize = 50,
				Directory = "Sample",
				Name = "test.jpg"
			});

			if (_file == null)
				return;
            
            this.PhotoURLEntry = _file.Path;

            TakePhotoSucceeded?.Invoke(this, new PhotoSavedSuccessAlertEventArgs { Title = "File Location", Message = _file.Path });
            //await DisplayAlert("File Location", _file.Path, "OK");

			//_dogImage.Source = ImageSource.FromStream(() =>
			//{
			//    var stream = file.GetStream();
			//    file.Dispose();
			//    return stream;
			//});


			//or:
			//HANDLE VIA BINDING
			//_dogImage.Source = ImageSource.FromFile(_file.Path);
			_file.Dispose();


        }
	}
}
