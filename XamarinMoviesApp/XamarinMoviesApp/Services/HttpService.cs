using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using XamarinMoviesApp.Helpers;
using XamarinMoviesApp.Interfaces;
using XamarinMoviesApp.Services;

[assembly: Dependency(typeof(HttpService))]
namespace XamarinMoviesApp.Services
{
    public class HttpService : IHttpService
    {
        readonly HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<RequestResult<T>> Get<T>(string endpoint, Dictionary<string, object> parameters = null)
        {
            if (parameters != null) {
                endpoint = CreateRequestUri(endpoint, parameters);
            }
            return await ExecuteRequestAsync<T>(endpoint, HttpMethod.Get);
        }

		public async Task<RequestResult<T>> PostJson<T>(string endpoint, object body)
		{
			var a = JsonConvert.SerializeObject(body);
			var bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
			return await ExecuteRequestAsync<T>(endpoint, HttpMethod.Post, bodyContent);
		}

		public async Task<RequestResult<T>> PostBody<T>(string endpoint, Dictionary<string, string> body)
		{
			var bodyContent = new FormUrlEncodedContent(body.ToList());
			return await ExecuteRequestAsync<T>(endpoint, HttpMethod.Post, bodyContent);
		}

		public async Task<RequestResult<T>> ExecuteRequestAsync<T>(String url, HttpMethod method, HttpContent body = null)
		{
            //Verify internet connection before every request
            if (!CrossConnectivity.Current.IsConnected) {
                return new RequestResult<T>
                {
                    IsSuccess = false,
                    ErrorData = new ErrorData { Error = Error.NotConnected, ErrorMessage = Messages.NotConnectedMessage }
                };
            }

            var baseUrl = Settings.BaseUrl;

			try
			{
				var requestUrl = $"{baseUrl}{url}";
				HttpRequestMessage message = new HttpRequestMessage(method, requestUrl);

				if (body != null)
				{
					message.Content = body;
				}

                var response = await _httpClient.SendAsync(message);
				return await ToRequestResult<T>(response);
			}
			catch (Exception e)
			{
				return new RequestResult<T>
				{
					IsSuccess = false,
                    ErrorData = new ErrorData { Error = Error.Exception, ErrorMessage = e.Message }
				};
			}
		}

		private static string CreateRequestUri(string endpoint, Dictionary<string, object> parameters)
		{
			var sb = new StringBuilder();

            sb.Append(endpoint);

			bool first = true;
			foreach (var param in parameters)
			{
				var format = first ? "?{0}={1}" : "&{0}={1}";
				sb.AppendFormat(format, Uri.EscapeDataString(param.Key), Uri.EscapeDataString(param.Value.ToString()));
				first = false;
			}

			return sb.ToString();
		}

		private async Task<RequestResult<T>> ToRequestResult<T>(HttpResponseMessage response)
		{
			var requestResult = new RequestResult<T>();
			if (response.IsSuccessStatusCode)
			{
				try
				{
					var json = await response.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<T>(json);
					return new RequestResult<T>
					{
                        IsSuccess = true,
						Data = data
					};

				}
				catch (Exception ex)
				{
                    requestResult.IsSuccess = false;
                    requestResult.ErrorData = new ErrorData { Error = Error.Exception, ErrorMessage = ex.Message };
					return requestResult;
				}
			}

			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
                try
                {
					var json = await response.Content.ReadAsStringAsync();
					var errorResult = JsonConvert.DeserializeObject<ErrorData>(json);
                    errorResult.Error = Error.BadRequest;
                    requestResult.IsSuccess = false;
                    requestResult.ErrorData = errorResult;
                }
                catch (Exception ex)
                {
                    requestResult.IsSuccess = false;
                    requestResult.ErrorData = new ErrorData { Error = Error.Exception, ErrorMessage = ex.Message };
                }
			}

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				requestResult.IsSuccess = false;
                requestResult.ErrorData = new ErrorData { Error = Error.Unauthorized, ErrorMessage = response.ReasonPhrase };
			}

			requestResult.IsSuccess = false;
            requestResult.ErrorData = new ErrorData { Error = Error.Fail, ErrorMessage = response.ReasonPhrase };
			return requestResult;
		}
    }
}
