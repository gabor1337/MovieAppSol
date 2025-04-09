namespace MovieDB
{
    public partial class Movie {

        public Movie()
        {
            //a new movie has 0 reviews and 0.0 rating (on default)
            this.review_count = 0;
            this.avg_rating = 0;

            //init an empty "Reviews" list and a empty "Movie_watchlists" list 
            this.Reviews = new List<Review>();
            this.Movie_Watchlists = new List<Movie_Watchlist>();
            OnCreated();
        }

        //connects the review to the movie
        public void AddReview(Review review)
        {
            // Set the relationship from both sides
            review.Movie = this;
            Reviews.Add(review);

            // Update the counts and average
            review_count = Reviews.Count;
            avg_rating = (decimal)Reviews.Average(r => r.stars);
        }

        public short movie_id { get; set; }
        public int review_count { get; set; }

        public decimal avg_rating { get; set; }

        public string? title { get; set; }

        public string? genre { get; set; }

        public short releaseYear { get; set; }

        public short runTime { get; set; }

        public virtual IList<Review> Reviews { get; set; }

        public virtual IList<Movie_Watchlist> Movie_Watchlists { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
