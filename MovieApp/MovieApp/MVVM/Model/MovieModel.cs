using System.Globalization;
using System.Text.Json.Serialization;


namespace MovieApp.MVVM.Model
{
    public class MovieModel
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("primaryTitle")]
        public string? PrimaryTitle { get; set; }

        [JsonPropertyName("primaryImage")]
        public string? PrimaryImage { get; set; }

        [JsonPropertyName("averageRating")]
        public double AverageRating { get; set; }

        [JsonPropertyName("genres")]
        public string[]? Genres { get; set; }

        [JsonPropertyName("startYear")]
        public string? StartYear { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("runtimeMinutes")]
        public string RunTime { get; set; }
    }       

    public class MovieBuilder
    {
        private readonly MovieModel movie = new MovieModel();
        public MovieBuilder setId(string id)
        {
            movie.Id = id;
            return this;
        }
        public MovieBuilder setPrimaryTitle(string title)
        {
            movie.PrimaryTitle = title;
            return this;
        }
        public MovieBuilder setPrimaryImage(string imageUrl)
        {
            movie.PrimaryImage = imageUrl;
            return this;
        }
        public MovieBuilder setAverageRating(double rating)
        {
            movie.AverageRating = rating;
            return this;
        }
        public MovieBuilder setGenre(string[] genres)
        {
            movie.Genres = genres;
            return this;
        }
        public MovieBuilder StartYear(string startYear)
        {
            movie.StartYear = startYear;
            return this;
        }
        public MovieBuilder setDesc(string desc)
        {
            movie.Description = desc;
            return this;
        }
        public MovieBuilder setRunTime(string runTime)
        {
            movie.RunTime = runTime;
            return this;
        }
        public MovieModel Build()
        {
            return movie; 
        }

    }
} 