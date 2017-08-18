﻿using Xamarin.Forms;

namespace XamarinMoviesApp.Views
{
	public class SectionView : ContentView
	{
        readonly Label _label;

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
			nameof(Text),
			typeof(string),
			typeof(SectionView),
			string.Empty
		);

		public string Text
		{
			get
			{
				return (string)GetValue(TextProperty);
			}
			set
			{
				SetValue(TextProperty, value);
				_label.Text = value;
			}
		}

		public SectionView()
		{
            var layout = new StackLayout { Spacing = 0 };

			if (Device.RuntimePlatform == "iOS")
			{
				_label = new Label
				{
					TextColor = Color.FromHex("#666666"),
					FontSize = 12,
					VerticalOptions = LayoutOptions.Center,
					Margin = new Thickness(16, 16, 16, 8)
				};


                var separator = new BoxView { HeightRequest = 0.5, BackgroundColor = Color.FromHex("DBDBDB"), VerticalOptions = LayoutOptions.Start };

				layout.Children.Add(separator);
				layout.Children.Add(_label);
			}

			if (Device.RuntimePlatform == "Android")
			{
				_label = new Label
				{
					TextColor = Color.FromHex("#757575"),
					FontSize = 14,
					Margin = new Thickness(16, 16),
					FontAttributes = FontAttributes.Bold
				};

				var separator = new BoxView { HeightRequest = 1, BackgroundColor = Color.FromHex("DBDBDB") , VerticalOptions = LayoutOptions.Start };

				layout.Children.Add(separator);
				layout.Children.Add(_label);
   			}

            Content = layout;
		}

		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == TextProperty.PropertyName)
			{
				_label.Text = Device.RuntimePlatform == "iOS" ? Text.ToUpper() : Text;
			}
		}
	}
}

