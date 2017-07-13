using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASampleApp.Data;
using ASampleApp.Models;
using ASampleApp.Pages;
using Xamarin.Forms;

namespace ASampleApp
{
    public class App : Application
    {

        public static DogRepository DogRep { get; private set; }
        public static DogPhotoRepository DogPhotoRep { get; set; }
        public static DogRepositoryBaseSixtyFour DogRepBaseSixtyFour { get; private set; }

		public static DogListMVVMPage MyDogListMVVMPage { get; set; }
        public static DogListPhotoPage MyDogListPhotoPage { get; set; }
        public static DogListPhotoBase64Page MyDogListPhotoBase64Page  { get;set;}

		public App ()
		{

            string dbPath = FileAccessHelper.GetLocalFilePath("dog38.db3");
            //USE THIS FOR LIST AND LIST PHOTO PAGE
            DogRep = new DogRepository(dbPath);

			//USE THIS FOR LIST PHOTO PAGE

			DogPhotoRep = new DogPhotoRepository(dbPath);
            //USE THIS FOR LIST PHOTO BASE SIXTY FOUR PAGE
            DogRepBaseSixtyFour = new DogRepositoryBaseSixtyFour(dbPath);

			var applicationStartPage = new FirstPage ();

			//// The root page of your application
			//var content = new ContentPage {
			//	Title = "ASampleApp",
			//	Content = new StackLayout {
			//		VerticalOptions = LayoutOptions.Center,
			//		Children = {
			//			new Label {
			//				HorizontalTextAlignment = TextAlignment.Center,
			//				Text = "Welcome to Xamarin Forms!"
			//			}
			//		}
			//	}
			//};
            var myNavigationPage = new NavigationPage(applicationStartPage);
            MainPage = myNavigationPage;

            //Initialize Dog Photo View Page

		    MyDogListMVVMPage = new DogListMVVMPage();
            MyDogListPhotoPage = new DogListPhotoPage();
            MyDogListPhotoBase64Page = new DogListPhotoBase64Page();

            //THIS WILL RUN EACH TIME YOU CHANGE THE PATH OF THE DATABASE (ie. creating a new Database)
//            IfDogSQLListEmptyThenFill();

		}

  //      private void IfDogSQLListEmptyThenFill()
  //      {
		//	var list = new List<Dog> { };
		//	list = App.DogRep.GetAllDogs();

		//	if (!list.Any()) //if LIST == EMPTY
		//	{
		//		var myListOfCosmosDogs = Task.Run(async () => await CosmosDB.CosmosDBService.GetAllCosmosDogs()).Result;
		//		foreach (var item in myListOfCosmosDogs)
		//		{
		//			var tempDog = CosmosDB.DogConverter.ConvertToDog(item);
		//			App.DogRep.AddNewDogPhotoSource(tempDog.Name, tempDog.FurColor, tempDog.DogPictureSource);

  //                  //TODO: MW
  //                  App.MyDogListMVVMPage.MyViewModel._observableCollectionOfDogs.Add(tempDog);

  //                     //_observableCollectionOfDogs.Add(item);
		//		}
		//	}
		//}

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
