using System;
using Xamarin.Forms;

namespace ASampleApp
{
	public class AddDogPhotoURLPage : BaseContentPage<AddDogPhotoURLViewModel>
	{
		Label _firstLabel;
		Entry _firstEntry;
		Entry _secondEntry;
		Entry _photoURLEntry;
		Button _firstButton;

        Image _dogImage;

		public AddDogPhotoURLPage ()
		{
			this.Title = "Add Dog Photo URL Page";

			_firstLabel = new Label ();
			_firstEntry = new Entry () { Placeholder = "Dog Name" };
			_secondEntry = new Entry () { Placeholder = "Fur color" };
			_photoURLEntry = new Entry () { Placeholder = "Dog URL"};
			_firstButton = new Button () { Text = "Button" };
            _dogImage = new Image() { };

			_firstLabel.SetBinding (Label.TextProperty, nameof (MyViewModel.FirstLabel), BindingMode.TwoWay);
			_firstEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.FirstEntryText));
			_secondEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.SecondEntryText));
			_photoURLEntry.SetBinding (Entry.TextProperty, nameof (MyViewModel.PhotoURLEntry));
			_firstButton.SetBinding (Button.CommandProperty, nameof (MyViewModel.MyFavoriteCommand));
            _dogImage.SetBinding(Image.SourceProperty, nameof(MyViewModel.PhotoURLEntry), BindingMode.TwoWay);

			//https://s-media-cache-ak0.pinimg.com/736x/62/22/3c/62223cc60b713821fa04b738f740d328--baby-beagle-beagle-pups.jpg
			
            Content = new StackLayout
            {
                Margin = 20,
                Children =
                {
                    _firstLabel,
                    _firstEntry,
                    _secondEntry,
                    _photoURLEntry,
                    _firstButton,
                    _dogImage
				}
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//_firstButton.Clicked += OnAddDogPhotoButtonClicked;	
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//_firstButton.Clicked -= OnAddDogPhotoButtonClicked;	

		}

		//private void OnAddDogPhotoButtonClicked (object sender, EventArgs e)
		//{
		//	Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new AddPuppyPhotoPage ()));
		//}

		//void OnToDogListClicked (object sender, EventArgs e)
		//{
		//	//OPTION 1
		//	//Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListPage ()));

		//	//OPTION 2
		//	Device.BeginInvokeOnMainThread (() => Navigation.PushAsync (new DogListMVVMPage ()));
		//}





	}
}