using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieApp.MVVM.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace MovieApp.MVVM.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        private HomeViewModel _homeVM;

        [ObservableProperty]
        private TopMoviesViewModel _topMoviesVM;

        [ObservableProperty]
        private SelectedMoviePageViewModel _selectedMovieVM;

        [ObservableProperty]
        private ObservableCollection<MovieModel> _allMovies;

        [ObservableProperty]
        private MovieModel _selectedMovie;

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            TopMoviesVM = new TopMoviesViewModel();
            SelectedMovieVM = new SelectedMoviePageViewModel();
            CurrentView = HomeVM;

            // Start loading movies when MainViewModel is created
            _ = InitializeMovies();
        }

        private async Task InitializeMovies()
        {
            // Only load if we don't have movies already
            if (AllMovies == null || AllMovies.Count == 0)
            {
                AllMovies = await MovieApi.GetMoviesFromApi();
                HomeVM.SetMovies(AllMovies);
            }
            
        }

        [RelayCommand]
        private void NavigateToHome()
        {
            if (CurrentView != HomeVM)
            {
                CurrentView = HomeVM;
                HomeVM.SetMovies(AllMovies);
            }
        }

        [RelayCommand]
        private void NavigateToTopMovies()
        {
            if (CurrentView != TopMoviesVM)
            {
                CurrentView = TopMoviesVM;
            }
        }

        [RelayCommand]
        private void ShowMovieDetails(MovieModel movie)
        {
            if (movie != null)
            {
                System.Diagnostics.Debug.WriteLine(movie.RunTime);
                SelectedMovie = movie;
                SelectedMovieVM.SetMovie(movie); // Update the selected movie VM
                CurrentView = SelectedMovieVM;   // Switch to the selected movie view
            }
        }

        [RelayCommand]
        private void NavigateBack()
        {
            CurrentView = HomeVM;
        }
    }
}