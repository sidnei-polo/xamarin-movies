using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinMoviesApp.Helpers;

namespace XamarinMoviesApp.Interfaces
{
    public interface IHttpService
    {
        Task<RequestResult<T>> Get<T>(string endpoint, Dictionary<string, object> parameters);
		Task<RequestResult<T>> PostBody<T>(string url, Dictionary<string, string> body);
		Task<RequestResult<T>> PostJson<T>(string url, object body);
    }
}