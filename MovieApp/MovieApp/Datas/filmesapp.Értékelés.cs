namespace masterModel
{
    public partial class Értékelés {

        public Értékelés()
        {
            OnCreated();
        }

        public int értékelés_id { get; set; }

        public int csillagok { get; set; }

        public DateTime közlési_dátum { get; set; }

        public string tartalom { get; set; }

        public int film_id { get; set; }

        public int felhasználó_id { get; set; }

        public virtual Film Film { get; set; }

        public virtual Felhasználó értékelések { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
