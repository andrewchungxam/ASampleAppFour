using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ASampleApp
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public BaseViewModel ()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;


		protected void SetProperty<T> (ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals (backingStore, value))
				return;
			backingStore = value;
			onChanged?.Invoke ();
			OnPropertyChanged (propertyName);
		}

		public void OnPropertyChanged (string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

	}
}
