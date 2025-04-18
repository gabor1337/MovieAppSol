using MovieApp.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace MovieApp.MVVM.View
{
    public partial class HomeView : UserControl
    {
        private const double MovieCardWidth = 220; // 200 width + 20 margin

        public HomeView()
        {
            InitializeComponent();
            Loaded += HomeView_Loaded;
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateItemsPerPage();
        }

        private void MoviesGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateItemsPerPage();
        }

        private void UpdateItemsPerPage()
        {
            if (DataContext is HomeViewModel vm)
            {
                vm.RandomMoviesList.CalculateItemsPerPage(MoviesGrid.ActualWidth, MovieCardWidth);
                vm.TopMoviesList.CalculateItemsPerPage(MoviesGrid.ActualWidth, MovieCardWidth);
            }
        }
    }
}