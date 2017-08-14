using System;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace XamarinMoviesApp.UITests.Pages
{
	public class BasePage
	{
		protected readonly IApp App;
		protected readonly bool OnAndroid;
		protected readonly bool OniOS;

		public BasePage()
		{
            App = AppInitializer.App;
			OnAndroid = App.GetType() == typeof(AndroidApp);
			OniOS = App.GetType() == typeof(iOSApp);
		}
	}
}
