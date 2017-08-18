using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMoviesApp.Interfaces;
using XamarinMoviesApp.Services;

[assembly: Dependency(typeof(UserDialogsService))]
namespace XamarinMoviesApp.Services
{
    public class UserDialogsService : IUserDialogsService
    {
        private Page _mainPage => Application.Current.MainPage;

        public UserDialogsService()
        {
        }

        public async Task DisplayAlert(string title, string message)
        {
            await _mainPage.DisplayAlert(title, message, "OK");
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await _mainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
