using System;
using ASampleApp.Pages;
using Xamarin.Forms;
namespace ASampleApp
{
	public class FirstPage : BaseContentPage<FirstViewModel>
	{
		Label _firstLabel;
		Entry _firstEntry;
        Entry _secondEntry;
		Button _firstButton;
        Button _goToDogListButton;

		Button _addAddDogPhotoButton;
		Button _addAddDogPhotoURLButton;
        Button _addAddDogPhotoBase64Button;
        Button _goToDogPhotoListButton;

        Button _goToDogPhotoBase64ListButton;

		Label _emptyLabel;
        Label _emptyLabel2;

		public FirstPage ()
		{

            this.Title = "A Sample App Four";


			//METHOD#1 non-MVVM
			//BindingContext = new FirstViewModel ();

			_firstLabel = new Label (); //{ Text = "Hello"};
            _firstEntry = new Entry () {Placeholder = "Dog Name"};
            _secondEntry = new Entry() { Placeholder = "Fur color" };
			_firstButton = new Button () { Text = "Submit" };
            _goToDogListButton = new Button () { Text = "Go to Dog List" };
			_emptyLabel = new Label () { Text = " " };
			_emptyLabel2 = new Label() { Text = " " };


			_addAddDogPhotoButton = new Button() { Text = "Add Dog Photo" };
            _addAddDogPhotoURLButton = new Button { Text = "Add Dog Photo URL" };
            _addAddDogPhotoBase64Button = new Button { Text = "Add Dog Photo Base64" };
            _goToDogPhotoListButton = new Button() { Text = "Go to Dog Photo List"};
            _goToDogPhotoBase64ListButton = new Button() { Text = "Go to Dog Photo Base64 List" }; 

			//METHOD#2 MVVM
			//
            _firstLabel.SetBinding (Label.TextProperty, nameof (MyViewModel.FirstLabel), BindingMode.TwoWay);
			_firstEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.FirstEntryText));
			_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));

			_firstButton.SetBinding (Button.CommandProperty, nameof (MyViewModel.MyFavoriteCommand));



            var mainStackLayout = new StackLayout() { 
                Margin = 20,
				Children =
				{
					_firstLabel,
					_firstEntry,
					_secondEntry,
					_firstButton,
					_goToDogListButton,
					_emptyLabel,
					_addAddDogPhotoButton,
					_addAddDogPhotoURLButton,
					_goToDogPhotoListButton,
					_emptyLabel2,
					_addAddDogPhotoBase64Button,
					_goToDogPhotoBase64ListButton


				}
            
            };

			var myScrollView = new ScrollView() { };
            myScrollView.Content = mainStackLayout;

            Content = myScrollView;

   //         Content = new StackLayout 
   //         {
			//	Margin = 20,
			//	Children =
			//	{
			//		_firstLabel,
			//		_firstEntry,
   //                 _secondEntry,
			//		_firstButton,
			//		_goToDogListButton,
			//		_emptyLabel,
   //                 _addAddDogPhotoButton,
   //                 _addAddDogPhotoURLButton,
   //                 _goToDogPhotoListButton,
   //                 _emptyLabel2,
			//		_addAddDogPhotoBase64Button,
   //                 _goToDogPhotoBase64ListButton


			//	}

			//};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//METHOD 1
			//			_firstButton.Clicked += OnFirstButtonClicked;

			_goToDogListButton.Clicked += OnToDogListClicked;
			_addAddDogPhotoButton.Clicked += OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked += OnAddDogPhotoURLButtonClicked;
            _addAddDogPhotoBase64Button.Clicked += OnAddDogPhotoBase64ButtonClicked;
			_goToDogPhotoListButton.Clicked += OnAddDogPhotoListLButtonClicked;

			_goToDogPhotoBase64ListButton.Clicked += OnAddDogPhotoBase64ListButtonClicked;


		}

        protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//METHOD 1

			//			_firstButton.Clicked -= OnFirstButtonClicked;

			_goToDogListButton.Clicked -= OnToDogListClicked;
            _addAddDogPhotoButton.Clicked -= OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked -= OnAddDogPhotoURLButtonClicked;
			_addAddDogPhotoBase64Button.Clicked -= OnAddDogPhotoBase64ButtonClicked;
			_goToDogPhotoListButton.Clicked -= OnAddDogPhotoListLButtonClicked;
			_goToDogPhotoBase64ListButton.Clicked -= OnAddDogPhotoBase64ListButtonClicked;

		}


		private void OnAddDogPhotoButtonClicked(object sender, EventArgs e)
        {
//            throw new NotImplementedException();
            Device.BeginInvokeOnMainThread(()=> Navigation.PushAsync(new AddPuppyPhotoPage()));
        }

		void OnAddDogPhotoURLButtonClicked (object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread (()=>Navigation.PushAsync (new AddDogPhotoURLPage()));	
		}





		//ADD PHOTO
		private void OnAddDogPhotoBase64ButtonClicked(object sender, EventArgs e)
		{
			Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new AddDogPhotoBaseSixtyFourPage()));
		}


		void OnToDogListClicked(object sender, EventArgs e)
		{
			//OPTION 1
			//Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListPage ()));

			//OPTION 2
            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListMVVMPage));
		}



		void OnAddDogPhotoListLButtonClicked(object sender, EventArgs e)
		{
			//Option 1 - creating a new DogListPhotoPage
			//THIS OPTION IS NOT COMPATIBLE WITH //ADD DOG TO OBSERVABLE COLLECTION OF THE LISTVIEW
			//Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new DogListPhotoPage()));

			//TODO - using a static DogListPhotoPage
			//Option 2 - using a static DogListPhotoBase64Page
            Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoPage));

		}

		//LIST
		void OnAddDogPhotoBase64ListButtonClicked(object sender, EventArgs e)
		{
			//Option 1 - creating a new DogListPhotoPage
			//Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(new DogListPhotoBase64Page()));

			//TODO - using a static DogListPhotoBase64Page
			//Option 2 - using a static DogListPhotoBase64Page
			Device.BeginInvokeOnMainThread(() => Navigation.PushAsync(App.MyDogListPhotoBase64Page));

		}






		//EventHandler OnToDogListClicked ()
		//{
		//	//throw new NotImplementedException ();
		//	Device.BeginInvokeOnMainThread (()=> Navigation.PushAsync (new DogListPage()));
		//}

		//void OnFirstButtonClicked (object sender, EventArgs e)
		//{
		//	Console.WriteLine ("Hello there;");
		//	string ft = _firstEntry.Text;

		//	//SIMPLE ACTION 1
		//	//Device.BeginInvokeOnMainThread (() => 
		//	//                                _firstLabel.Text = ft
		//	//                               );

		//	//SIMPLE ACTION 2
		//	//ViewModel.FirstLabel = "hi there!";

		//}
	}
}
