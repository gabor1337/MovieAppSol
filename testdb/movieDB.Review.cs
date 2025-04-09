namespace MovieDB
{
    public partial class Review {

        public Review()
        {
            //generates the exact time
            publish_date = DateTime.Now;
            OnCreated();
        }

        

        public short review_id { get; set; }

        public string? content { get; set; }

        public DateTime publish_date { get; set; }

        public short stars { get; set; }

        public short movie_id { get; set; }

        public short user_id { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual User User { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
