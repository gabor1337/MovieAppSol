namespace MovieDB
{
    public partial class Movie_Watchlist
    {
        public Movie_Watchlist()
        {
            //generates the exact time
            this.added_date = DateTime.Now;
            OnCreated();
        }

        public DateTime added_date { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Watchlist Watchlist { get; set; }
        partial void OnCreated();
    }
}