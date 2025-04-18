namespace masterModel
{
    public partial class Megnézendő_film {

        public Megnézendő_film()
        {
            this.Filmek = new List<Film>();
            OnCreated();
        }

        public int lista_id { get; set; }

        public string film_id { get; set; }

        public DateTime hozzáadási_dátum { get; set; }

        public virtual IList<Film> Filmek { get; set; }

        public virtual Lisa Lisa { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
