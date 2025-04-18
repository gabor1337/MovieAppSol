using CommunityToolkit.Mvvm.ComponentModel;
using MovieApp.MVVM.Model;

namespace MovieApp.MVVM.ViewModel
{
    public partial class SelectedMoviePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private MovieModel _currentMovie;

        public void SetMovie(MovieModel movie)
        {
            CurrentMovie = movie;
        }
    }
}
