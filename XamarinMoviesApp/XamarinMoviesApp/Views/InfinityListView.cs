using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinMoviesApp.Views
{
    public class InfiniteListView : ListView
    {
		public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
			nameof(LoadMoreCommand),
            typeof(ICommand),
			typeof(InfiniteListView),
			default(ICommand)
		);

        public ICommand LoadMoreCommand
        {
            get { return (ICommand)GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }

        public InfiniteListView()
		{
			ItemAppearing += InfiniteListView_ItemAppearing;
		}

        public InfiniteListView (ListViewCachingStrategy strategy) : base(strategy)
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            if (ItemsSource is IList items && e.Item == items[items.Count - 1])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
            }
        }
    }
}
