using System;
namespace XamarinMoviesApp.Helpers
{
    public static class Settings
    {
        public const string ApiKey = ""; //TODO
        public const string BaseUrl = "https://api.themoviedb.org/3";
        public const string ImageBaseUrl = "https://image.tmdb.org";
		public const string PosterThumbImageMethod = "/t/p/w92";
		public const string BackdropImageMethod = "/t/p/w600";

		//Endpoints 
        public const string UpcomingMoviesEndpoint = "/movie/upcoming";
        public const string SearchMoviesEndpoint = "/search/movie";
        public const string MovieGenresEndpoint = "/genre/movie/list";
    }
}
