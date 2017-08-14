using System;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinMoviesApp.Droid.Renderers;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSerachBarRenderer))]
namespace XamarinMoviesApp.Droid.Renderers
{
    public class CustomSerachBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
			if (Control == null)
				return;
            UpdateSearchIcon();
        }

		void UpdateSearchIcon()
		{
			try
			{
				var searchId = Control.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
				if (searchId == 0)
					return;


				var image = this.FindViewById<ImageView>(searchId);
				if (image == null)
					return;

                image.SetImageResource(Resource.Drawable.ic_search_white_24dp);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("Unable to get icon" + ex);
			}
		}
    }
}
