using System.Collections.ObjectModel;

namespace MovieApp.MVVM.Utils
{
    public static class PaginationUtils
    {
        public static void UpdateVisibleItems<T>(
    ObservableCollection<T> sourceCollection,
    ObservableCollection<T> visibleCollection,
    int currentPage,
    int itemsPerPage)
        {
            visibleCollection.Clear();

            var startIndex = currentPage * itemsPerPage;
            var availableItems = sourceCollection.Count - startIndex;

            // If we don't have enough items on the current page to fill itemsPerPage
            if (availableItems < itemsPerPage && availableItems > 0)
            {
                // Calculate how many items we need from the previous page
                var itemsNeeded = itemsPerPage - availableItems;
                // Ensure we don't go before the start of the collection
                var newStartIndex = Math.Max(0, startIndex - itemsNeeded);

                // Take itemsPerPage items starting from newStartIndex
                var itemsToShow = sourceCollection.Skip(newStartIndex).Take(itemsPerPage).ToList();

                foreach (var item in itemsToShow)
                {
                    visibleCollection.Add(item);
                }
            }
            else
            {
                // Normal case - we have enough items on the current page
                var itemsToShow = sourceCollection.Skip(startIndex).Take(itemsPerPage).ToList();

                foreach (var item in itemsToShow)
                {
                    visibleCollection.Add(item);
                }
            }
        }

        public static bool CanGoNext<T>(int currentPage, int itemsPerPage, ObservableCollection<T> sourceCollection)
        {
            return (currentPage + 1) * itemsPerPage < sourceCollection.Count;
        }

        public static bool CanGoPrevious(int currentPage)
        {
            return currentPage > 0;
        }
    }
}