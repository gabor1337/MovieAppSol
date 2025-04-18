using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieApp.MVVM.Utils;
using System.Collections.ObjectModel;

namespace MovieApp.MVVM.ViewModel
{
    public partial class PaginatedListViewModel<T> : ObservableObject
    {
        private int _currentPage = 0;
        private int _itemsPerPage = 7;

        [ObservableProperty]
        private ObservableCollection<T> _allItems = new();

        [ObservableProperty]
        private ObservableCollection<T> _visibleItems = new();

        public int CurrentPage => _currentPage;

        public int ItemsPerPage
        {
            get => _itemsPerPage;
            set
            {
                if (SetProperty(ref _itemsPerPage, value))
                {
                    // Ensure currentPage is valid after ItemsPerPage changes
                    AdjustCurrentPageAfterItemsPerPageChange();
                    UpdateVisibleItems();
                    OnPropertyChanged(nameof(CanGoNext));
                    OnPropertyChanged(nameof(CanGoPrevious));
                }
            }
        }

        public bool CanGoNext => PaginationUtils.CanGoNext(_currentPage, ItemsPerPage, AllItems);
        public bool CanGoPrevious => PaginationUtils.CanGoPrevious(_currentPage);

        public void CalculateItemsPerPage(double containerWidth, double itemWidth)
        {
            ItemsPerPage = Math.Max(1, (int)(containerWidth / itemWidth));
        }

        [RelayCommand]
        public void NextPage()
        {
            if (CanGoNext)
            {
                _currentPage++;
                UpdateVisibleItems();
                OnPropertyChanged(nameof(CanGoNext));
                OnPropertyChanged(nameof(CanGoPrevious));
            }
        }

        [RelayCommand]
        public void PreviousPage()
        {
            if (CanGoPrevious)
            {
                _currentPage--;
                UpdateVisibleItems();
                OnPropertyChanged(nameof(CanGoNext));
                OnPropertyChanged(nameof(CanGoPrevious));
            }
        }

        public void ResetPagination()
        {
            _currentPage = 0;
            UpdateVisibleItems();
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(CanGoPrevious));
        }

        protected void UpdateVisibleItems()
        {
            PaginationUtils.UpdateVisibleItems(AllItems, VisibleItems, _currentPage, ItemsPerPage);
        }

        /// <summary>
        /// Adjusts currentPage if it becomes invalid after ItemsPerPage changes.
        /// </summary>
        private void AdjustCurrentPageAfterItemsPerPageChange()
        {
            if (AllItems.Count == 0)
            {
                _currentPage = 0;
                return;
            }

            int maxPage = Math.Max(0, (int)Math.Ceiling((double)AllItems.Count / ItemsPerPage) - 1);
            _currentPage = Math.Min(_currentPage, maxPage);
        }
    }
}