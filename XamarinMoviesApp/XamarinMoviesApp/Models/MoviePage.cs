using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XamarinMoviesApp.Models
{
    public class MoviePage
    {
		[JsonProperty("page")]
		public int Page
        { 
            get; 
            set; 
        }

		[JsonProperty("results")]
        public List<MovieItem> Results 
        { 
            get; 
            set; 
        }

		[JsonProperty("total_results")]
		public int TotalResults 
        { 
            get; 
            set; 
        }

		[JsonProperty("total_pages")]
		public int TotalPages 
        { 
            get; 
            set; 
        }
    }
}
