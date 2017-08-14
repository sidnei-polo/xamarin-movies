using NUnit.Framework;
using Xamarin.UITest;
using XamarinMoviesApp.UITests.Pages;

namespace XamarinMoviesApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

        [Test]
        public void VisualizationTest()
        {
            new MoviesPage()
                .SelectFirstMovie();

            new MovieDetailPage()
                .GoBack();
        }

		[Test]
		public void SearchTest()
		{
            new MoviesPage()
                .SearchMovie("Star Wars")
                .SelectFirstMovie();

			new MovieDetailPage()
				.GoBack();
		}
    }
}
