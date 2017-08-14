using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMoviesApp.Interfaces;
using XamarinMoviesApp.Pages;
using XamarinMoviesApp.Services;
using XamarinMoviesApp.ViewModels;

[assembly: Dependency(typeof(NavigationService))]
namespace XamarinMoviesApp.Services
{
    public class NavigationService : INavigationService
    {
        private INavigation _navigation => Application.Current.MainPage.Navigation;

        public NavigationService()
        {
        }

        public async Task NavigateBack()
        {
            await _navigation.PopAsync();
        }

        public async Task NavigateToDetail(MovieViewModel movie)
        {
            await _navigation.PushAsync(new MovieDetailPage() { BindingContext = movie });
        }

        public void OpenUri(Uri uri)
        {
            Device.OpenUri(uri);
        }
    }
}
