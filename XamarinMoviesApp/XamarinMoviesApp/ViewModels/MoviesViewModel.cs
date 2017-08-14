using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinMoviesApp.Interfaces;

namespace XamarinMoviesApp.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
		private int _loadedPages;

		private readonly IMoviesService _moviesService;
		private readonly INavigationService _navigationService;

		public MoviesViewModel()
        {
            _moviesService = DependencyService.Get<IMoviesService>();
            _navigationService = DependencyService.Get<INavigationService>();
            LoadMoviesAsync(1);
        }

        public ObservableCollection<MovieViewModel> Movies { get; protected set; } = new ObservableCollection<MovieViewModel>();

        private bool _noMoviesFound;
        public bool NoMoviesFound
        {
            get { return _noMoviesFound; }
            set { SetPropertyChanged(ref _noMoviesFound, value); }
        }

        private bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get { return _isLoadingMore; }
            set { SetPropertyChanged(ref _isLoadingMore, value); }
        }

        private MovieViewModel _selectedMovie;
        public MovieViewModel SelectedMovie
        {
            get { return _selectedMovie; }
            set { 
                SetPropertyChanged(ref _selectedMovie, value);
				if (value != null)
				{
					SelectedMovie = null;
					NavigateToMovieDetail(value);
				}
            }
        }

		string filter = string.Empty;
        public string Filter
		{
			get { return filter; }
			set
			{
				if (SetPropertyChanged(ref filter, value))
					ExecuteFilterMoviesAsync();
			}
		}

		async Task ExecuteFilterMoviesAsync()
		{
			if (!string.IsNullOrEmpty(Filter))
			{
				var query = Filter;
				await Task.Delay(500);
				if (query != Filter)
					return;
			}

            await LoadMoviesAsync(1, Filter);
		}

        public async Task LoadMoviesAsync(int page, string query = null)
		{
            NoMoviesFound = false;

			if (page == 1)
			{
				Movies.Clear();
                IsBusy = true;
			}

            var movies = await _moviesService.GetMoviesAsync(page, query);

            if (movies == null) {
                NoMoviesFound = true;
                IsBusy = false;
                return;
            }

            NoMoviesFound = movies.Count == 0 && page == 1;

            foreach (var item in movies)
            {
                Movies.Add(item);
            }

            _loadedPages = page;
            IsBusy = false;
		}

        private async Task NavigateToMovieDetail(MovieViewModel movie)
		{
            await _navigationService.NavigateToDetail(movie);
		}

        private ICommand _loadMoreCommand;
        public ICommand LoadMoreCommand => _loadMoreCommand ?? (_loadMoreCommand = new Command(async() => await ExecuteLoadMoreCommand(), CanExecuteLoadMoreCommand));

		public bool CanExecuteLoadMoreCommand()
		{
            return !IsLoadingMore;
		}

		public async Task ExecuteLoadMoreCommand()
        {
            IsLoadingMore = true;
            await LoadMoviesAsync(_loadedPages + 1, Filter);
            IsLoadingMore = false;
        }
    }
}
