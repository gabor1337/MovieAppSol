using CommunityToolkit.Mvvm.ComponentModel;
using MovieApp.MVVM.Model;
using MovieApp.MVVM.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MovieApp.MVVM.ViewModel
{
    public partial class HomeViewModel : ObservableObject
    {

        private const int TotalMoviesToKeep = 20;
        private static readonly List<string> FallbackTitles = new()
        {
            "The Shawshank Redemption", "The Godfather", "The Dark Knight",
            "Pulp Fiction", "Fight Club", "Forrest Gump", "Inception",
            "The Matrix", "Goodfellas", "The Silence of the Lambs"
        };

        public PaginatedListViewModel<MovieModel> RandomMoviesList { get; } = new PaginatedListViewModel<MovieModel>();
        public PaginatedListViewModel<MovieModel> TopMoviesList { get; } = new PaginatedListViewModel<MovieModel>();

        public void SetMovies(ObservableCollection<MovieModel> allMovies)
        {
            try
            {
                if (allMovies?.Count > 0)
                {
                    // Set random movies
                    MovieCollectionUtils.SetRandomMovies(
                        allMovies,
                        RandomMoviesList.AllItems,
                        TotalMoviesToKeep);

                    // Set top rated movies
                    MovieCollectionUtils.SetTopMovies(
                        allMovies,
                        TopMoviesList.AllItems,
                        TotalMoviesToKeep);

                    // Reset pagination for both lists
                    RandomMoviesList.ResetPagination();
                    TopMoviesList.ResetPagination();
                }
                else
                {
                    LoadFallbackMovies();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Critical error: {ex}");
                LoadFallbackMovies();
            }
        }

        private void LoadFallbackMovies()
        {
            MovieCollectionUtils.LoadFallbackMovies(
                RandomMoviesList.AllItems,
                TotalMoviesToKeep,
                FallbackTitles);

            MovieCollectionUtils.LoadFallbackMovies(
                TopMoviesList.AllItems,
                TotalMoviesToKeep,
                FallbackTitles);

            RandomMoviesList.ResetPagination();
            TopMoviesList.ResetPagination();
        }
    }
}