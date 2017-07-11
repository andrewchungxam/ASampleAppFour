//METHOD 1 - Observable Collection
using System;
using Xamarin.Forms;
namespace ASampleApp
{
	public class DogListMVVMPage : BaseContentPage<DogListMVVMViewModel>
	{
		ListView _dogListView;

		public DogListMVVMPage ()
		{
			_dogListView = new ListView ();


			var myTemplate = new DataTemplate (typeof (TextCell));
			_dogListView.ItemTemplate = myTemplate;
			myTemplate.SetBinding (TextCell.TextProperty, "Name");
			myTemplate.SetBinding (TextCell.DetailProperty, "FurColor");

			_dogListView.SetBinding (ListView.ItemsSourceProperty, nameof(MyViewModel.ObservableCollectionOfDogs));

			Content = new StackLayout () {
				Children = {
					_dogListView
				}
			};
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
