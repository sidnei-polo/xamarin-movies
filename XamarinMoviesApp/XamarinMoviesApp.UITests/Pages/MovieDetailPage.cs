using System;
using System.Threading;

namespace XamarinMoviesApp.UITests.Pages
{
    public class MovieDetailPage : BasePage
    {
		public MovieDetailPage GoBack()
		{
            Thread.Sleep(3000);
            App.Back();
			return this;
		}
    }
}
