using System;
using System.Windows.Input;
using ASampleApp.CosmosDB;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;


namespace ASampleApp.ViewModels
{
    public class AddDogPhotoBaseSixtyFourViewModel : BaseViewModel
    {
        string _firstLabel;
        string _firstEntryText;
        string _secondEntryText;
        string _photoURLEntry;
        string _photoSourceEntry;
		MediaFile _file;

		string _photoSourceBaseSixtyFourEntry;

		public EventHandler<AlertEventArgs> TakePhotoFailed;
		public class AlertEventArgs : EventArgs
		{
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

        public string FirstLabel
        {
            get { return _firstLabel; }
            set { SetProperty(ref _firstLabel, value); }
        }

        public string FirstEntryText
        {
            get { return _firstEntryText; }
            set { SetProperty(ref _firstEntryText, value); }

        }

        public string SecondEntryText
        {
            get { return _secondEntryText; }
            set { SetProperty(ref _secondEntryText, value); }
        }

        public string PhotoURLEntry
        {
            get { return _photoURLEntry; }
            set
            {
                SetProperty(ref _photoURLEntry, value);
                this.PhotoSourceEntry = _photoURLEntry;
            }
        }

        public string PhotoSourceEntry
        {
            get { return _photoSourceEntry; }
            set { SetProperty(ref _photoSourceEntry, value); }
        }

		public string PhotoSourceBaseSixtyFourEntry
		{
			get { return _photoSourceBaseSixtyFourEntry; }
			set { SetProperty(ref _photoSourceBaseSixtyFourEntry, value); 
            
            }
		}


        public AddDogPhotoBaseSixtyFourViewModel()
        {
            MyFavoriteCommand = new Command(OnMyFavoriteAction);
			MySecondFavoriteCommand = new Command(OnMySecondFavoriteAction);

		}

        void OnMyFavoriteAction()
        {

            //point 1
            //App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);

            //point 2
            App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);


            string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
            string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
            this.FirstLabel = _lastNameStringAdd;

			//ADD THE LAST DOG TO THE ViewModel
            var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
            App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);

			return;
        }


		private async void OnMySecondFavoriteAction(object obj)
		{
			await CrossMedia.Current.Initialize();

			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				TakePhotoFailed?.Invoke(this, new AlertEventArgs { Title = "No Camera", Message = "no camera" });

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


			var stream = _file.GetStream();
			var bytes = new byte[stream.Length];
			await stream.ReadAsync(bytes, 0, (int)stream.Length);
			string base64 = System.Convert.ToBase64String(bytes);

			this.PhotoSourceBaseSixtyFourEntry = base64;

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
			
            this.PhotoURLEntry = _file.Path;

            //_dogImage.Source = ImageSource.FromFile(_file.Path);

            stream.Dispose();
			_file.Dispose();




		}


    }
}






////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


//using System;
//using System.Windows.Input;
//using ASampleApp.CosmosDB;
//using Xamarin.Forms;

//namespace ASampleApp
//{
//public class AddPuppyPhotoViewModel : BaseViewModel
//{
//string _firstLabel;
//string _firstEntryText;
//string _secondEntryText;
//string _photoURLEntry;
//string _photoSourceEntry;

//public ICommand MyFavoriteCommand { get; set; }
//public ICommand MySecondFavoriteCommand { get; set; }

//public string FirstLabel
//{
//	get { return _firstLabel; }
//	set { SetProperty(ref _firstLabel, value); }
//}

//public string FirstEntryText
//{
//	get { return _firstEntryText; }
//	set { SetProperty(ref _firstEntryText, value); }

//}

//public string SecondEntryText
//{
//	get { return _secondEntryText; }
//	set { SetProperty(ref _secondEntryText, value); }
//}

//public string PhotoURLEntry
//{
//	get { return _photoURLEntry; }
//	set
//	{
//		SetProperty(ref _photoURLEntry, value);
//		this.PhotoSourceEntry = _photoURLEntry;
//	}
//}

//public string PhotoSourceEntry
//{
//	get { return _photoSourceEntry; }
//	set { SetProperty(ref _photoSourceEntry, value); }
//}

//public AddPuppyPhotoViewModel()
//{
//	MyFavoriteCommand = new Command(OnMyFavoriteAction);
//}

//void OnMyFavoriteAction()
//{

//	//point 1
//	//App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);

//	//point 2
//	App.DogRep.AddNewDogPhotoSource(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry);
//	AddLastDogToCosmosDBAsync();
//	string _lastNameString = App.DogRep.GetLastDog().Name;
//	string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//	this.FirstLabel = _lastNameStringAdd;

//	return;
//}

//private async void AddLastDogToCosmosDBAsync()
//{
//	var myDog = App.DogRep.GetLastDog();
//	var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
//	await CosmosDB.CosmosDBService.PostCosmosDogAsync(myCosmosDog);
//}
//	}
//}

