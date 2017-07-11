using System;
using System.Windows.Input;
using ASampleApp.CosmosDB;
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
		}

		void OnMyFavoriteAction ()
		{

            //point 1
			//App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);

            //point 2
            App.DogRep.AddNewDogPhotoSource(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceEntry);
			AddLastDogToCosmosDBAsync();
			string _lastNameString = App.DogRep.GetLastDog ().Name;
			string _lastNameStringAdd = System.String.Format ("{0} added to the list!", _lastNameString);
			this.FirstLabel = _lastNameStringAdd;

			return;
		}

		private async void AddLastDogToCosmosDBAsync()
		{
			var myDog = App.DogRep.GetLastDog();
			var myCosmosDog = DogConverter.ConvertToCosmosDog(myDog);
			await CosmosDB.CosmosDBService.PostCosmosDogAsync(myCosmosDog);
		}
	}
}
