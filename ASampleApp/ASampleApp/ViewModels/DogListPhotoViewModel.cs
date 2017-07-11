using System.Collections.Generic;
using ASampleApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;
using System;
using System.Diagnostics;

using ASampleApp.CosmosDB;
using System.Threading.Tasks;
using System.Linq;

namespace ASampleApp
{
	public class DogListPhotoViewModel : BaseViewModel
	{

		public ObservableCollection<Dog> _observableCollectionOfDogs;

        public ICommand DeleteDogFromListCommand { get; set; }



		public DogListPhotoViewModel ()
		{
			//https://stackoverflow.com/questions/5561156/convert-listt-to-observablecollectiont-in-wp7
			var list = new List<Dog> { };
			list = App.DogRep.GetAllDogs ();

			_observableCollectionOfDogs = new ObservableCollection<Dog>();

			if (list.Any()) //if LIST != EMPTY
			{
			    foreach (var item in list)
                {
				    _observableCollectionOfDogs.Add (item);
                }
            } 

			DeleteDogFromListCommand = new Command(DeleteDogFromListAction);

		}

        private async void DeleteDogFromListAction(object obj)
        {
            Debug.WriteLine("DELETE DOG FROM LIST ACTION");

            var myItem = obj as Dog;
            _observableCollectionOfDogs.Remove(myItem);


            var myCosmosDog = DogConverter.ConvertToCosmosDog(myItem);

            await CosmosDBService.DeleteCosmosDogAsync(myCosmosDog);
        }



        public ObservableCollection<Dog> ObservableCollectionOfDogs {
			get { return _observableCollectionOfDogs; }
			set { SetProperty (ref _observableCollectionOfDogs, value); }
		}
	}
}



////METHOD 2 - using a IList - only works if you initalize with what you're looking for -- it doesn't update because IList is not observable
//using System;
//using System.Collections.Generic;
//using ASampleApp.Models;

//namespace ASampleApp
//{
//	public class DogListMVVMViewModel : BaseViewModel
//	{

//		IList<Dog> _listOfDogs;

//		public DogListMVVMViewModel()
//		{
//			//here do something where you initialize the IList to something.
//			_listOfDogs = App.DogRep.GetAllDogs ();

//		}

//		public IList<Dog> ListOfDogs
//		{
//			get { return _listOfDogs;}
//			set { SetProperty (ref _listOfDogs, value);}
//		}
//	}
//}





//using System.Collections.Generic;
//using ASampleApp.Models;
//using System.Collections.ObjectModel;
//using Xamarin.Forms;
//using System.Windows.Input;
//using System;
//using System.Diagnostics;

//using ASampleApp.CosmosDB;
//using System.Threading.Tasks;
//using System.Linq;

//namespace ASampleApp
//{
//	public class DogListPhotoViewModel : BaseViewModel
//	{

//		ObservableCollection<Dog> _observableCollectionOfDogs;

//		public ICommand DeleteDogFromListCommand { get; set; }



//		public DogListPhotoViewModel()
//		{
//			//https://stackoverflow.com/questions/5561156/convert-listt-to-observablecollectiont-in-wp7
//			var list = new List<Dog> { };
//			list = App.DogRep.GetAllDogs();

//			_observableCollectionOfDogs = new ObservableCollection<Dog>();

//			if (list.Any()) //if LIST != EMPTY
//			{
//				foreach (var item in list)
//				{
//					_observableCollectionOfDogs.Add(item);
//				}
//			}
//			else // if LIST == EMPTY
//			{
//				var myListOfCosmosDogs = Task.Run(async () => await CosmosDB.CosmosDBService.GetAllCosmosDogs()).Result;
//				foreach (var item in myListOfCosmosDogs)
//				{
//					var tempDog = CosmosDB.DogConverter.ConvertToDog(item);
//					App.DogRep.AddNewDogPhotoSource(tempDog.Name, tempDog.FurColor, tempDog.DogPictureSource);
//					_observableCollectionOfDogs.Add(tempDog);
//				}
//			}

//			DeleteDogFromListCommand = new Command(DeleteDogFromListAction);

//		}

//		private async void DeleteDogFromListAction(object obj)
//		{
//			Debug.WriteLine("DELETE DOG FROM LIST ACTION");

//			var myItem = obj as Dog;
//			_observableCollectionOfDogs.Remove(myItem);


//			var myCosmosDog = DogConverter.ConvertToCosmosDog(myItem);

//			await CosmosDBService.DeleteCosmosDogAsync(myCosmosDog);
//		}



//		public ObservableCollection<Dog> ObservableCollectionOfDogs
//		{
//			get { return _observableCollectionOfDogs; }
//			set { SetProperty(ref _observableCollectionOfDogs, value); }
//		}
//	}
//}



//////METHOD 2 - using a IList - only works if you initalize with what you're looking for -- it doesn't update because IList is not observable
////using System;
////using System.Collections.Generic;
////using ASampleApp.Models;

////namespace ASampleApp
////{
////  public class DogListMVVMViewModel : BaseViewModel
////  {

////      IList<Dog> _listOfDogs;

////      public DogListMVVMViewModel()
////      {
////          //here do something where you initialize the IList to something.
////          _listOfDogs = App.DogRep.GetAllDogs ();

////      }

////      public IList<Dog> ListOfDogs
////      {
////          get { return _listOfDogs;}
////          set { SetProperty (ref _listOfDogs, value);}
////      }
////  }
////}


























////using System;
////using System.Windows.Input;
////using Xamarin.Forms;

////namespace ASampleApp
////{
////  public class DogListPhotoViewModel : BaseViewModel
////  {

////      string _firstLabel;
////      string _firstEntryText;
////      string _secondEntryText;

////      public ICommand MyFavoriteCommand { get; set; }
////      public ICommand MySecondFavoriteCommand { get; set; }


////      public string FirstLabel
////      {
////          get { return _firstLabel; }
////          set { SetProperty(ref _firstLabel, value); }
////      }



////      public string FirstEntryText
////      {
////          get { return _firstEntryText; }
////          set { SetProperty(ref _firstEntryText, value); }
////      }

////      public string SecondEntryText
////      {
////          get { return _secondEntryText; }
////          set { SetProperty(ref _secondEntryText, value); }
////      }


////      public DogListPhotoViewModel()
////      {
////          MyFavoriteCommand = new Command(OnMyFavoriteAction);
////          //MySecondFavoriteCommand = new Command(async () => await OnMySecondFavoriteCommand());

////      }

////      void OnMyFavoriteAction()
////      {


////          //RETURN DOG
////          App.DogRep.AddNewDog(this.FirstEntryText, this.SecondEntryText);
////          string _lastNameString = App.DogRep.GetLastDog().Name;


////          string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
////          this.FirstLabel = _lastNameStringAdd;

////          return;

////      }


////  }
////}
























//using System;
//using System.Windows.Input;
//using Xamarin.Forms;

//namespace ASampleApp
//{
//	public class DogListPhotoViewModel : BaseViewModel
//	{

//		string _firstLabel;
//		string _firstEntryText;
//		string _secondEntryText;

//		public ICommand MyFavoriteCommand { get; set; }
//		public ICommand MySecondFavoriteCommand { get; set; }


//		public string FirstLabel
//		{
//			get { return _firstLabel; }
//			set { SetProperty(ref _firstLabel, value); }
//		}



//		public string FirstEntryText
//		{
//			get { return _firstEntryText; }
//			set { SetProperty(ref _firstEntryText, value); }
//		}

//		public string SecondEntryText
//		{
//			get { return _secondEntryText; }
//			set { SetProperty(ref _secondEntryText, value); }
//		}


//		public DogListPhotoViewModel()
//		{
//			MyFavoriteCommand = new Command(OnMyFavoriteAction);
//			//MySecondFavoriteCommand = new Command(async () => await OnMySecondFavoriteCommand());

//		}

//		void OnMyFavoriteAction()
//		{


//			//RETURN DOG
//			App.DogRep.AddNewDog(this.FirstEntryText, this.SecondEntryText);
//			string _lastNameString = App.DogRep.GetLastDog().Name;


//			string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//			this.FirstLabel = _lastNameStringAdd;

//			return;

//		}


//	}
//}
