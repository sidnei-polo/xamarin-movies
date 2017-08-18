using System;
using Newtonsoft.Json;

namespace XamarinMoviesApp.Helpers
{
	public class RequestResult
	{
        public bool IsSuccess 
        { 
            set; 
            get; 
        }

        public ErrorData ErrorData
        {
            get;
            set;
        }
	}

    public class RequestResult<TData> : RequestResult
	{
		public TData Data 
        { 
            set; 
            get; 
        }
	}

	public class ErrorData
	{
        [JsonIgnore]
        public Error Error
        {
            get;
            set;
        }

		[JsonProperty("status_code")]
		public int ErrorCode 
        { 
            set; 
            get; 
        }

		[JsonProperty("status_message")]
		public String ErrorMessage 
        { 
            set; 
            get; 
        }
	}

	public enum Error
	{
		NotConnected,
		Unauthorized,
        Exception,
        BadRequest,
        Fail
	}
}
