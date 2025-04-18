namespace masterModel
{
    public partial class Felhasználó {

        public Felhasználó()
        {
            this.Értékelés = new List<Értékelés>();
            this.Lisas = new List<Lisa>();
            OnCreated();
        }

        public int felhasználó_id { get; set; }

        public string Felhasználó_név { get; set; }

        public string jelszó { get; set; }

        public string email { get; set; }

        public virtual IList<Értékelés> Értékelés { get; set; }

        public virtual IList<Lisa> Lisas { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
