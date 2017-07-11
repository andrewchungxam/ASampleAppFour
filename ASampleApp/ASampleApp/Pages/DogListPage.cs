//
// PLEASE USE DOGLISTMVVMPAGE INSTEAD
//
//
//
//
//
//



using System;
using System.Collections.Generic;
using System.Diagnostics;
using ASampleApp.Models;
using Xamarin.Forms;
namespace ASampleApp
{
    public class DogListPage : BaseContentPage<DogListViewModel> //: BaseContentPage<DogListViewModel>
    {

        ListView _dogListView;

        public DogListPage()
        {
            _dogListView = new ListView();

            //DEFINING THE CELL TYPE
            //OPTION 1 - TEXTCELL
            //var myTemplate = new DataTemplate(typeof(TextCell));
            //_dogListView.ItemTemplate = myTemplate;

            //OPTION 2 - DOGCELL
            var myTemplate = new DataTemplate(typeof(DogViewCell));
            _dogListView.ItemTemplate = myTemplate;

            //DATASOURCE
            var _myListOfDogs = App.DogRep.GetAllDogs();
            _dogListView.ItemsSource = _myListOfDogs;

            //BINDINGS IN THE CELLS
            //OPTION 1 - TEXTCELL
            //myTemplate.SetBinding(TextCell.TextProperty, "Name");
            //myTemplate.SetBinding(TextCell.DetailProperty, "FurColor");

            //OPTION 2 - DOGCELL
            //SET THESE BINDINGS WITHIN THE VIEW CELL ITSELF


            Content = new StackLayout()
            {
                Children = {
                    _dogListView
                }

            };


        }
    }

//    public class DogViewCell : ViewCell
//    {
//        public DogViewCell()
//        {
////            var dogImage = new Image() { };
 //           var myTextProperty = new Label() { Text = "Text" };
 //           var myDetailProperty = new Label() { Text = "Details" };

 //           var textStack = new StackLayout
 //           {
 //               HorizontalOptions = LayoutOptions.FillAndExpand,
 //               Orientation = StackOrientation.Vertical,
 //               Children =
 //               {
 //                   myTextProperty,
 //                   myDetailProperty
 //               }
 //           };

 //           var cellLayoutStack = new StackLayout
 //           {
 //               Orientation = StackOrientation.Horizontal,
 //               Children =
 //               {
 ////                   dogImage,
    //                textStack
    //            }

    //        };



    //        //View = cellLayoutStack;
    //        View = textStack;

    //        var moreAction = new MenuItem { Text = "More" };
    //        moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
    //        moreAction.Clicked += async (sender, e) =>
    //        {
    //            var mi = ((MenuItem)sender);
    //            Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
    //        };

    //        var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
    //        deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
    //        deleteAction.Clicked += async (sender, e) =>
    //        {
    //            var mi = ((MenuItem)sender);
    //            Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
    //        };
    //        // add to the ViewCell's ContextActions property
    //        ContextActions.Add(moreAction);
    //        ContextActions.Add(deleteAction);
    //    }
    //}






}
