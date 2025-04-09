namespace MovieDB
{
    public partial class Watchlist {

        public Watchlist()
        {
            create_date = DateTime.Now;
            //isDefault always true if its not a custom list
            this.isDefault = true;
            //decides if the list will be default or custom
            CustomList(this.isDefault,this.list_name);
            this.movie_count = 0;
            this.Movie_Watchlists1 = new List<Movie_Watchlist>();
            OnCreated();
        }

        
        public void CustomList(bool isDefault, string customName = null)
        {
            //if isDefault = true -> list_name will be "watchlist"
            if (isDefault)
            {
                this.list_name = "Watchlist";
            }
            //isDefault = false
            else
            {
                //we check if the user named the list to something or left it empty
                if (!string.IsNullOrWhiteSpace(customName))
                {
                    this.list_name = customName;
                }
                else
                {
                    //throw exception to the user if the customName is empty
                    throw new ArgumentException("Custom list name must be provided when isDefault is false");
                }
            }
        }
        public void AddMovie(Movie movie)
        {
            // Check if movie already exists in this watchlist
            if (!Movie_Watchlists1.Any(mw => mw.Movie.movie_id == movie.movie_id))
            {
                var movieWatchlist = new Movie_Watchlist
                {
                    Movie = movie,
                    Watchlist = this,
                    added_date = DateTime.Now
                };

                Movie_Watchlists1.Add(movieWatchlist);
                movie_count = Movie_Watchlists1.Count;
            }
        }
        public short watchlist_id { get; set; }
        public string list_name { get; set; }
        public DateTime create_date { get; set; }
        public int movie_count { get; set; }
        public bool isDefault { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Movie_Watchlist> Movie_Watchlists1 { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
