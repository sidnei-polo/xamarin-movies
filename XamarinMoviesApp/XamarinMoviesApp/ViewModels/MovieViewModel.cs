using System;
namespace XamarinMoviesApp.ViewModels
{
    public class MovieViewModel
    {
        public string Title
        {
            get;
            set;
        }

        public string PosterImage
        {
            get;
            set;
        }

        public string BackdropImage
        {
            get;
            set;
        }

        public string Genres
        {
            get;
            set;
        }

        public string Overview
        {
            get;
            set;
        }

        public DateTime? ReleaseDate
        {
            get;
            set;
        }

        public double VoteAverage
        {
            get;
            set;
        }
    }
}
