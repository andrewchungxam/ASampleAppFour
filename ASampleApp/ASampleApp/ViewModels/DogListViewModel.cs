using System;
using System.Collections.Generic;
using ASampleApp.Models;
namespace ASampleApp
{
	public class DogListViewModel : BaseViewModel
	{

		//public string _dogName;
		//public string _furColor;

		//public string DogName {
		//	get { return _dogName;}
		//	set { SetProperty (ref _dogName, value);}
		//}

		//public string FurColor {
		//	get { return _furColor;}
		//	set { SetProperty (ref _furColor, value);}
		//}

		IList<Dog> _listOfDogs;

		public IList<Dog> ListOfDogs 
		{
			get { return _listOfDogs;}
			set { SetProperty (ref _listOfDogs, value);}
		}



	}
}
