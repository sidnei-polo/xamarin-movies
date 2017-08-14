using System;
using System.Threading;

namespace XamarinMoviesApp.UITests.Pages
{
    public class MoviesPage : BasePage
    {
		public MoviesPage SelectFirstMovie()
		{
            Thread.Sleep(3000);
			App.Tap("MovieItem");
			return this;
		}

		public MoviesPage SearchMovie(string name)
		{
			App.EnterText("Search", name);
			App.DismissKeyboard();
            Thread.Sleep(3000);
			return this;
		}
    }
}
