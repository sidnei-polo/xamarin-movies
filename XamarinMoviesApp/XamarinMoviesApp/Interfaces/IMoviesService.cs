using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMoviesApp.Helpers;
using XamarinMoviesApp.Models;
using XamarinMoviesApp.ViewModels;

namespace XamarinMoviesApp.Interfaces
{
    public interface IMoviesService
    {
        Task<IList<MovieViewModel>> GetMoviesAsync (int page, string query);
        Task<RequestResult<MoviePage>> SearchMoviesAsync(string query, int page);
        Task<RequestResult<MoviePage>> GetUpcomingMoviesAsync(int page);
        Task<RequestResult<MovieGenres>> GetMovieGenresAsync();
    }
}