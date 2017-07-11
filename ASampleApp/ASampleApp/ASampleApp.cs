using System;
using ASampleApp.Data;
using Xamarin.Forms;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Push;

namespace ASampleApp
{
	public class App : Application
	{

        public static DogRepository DogRep { get; private set; }

		public App ()
		{

            string dbPath = FileAccessHelper.GetLocalFilePath("dog2.db3");
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

			MainPage = new NavigationPage (applicationStartPage);
		}

		protected override void OnStart ()
		{

			//// This should come before MobileCenter.Start() is called
			Push.PushNotificationReceived += (sender, e) =>
			{

				// Add the notification message and title to the message
				var summary = $"Push notification received:" +
                    $"\n\tNotification title: {e.Title}" +
                    $"\n\tMessage: {e.Message}";

				// If there is custom data associated with the notification,
				// print the entries
				if (e.CustomData != null)
				{
					summary += "\n\tCustom data:\n";
					foreach (var key in e.CustomData.Keys)
					{
						summary += $"\t\t{key} : {e.CustomData[key]}\n";
					}
				}

				// Send the notification summary to debug output
				System.Diagnostics.Debug.WriteLine(summary);
			};




			// Handle when your app starts

//			MobileCenter.Start("6c5b3d39-15aa-455b-95d7-c5f65578fb4b", typeof(Push));

			MobileCenter.Start("ios=294968e2-d712-43fc-a8bd-1492ed9ea29c;" +
				   "uwp={Your UWP App secret here};" +
				   "android=6c5b3d39-15aa-455b-95d7-c5f65578fb4b;",
				   typeof(Analytics), typeof(Crashes), typeof(Push));

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
