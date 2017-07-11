using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASampleApp.Data;
using ASampleApp.Models;
using Xamarin.Forms;

namespace ASampleApp
{
	public class App : Application
	{

        public static DogRepository DogRep { get; private set; }

        public static DogListPhotoPage MyDogListPhotoPage { get; set; }


		public App ()
		{

            string dbPath = FileAccessHelper.GetLocalFilePath("dog18.db3");
            DogRep = new DogRepository(dbPath);

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
            MyDogListPhotoPage = new DogListPhotoPage();

            //THIS WILL RUN EACH TIME YOU CHANGE THE PATH OF THE DATABASE (ie. creating a new Database)
            IfDogSQLListEmptyThenFill();

		}

        private void IfDogSQLListEmptyThenFill()
        {
			var list = new List<Dog> { };
			list = App.DogRep.GetAllDogs();

			if (!list.Any()) //if LIST == EMPTY
			{
				var myListOfCosmosDogs = Task.Run(async () => await CosmosDB.CosmosDBService.GetAllCosmosDogs()).Result;
				foreach (var item in myListOfCosmosDogs)
				{
					var tempDog = CosmosDB.DogConverter.ConvertToDog(item);
					App.DogRep.AddNewDogPhotoSource(tempDog.Name, tempDog.FurColor, tempDog.DogPictureSource);

                    //TODO: MW
                    App.MyDogListPhotoPage.MyViewModel._observableCollectionOfDogs.Add(tempDog);

                       //_observableCollectionOfDogs.Add(item);
				}
			}






		}

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
