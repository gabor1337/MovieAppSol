namespace MovieDB
{
    public partial class User {

        public User()
        {
            this.Watchlists = new List<Watchlist>();
            this.Reviews = new List<Review>();
            OnCreated();
        }

        public short user_id { get; set; }

        public string? username { get; set; }

        public string? passwd { get; set; }

        public virtual IList<Watchlist> Watchlists { get; set; }

        public virtual IList<Review> Reviews { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
