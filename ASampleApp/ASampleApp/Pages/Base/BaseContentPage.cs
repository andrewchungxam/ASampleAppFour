using System;
using Xamarin.Forms;
namespace ASampleApp
{
	public abstract class BaseContentPage<T>: ContentPage where T: BaseViewModel, new() 
	{

		T _viewModel;

		public BaseContentPage ()
		{
			BindingContext = MyViewModel;
		}

		//protected T ViewModel => _viewModel ?? (_viewModel = new T ());
		protected T MyViewModel {
			get 
			{
				return _viewModel ?? (_viewModel = new T ());
			}
		}
	}
}


