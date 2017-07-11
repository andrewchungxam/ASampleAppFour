using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
//using System.Windows.Input;

using ASampleApp.Data;

namespace ASampleApp
{
	public class FirstViewModel : BaseViewModel
	{

		string _firstLabel;
        string _firstEntryText;
        string _secondEntryText;

        public ICommand MyFavoriteCommand { get; set; }
		public ICommand MySecondFavoriteCommand { get; set; }


		public string FirstLabel
		{
			get { return _firstLabel;}
			set { SetProperty (ref _firstLabel, value);}
		}



		public string FirstEntryText
		{
			get { return _firstEntryText;}
			set { SetProperty (ref _firstEntryText, value);}
		}

		public string SecondEntryText
		{
			get { return _secondEntryText; }
			set { SetProperty(ref _secondEntryText, value); }
		}


		public FirstViewModel ()
		{
             MyFavoriteCommand = new Command(OnMyFavoriteAction);
//           MySecondFavoriteCommand = new Command(async () => await OnMySecondFavoriteCommand());

		}

        void OnMyFavoriteAction()
        {

            //OPTION 1 - RETURN BARLIZO
            //this.FirstLabel = "BARLIZO";
            //return;

            //OPTION 2 - RETURN DATABASEPATH
            //this.FirstLabel = FileAccessHelper.GetLocalFilePath("dogs.db3");
            //return;

            //OPTION 3 - RETURN DOG

            App.DogRep.AddNewDog(this.FirstEntryText, this.SecondEntryText);
            string _lastNameString = App.DogRep.GetLastDog().Name;

            //NOT BOUND1
            //            this.FirstLabel = System.String.Format("{0} added to the list!", _firstNameString);
			//YES BOUND1
            string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
            this.FirstLabel = _lastNameStringAdd;

            return;


        }

//        private async Task OnMySecondFavoriteCommand()
//        {

//            var myDude = new Label();
////            await myDude.Text = "hello";
////            await ;
////
        //    await //need to do something that requires Async; 

        //}



    }
}
