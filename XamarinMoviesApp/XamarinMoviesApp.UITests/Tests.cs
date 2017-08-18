using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using XamarinMoviesApp.UITests.Pages;

namespace XamarinMoviesApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp _app;
        Platform _platform;

        public Tests(Platform platform)
        {
            _platform = platform;
        }

		[SetUp]
		public void BeforeEachTest()
		{
			_app = AppInitializer.StartApp(_platform);
		}

		[Test]
		public void SearchTest()
		{
            const string searchParam = "Star War";
            const string movieTitle = "Star Wars";

            new MoviesPage()
                .SearchMovie(searchParam)
                .SelectFirstMovie();

            AppResult[] result = _app.Query(movieTitle);
            Assert.IsTrue(result.Any(), "Movie Found");
		}
    }
}
