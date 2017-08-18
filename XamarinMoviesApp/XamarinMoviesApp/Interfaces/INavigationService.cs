using System;
using System.Threading.Tasks;
using XamarinMoviesApp.ViewModels;

namespace XamarinMoviesApp.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToDetail(MovieViewModel movie);
        Task NavigateBack();
        void OpenUri(Uri uri);
    }
}
