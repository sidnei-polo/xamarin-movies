using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinMoviesApp.Helpers;
using XamarinMoviesApp.Interfaces;
using XamarinMoviesApp.Models;
using XamarinMoviesApp.Services;
using XamarinMoviesApp.ViewModels;

[assembly: Dependency(typeof(MoviesService))]
namespace XamarinMoviesApp.Services
{
    public class MoviesService : IMoviesService
    {
        readonly IHttpService _httpService;
        readonly IUserDialogsService _userDialogsService;

        private IEnumerable<MovieGenre> _movieGenres;

        public MoviesService()
        {
            _httpService = DependencyService.Get<IHttpService>();
            _userDialogsService = DependencyService.Get<IUserDialogsService>();
        }

        public async Task<RequestResult<MoviePage>> GetUpcomingMoviesAsync (int page)
        {
            var parameters = new Dictionary<string, object>
            {
                { "api_key", Settings.ApiKey },
                { "page", page }
            };

            return await _httpService.Get<MoviePage>(Settings.UpcomingMoviesEndpoint, parameters);
        }

        public async Task<RequestResult<MoviePage>> SearchMoviesAsync (string query, int page)
        {
			var parameters = new Dictionary<string, object>
			{
                { "api_key", Settings.ApiKey },
                { "query", query },
				{ "page", page }
			};

            return await _httpService.Get<MoviePage>(Settings.SearchMoviesEndpoint, parameters);
        }

        public async Task<RequestResult<MovieGenres>> GetMovieGenresAsync () {
			var parameters = new Dictionary<string, object>
			{
                { "api_key", Settings.ApiKey }
            };
            return await _httpService.Get<MovieGenres>(Settings.MovieGenresEndpoint, parameters);
        }

        public async Task<IList<MovieViewModel>> GetMoviesAsync(int page, string query)
        {
            if (_movieGenres == null) {
                var movieGenresResult = await GetMovieGenresAsync();
                if (!movieGenresResult.IsSuccess) {
	                if (movieGenresResult.ErrorData.Error == Error.NotConnected){
	                    _userDialogsService.DisplayAlert(Messages.ErrorLoadingMoviesTitle, movieGenresResult.ErrorData.ErrorMessage);
	                }
                    return null;
                }
                _movieGenres = movieGenresResult.Data.Genres;
            }

            var moviesResult = string.IsNullOrEmpty(query) ? await GetUpcomingMoviesAsync(page) : await SearchMoviesAsync(query, page);

			if (!moviesResult.IsSuccess)
			{
				if (moviesResult.ErrorData.Error == Error.NotConnected)
				{
                    _userDialogsService.DisplayAlert(Messages.ErrorLoadingMoviesTitle, moviesResult.ErrorData.ErrorMessage);
				}
				return null;
			}

            return ConvertToMovieViewModel(moviesResult.Data, _movieGenres).ToList();
        }

        private IEnumerable<MovieViewModel> ConvertToMovieViewModel(MoviePage moviePage, IEnumerable<MovieGenre> movieGenres)
        {
            var movieViewModels = moviePage.Results.Select(e => new MovieViewModel {
                Title = e.Title,
                PosterImage = GetPosterImagePath(e.PosterPath, true),
                BackdropImage = GetPosterImagePath(e.BackdropPath),
                ReleaseDate = e.ReleaseDate,
                Overview = e.Overview,
                VoteAverage = e.VoteAverage,
                Genres = ConvertGenresToString(e.GenreIds, movieGenres)
            });

            return movieViewModels;
        }

        private string ConvertGenresToString(IEnumerable<int> genreIds, IEnumerable<MovieGenre> movieGenres)
        {
            var genreStrings = movieGenres.Where(e => genreIds.Contains(e.Id)).Select(e => e.Name);
            return string.Join("/", genreStrings);
        }

		private string GetPosterImagePath(string imagePath, bool isPoster = false)
		{
            return string.Concat(Settings.ImageBaseUrl, isPoster ? Settings.PosterThumbImageMethod : Settings.BackdropImageMethod, imagePath);
		}
    }
}
