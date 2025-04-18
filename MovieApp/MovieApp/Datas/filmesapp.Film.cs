namespace masterModel
{
    public partial class Film {

        public Film()
        {
            this.Értékelés = new List<Értékelés>();
            this.Megnézendő_film = new List<Megnézendő_film>();
            OnCreated();
        }

        public int film_id { get; set; }

        public int Időtartam { get; set; }

        public int Megjelenési_év { get; set; }

        public double Átlag_értékelés { get; set; }

        public string Kép { get; set; }

        public string Műfaj { get; set; }

        public string Cím { get; set; }

        public virtual IList<Értékelés> Értékelés { get; set; }

        public virtual IList<Megnézendő_film> Megnézendő_film { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
