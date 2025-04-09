namespace MovieDB
{
    public partial class Movie_Watchlist
    {
        public Movie_Watchlist()
        {
            this.added_date = DateTime.Now;
            OnCreated();
        }

        public DateTime added_date { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Watchlist Watchlist { get; set; }

        public override bool Equals(object obj)
        {
            Movie_Watchlist toCompare = obj as Movie_Watchlist;
            if (toCompare == null) return false;

            // Compare both movie and watchlist IDs for proper equality
            return this.Movie.movie_id == toCompare.Movie.movie_id &&
                   this.Watchlist.watchlist_id == toCompare.Watchlist.watchlist_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Movie?.movie_id, Watchlist?.watchlist_id);
        }

        partial void OnCreated();
    }
}