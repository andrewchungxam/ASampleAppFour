using System;
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
        Button _goToDogPhotoListButton;

		Label _emptyLabel;

		public FirstPage ()
		{

            this.Title = "A Sample App";


			//METHOD#1 non-MVVM
			//BindingContext = new FirstViewModel ();

			_firstLabel = new Label (); //{ Text = "Hello"};
            _firstEntry = new Entry () {Placeholder = "Dog Name"};
            _secondEntry = new Entry() { Placeholder = "Fur color" };
			_firstButton = new Button () { Text = "Button" };
			_goToDogListButton = new Button () { Text = "Go to Dog List" };
			_emptyLabel = new Label () { Text = " " };

			_addAddDogPhotoButton = new Button() { Text = "Add Dog Photo" };
            _addAddDogPhotoURLButton = new Button { Text = "Add Dog Photo URL" };
            _goToDogPhotoListButton = new Button() { Text = "Go to Dog Photo List"}; 


			//METHOD#2 MVVM
			//
            _firstLabel.SetBinding (Label.TextProperty, nameof (MyViewModel.FirstLabel), BindingMode.TwoWay);
			_firstEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.FirstEntryText));
			_secondEntry.SetBinding(Entry.TextProperty, nameof(MyViewModel.SecondEntryText));

			_firstButton.SetBinding (Button.CommandProperty, nameof (MyViewModel.MyFavoriteCommand));


			Content = new StackLayout {
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
                    _goToDogPhotoListButton

				}

			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//METHOD 1
			//			_firstButton.Clicked += OnFirstButtonClicked;

			_goToDogListButton.Clicked += OnToDogListClicked;
			_addAddDogPhotoButton.Clicked += OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked += OnAddDogPhotoURLButtonClicked;

			_goToDogPhotoListButton.Clicked += OnAddDogPhotoListLButtonClicked;




			//TEST BY ADDING DOGS ON EACH ONAPPEARING
			//App.DogRep.AddNewDog("Oliver", "Black");
			//App.DogRep.AddNewDog("Oliver", "Black");


		}



		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//METHOD 1

			//			_firstButton.Clicked -= OnFirstButtonClicked;

			_goToDogListButton.Clicked -= OnToDogListClicked;
            _addAddDogPhotoButton.Clicked -= OnAddDogPhotoButtonClicked;
			_addAddDogPhotoURLButton.Clicked -= OnAddDogPhotoURLButtonClicked;

			_goToDogPhotoListButton.Clicked -= OnAddDogPhotoListLButtonClicked;

		}

		void OnAddDogPhotoListLButtonClicked (object sender, EventArgs e)
		{
			//throw new NotImplementedException ();

			Device.BeginInvokeOnMainThread (()=> Navigation.PushAsync (new DogListPhotoPage()));
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

        void OnToDogListClicked (object sender, EventArgs e)
		{
			//OPTION 1
			//Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListPage ()));

			//OPTION 2
			Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListMVVMPage ()));
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
