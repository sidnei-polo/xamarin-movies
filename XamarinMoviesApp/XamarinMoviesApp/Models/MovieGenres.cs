using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinMoviesApp.Models
{
    public class MovieGenres
    {
		[JsonProperty("genres")]
        public List<MovieGenre> Genres
		{
			get;
			set;
		}
    }
}
