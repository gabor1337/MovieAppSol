// MovieCollectionUtils.cs
using MovieApp.MVVM.Model;
using System.Collections.ObjectModel;

namespace MovieApp.MVVM.Utils
{
    public static class MovieCollectionUtils
    {
        public static void SetRandomMovies(
            ObservableCollection<MovieModel> sourceCollection,
            ObservableCollection<MovieModel> targetCollection,
            int count,
            bool clearExisting = true)
        {
            if (clearExisting)
            {
                targetCollection.Clear();
            }

            if (sourceCollection?.Count > 0)
            {
                var randomSelection = sourceCollection
                    .OrderBy(x => Random.Shared.Next())
                    .Take(count)
                    .ToList();

                foreach (var movie in randomSelection)
                {
                    targetCollection.Add(movie);
                }
            }
        }
        public static void SetTopMovies(
            ObservableCollection<MovieModel> sourceCollection,
            ObservableCollection<MovieModel> targetCollection,
            int count,
            bool clearExisting = true)
        {
            if (clearExisting)
            {
                targetCollection.Clear();
            }

            if (sourceCollection?.Count > 0)
            {
                var Selection = sourceCollection
                        .OrderByDescending(m => m.AverageRating)
                        .Take(count)
                        .ToList();

                foreach (var movie in Selection)
                {
                    targetCollection.Add(movie);
                }
            }
        }
        public static void LoadFallbackMovies(
            ObservableCollection<MovieModel> targetCollection,
            int count,
            List<string> fallbackTitles)
        {
            targetCollection.Clear();

            for (int i = 0; i < count; i++)
            {
                var randomTitle = fallbackTitles[Random.Shared.Next(fallbackTitles.Count)];
                targetCollection.Add(new MovieModel
                {
                    PrimaryTitle = randomTitle,
                    PrimaryImage = $"https://via.placeholder.com/200x300?text={Uri.EscapeDataString(randomTitle)}",
                    AverageRating = Math.Round(Random.Shared.NextDouble() * 3 + 7, 1)
                });
            }
        }
    }
}