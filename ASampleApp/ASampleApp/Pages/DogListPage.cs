using System;
using System.Collections.Generic;
using ASampleApp.Models;
using Xamarin.Forms;
namespace ASampleApp
{
	public class DogListPage : BaseContentPage<DogListViewModel> //: BaseContentPage<DogListViewModel>
	{

		ListView _dogListView;
		//List<Dog> _dogList;

		public DogListPage ()
		{
			_dogListView = new ListView ();

			var myTemplate = new DataTemplate (typeof (TextCell));
			_dogListView.ItemTemplate = myTemplate;

			var _myListOfDogs = App.DogRep.GetAllDogs ();
			_dogListView.ItemsSource = _myListOfDogs;


			//_dogListView.SetBinding (ListView.ItemsSourceProperty, nameof(ViewModel.ListOfDogs));


			myTemplate.SetBinding (TextCell.TextProperty, "Name");
			myTemplate.SetBinding (TextCell.DetailProperty, "FurColor");

			Content = new StackLayout () {
				Children = {
					_dogListView
				}
			
			};
		
		
		}
	}
}
