//METHOD 1 - Observable Collection
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ASampleApp.Models;
using Xamarin.Forms;
namespace ASampleApp
{
    public class DogListPhotoPage : BaseContentPage<DogListPhotoViewModel>
    {
        ListView _dogListView;

        public DogListPhotoPage()
        {


            _dogListView = new ListView();

            //DEFINING THE CELL TYPE
            //OPTION 1 - IMAGE CELL
            //var myTemplate = new DataTemplate (typeof (ImageCell));
            //_dogListView.ItemTemplate = myTemplate;

            //OPTION 2 - DOGPHOTOVIEWCELL
            var myTemplate = new DataTemplate(typeof(DogPhotoViewCell));
            _dogListView.ItemTemplate = myTemplate;

            //SETTING THE BINDINGS OF THE CELL
            //OPTION 1 - IMAGE CELL

            //var model = BindingContext as Dog; 
            //myTemplate.SetBinding (ImageCell.TextProperty, "Name");
            //myTemplate.SetBinding (ImageCell.DetailProperty, "FurColor");

            ////point 1
            ////myTemplate.SetBinding(ImageCell.ImageSourceProperty, "DogPictureURL");//nameof(model.DogPictureSource));
            ////point 2
            //myTemplate.SetBinding(ImageCell.ImageSourceProperty, "DogPictureSource");//nameof(model.DogPictureSource));


            //SETTING THE BINDING OF THE LIST
            _dogListView.SetBinding(ListView.ItemsSourceProperty, nameof(MyViewModel.ObservableCollectionOfDogs));

            //_dogListView.HasUnevenRows = true; //will adjust size of image large or small depending on actual size of image
            _dogListView.RowHeight = 50;


            Content = new StackLayout()
            {
                Children = {
                    _dogListView
                }
            };
        }
    }


    public class DogPhotoViewCell : ViewCell
    {
		MenuItem _deleteAction;

		public DogPhotoViewCell()
        {
            var dogImage = new Image() { };
            var myTextProperty = new Label() { };//Text = "Text" };
            var myDetailProperty = new Label() { }; //Text = "Details" };

            var model = BindingContext as Dog;

            dogImage.SetBinding(Image.SourceProperty, nameof(model.DogPictureSource));
            myTextProperty.SetBinding(Label.TextProperty, nameof(model.Name));
            myDetailProperty.SetBinding(Label.TextProperty, nameof(model.FurColor) );


            var textStack = new StackLayout
            {
				//Padding = 10, //E
				//HorizontalOptions = LayoutOptions.FillAndExpand, //NE
				Margin = new Thickness(0, 3, 0, 0),
				//VerticalOptions = LayoutOptions.FillAndExpand, //NE
				Orientation = StackOrientation.Vertical,
                Children =
                {
                    myTextProperty,
                    myDetailProperty
                }
            };

            var cellLayoutStack = new StackLayout
            {
                Margin = new Thickness(0,0,0,0),
                //Spacing = 10, //default is 6
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    dogImage,
                    textStack
                }

            };

            View = cellLayoutStack;


			//MenuItem ITEM AND CONTEXT ACTIONS

			//var moreAction = new MenuItem { Text = "More" };
			//moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
			//moreAction.Clicked += async (sender, e) =>
			//{
			//    var mi = ((MenuItem)sender);
			//    Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
			//};
			// add to the ViewCell's ContextActions property
			//ContextActions.Add(moreAction);


			_deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            _deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            //_deleteAction.Clicked += async (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
            //};

            ContextActions.Add(_deleteAction);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _deleteAction.Clicked += HandleDeleteActionClicked;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _deleteAction.Clicked -= HandleDeleteActionClicked;
        }

        private void HandleDeleteActionClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            var myMenuItem = (MenuItem)sender;

            var selectedModel = myMenuItem.BindingContext as Dog;

            //DELETE FROM DATABASE - AND BEFORE REFRESH THE DATA SOURCE ON THE UI 
            App.DogRep.DeleteDog(selectedModel);

			//Wait for the iOS animation to finish
			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					Task.Delay(250);
					break;
			}

            var navigationPage = Application.Current.MainPage as NavigationPage;
            var dogListPhotoPage = navigationPage.CurrentPage as DogListPhotoPage;
            var dogListPhotoViewModel = dogListPhotoPage.BindingContext as DogListPhotoViewModel;

            //dogListPhotoViewModel.RefreshAllDataCommand?.Execute(true); //BM
            //BM - refresh all the data

            var myMenuItemCommandParameter = (Dog)((MenuItem)sender).CommandParameter;

            //MAHDI - just remove the specific item and the observable collection + INotifyPropertyChanged will auto-update the UI as necessary
            dogListPhotoViewModel.DeleteDogFromListCommand.Execute(myMenuItemCommandParameter);

			//FormList.Remove(item); //MAHDI

		}
    }

}














//METHOD 2 - IList 
//using System;
//using Xamarin.Forms;
//namespace ASampleApp
//{
//	public class DogListMVVMPage : BaseContentPage<DogListMVVMViewModel>
//	{

//		ListView _dogListView;

//		public DogListMVVMPage ()
//		{
//			_dogListView = new ListView ();


//			var myTemplate = new DataTemplate (typeof (TextCell));
//			_dogListView.ItemTemplate = myTemplate;
//			myTemplate.SetBinding (TextCell.TextProperty, "Name");
//			myTemplate.SetBinding (TextCell.DetailProperty, "FurColor");

//			_dogListView.SetBinding (ListView.ItemsSourceProperty, nameof (MyViewModel.ListOfDogs));

//			Content = new StackLayout () {
//				Children = {
//					_dogListView
//				}
//			};
//		}



//	}
//}















































//using System;
//using Xamarin.Forms;

//namespace ASampleApp
//{
//    public class DogListPhotoPage : BaseContentPage<DogListPhotoViewModel>
//	{
//		Label _firstLabel;
//		Entry _firstEntry;
//		Entry _secondEntry;
//		Button _firstButton;
//		Button _goToDogListButton;

//		Button _addAddDogPhotoURLButton;
//		Button _addAddDogPhotoButton;
//		Button _goToDogPhotoListButton;


//		public DogListPhotoPage ()
//		{

//			this.Title = "Dog List Photo Page";


//			//METHOD#1 non-MVVM
//			//BindingContext = new FirstViewModel();

//			_firstLabel = new Label(); //{ Text = "Hello"};
//			_firstEntry = new Entry() { Placeholder = "Dog Name" };
//			_secondEntry = new Entry() { Placeholder = "Fur color" };
//			_firstButton = new Button() { Text = "Button" };
//			_goToDogListButton = new Button() { Text = "Go to Dog List" };

//			_addAddDogPhotoButton = new Button() { Text = "Add Dog Photo" };
//			_addAddDogPhotoURLButton = new Button { Text = "Add Dog Photo URL" };
//			_goToDogPhotoListButton = new Button() { Text = "Go to Dog Photo List" };


//			//METHOD#2 MVVM
//			//
//			_firstLabel.SetBinding(Label.TextProperty, nameof(MyViewModel.FirstLabel), BindingMode.TwoWay);
//			_firstEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.FirstEntryText));
//			_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));

//			_firstButton.SetBinding(Button.CommandProperty, nameof(MyViewModel.MyFavoriteCommand));


//			Content = new StackLayout
//			{
//				Margin = 20,
//				Children =
//				{
//					_firstLabel,
//					_firstEntry,
//					_secondEntry,
//					_firstButton,
//					_goToDogListButton,
//					_addAddDogPhotoButton,
//					_addAddDogPhotoURLButton,
//					_goToDogPhotoListButton

//				}

//			};

//		}



//		protected override void OnAppearing()
//		{
//			base.OnAppearing();
//			//METHOD 1
//			//          _firstButton.Clicked += OnFirstButtonClicked;

//			_goToDogListButton.Clicked += OnToDogListClicked;

//			//TEST BY ADDING DOGS ON EACH ONAPPEARING
//			//App.DogRep.AddNewDog("Oliver", "Black");
//			//App.DogRep.AddNewDog("Oliver", "Black");


//		}

//		protected override void OnDisappearing()
//		{
//			base.OnDisappearing();
//			//METHOD 1

//			//          _firstButton.Clicked -= OnFirstButtonClicked;

//			_goToDogListButton.Clicked -= OnToDogListClicked;
//		}

//		void OnToDogListClicked(object sender, EventArgs e)
//		{
//			//OPTION 1
//			//Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListPage ()));

//			//OPTION 2
//			Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new DogListMVVMPage()));
//		}



//		//EventHandler OnToDogListClicked ()
//		//{
//		//  //throw new NotImplementedException ();
//		//  Device.BeginInvokeOnMainThread (()=> Navigation.PushAsync (new DogListPage()));
//		//}

//		//void OnFirstButtonClicked (object sender, EventArgs e)
//		//{
//		//  Console.WriteLine ("Hello there;");
//		//  string ft = _firstEntry.Text;

//		//  //SIMPLE ACTION 1
//		//  //Device.BeginInvokeOnMainThread (() => 
//		//  //                                _firstLabel.Text = ft
//		//  //                               );

//		//  //SIMPLE ACTION 2
//		//  //ViewModel.FirstLabel = "hi there!";

//		//}
//	}
//}

