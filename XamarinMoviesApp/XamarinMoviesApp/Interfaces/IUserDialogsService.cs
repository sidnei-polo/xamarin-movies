using System.Threading.Tasks;

namespace XamarinMoviesApp.Interfaces
{
	public interface IUserDialogsService
	{
		Task DisplayAlert(string title, string message);
		Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
	}
}
