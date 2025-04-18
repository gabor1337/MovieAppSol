namespace masterModel
{
    public partial class Lisa {

        public Lisa()
        {
            this.Megnézendő_filmek = new List<Megnézendő_film>();
            OnCreated();
        }

        public int lista_id { get; set; }

        public string név { get; set; }

        public int Felhasználó_id { get; set; }

        public virtual Felhasználó felhasználólista { get; set; }

        public virtual IList<Megnézendő_film> Megnézendő_filmek { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
