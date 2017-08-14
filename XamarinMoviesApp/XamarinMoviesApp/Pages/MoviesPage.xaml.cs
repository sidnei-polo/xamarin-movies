using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinMoviesApp.ViewModels;

namespace XamarinMoviesApp.Pages
{
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            BindingContext = new MoviesViewModel();
            InitializeComponent();

            if (Device.RuntimePlatform == "Android")
            {
                searchBar.HeightRequest = 40;
            }
        }
    }
}
