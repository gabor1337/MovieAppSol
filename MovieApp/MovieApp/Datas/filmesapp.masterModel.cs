using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace masterModel
{

    public partial class masterModel : DbContext
    {

        public masterModel() :
            base()
        {
            OnCreated();
        }

        public masterModel(DbContextOptions<masterModel> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=True");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<Film> Films
        {
            get;
            set;
        }

        public virtual DbSet<Felhasználó> Felhasználós
        {
            get;
            set;
        }

        public virtual DbSet<Értékelés> Értékelés
        {
            get;
            set;
        }

        public virtual DbSet<Lisa> Lisas
        {
            get;
            set;
        }

        public virtual DbSet<Megnézendő_film> Megnézendő_films
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.FilmMapping(modelBuilder);
            this.CustomizeFilmMapping(modelBuilder);

            this.FelhasználóMapping(modelBuilder);
            this.CustomizeFelhasználóMapping(modelBuilder);

            this.ÉrtékelésMapping(modelBuilder);
            this.CustomizeÉrtékelésMapping(modelBuilder);

            this.LisaMapping(modelBuilder);
            this.CustomizeLisaMapping(modelBuilder);

            this.Megnézendő_filmMapping(modelBuilder);
            this.CustomizeMegnézendő_filmMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region Film Mapping

        private void FilmMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().ToTable(@"Filmek");
            modelBuilder.Entity<Film>().Property(x => x.film_id).HasColumnName(@"film_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Időtartam).HasColumnName(@"Időtartam").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Megjelenési_év).HasColumnName(@"Megjelenési_év").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Átlag_értékelés).HasColumnName(@"Átlag_értékelés").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Kép).HasColumnName(@"Kép").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Műfaj).HasColumnName(@"Műfaj").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().Property(x => x.Cím).HasColumnName(@"Cím").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Film>().HasKey(@"film_id");
        }

        partial void CustomizeFilmMapping(ModelBuilder modelBuilder);

        #endregion

        #region Felhasználó Mapping

        private void FelhasználóMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Felhasználó>().ToTable(@"Felhasználók");
            modelBuilder.Entity<Felhasználó>().Property(x => x.felhasználó_id).HasColumnName(@"felhasználó_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Felhasználó>().Property(x => x.Felhasználó_név).HasColumnName(@"Felhasználó_név").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Felhasználó>().Property(x => x.jelszó).HasColumnName(@"jelszó").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Felhasználó>().Property(x => x.email).HasColumnName(@"email").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Felhasználó>().HasKey(@"felhasználó_id");
        }

        partial void CustomizeFelhasználóMapping(ModelBuilder modelBuilder);

        #endregion

        #region Értékelés Mapping

        private void ÉrtékelésMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Értékelés>().ToTable(@"Értékelések");
            modelBuilder.Entity<Értékelés>().Property(x => x.értékelés_id).HasColumnName(@"értékelés_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().Property(x => x.csillagok).HasColumnName(@"csillagok").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().Property(x => x.közlési_dátum).HasColumnName(@"közlési_dátum").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().Property(x => x.tartalom).HasColumnName(@"tartalom").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().Property(x => x.film_id).HasColumnName(@"film_id").ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().Property(x => x.felhasználó_id).HasColumnName(@"felhasználó_id").ValueGeneratedNever();
            modelBuilder.Entity<Értékelés>().HasKey(@"értékelés_id");
        }

        partial void CustomizeÉrtékelésMapping(ModelBuilder modelBuilder);

        #endregion

        #region Lisa Mapping

        private void LisaMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lisa>().ToTable(@"listák");
            modelBuilder.Entity<Lisa>().Property(x => x.lista_id).HasColumnName(@"lista_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Lisa>().Property(x => x.név).HasColumnName(@"név").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Lisa>().Property(x => x.Felhasználó_id).HasColumnName(@"Felhasználó_id").ValueGeneratedNever();
            modelBuilder.Entity<Lisa>().HasKey(@"lista_id");
        }

        partial void CustomizeLisaMapping(ModelBuilder modelBuilder);

        #endregion

        #region Megnézendő_film Mapping

        private void Megnézendő_filmMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Megnézendő_film>().ToTable(@"Megnézendő_filmek");
            modelBuilder.Entity<Megnézendő_film>().Property(x => x.lista_id).HasColumnName(@"lista_id").ValueGeneratedNever();
            modelBuilder.Entity<Megnézendő_film>().Property(x => x.film_id).HasColumnName(@"film_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Megnézendő_film>().Property(x => x.hozzáadási_dátum).HasColumnName(@"hozzáadási_dátum").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Megnézendő_film>().HasKey(@"lista_id");
        }

        partial void CustomizeMegnézendő_filmMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().HasMany(x => x.Értékelés).WithOne(op => op.Film).HasForeignKey(@"film_id").IsRequired(true);
            modelBuilder.Entity<Film>().HasMany(x => x.Megnézendő_film).WithMany(op => op.Filmek);

            modelBuilder.Entity<Felhasználó>().HasMany(x => x.Értékelés).WithOne(op => op.értékelések).HasForeignKey(@"felhasználó_id").IsRequired(true);
            modelBuilder.Entity<Felhasználó>().HasMany(x => x.Lisas).WithOne(op => op.felhasználólista).HasForeignKey(@"Felhasználó_id").IsRequired(true);

            modelBuilder.Entity<Értékelés>().HasOne(x => x.Film).WithMany(op => op.Értékelés).HasForeignKey(@"film_id").IsRequired(true);
            modelBuilder.Entity<Értékelés>().HasOne(x => x.értékelések).WithMany(op => op.Értékelés).HasForeignKey(@"felhasználó_id").IsRequired(true);

            modelBuilder.Entity<Lisa>().HasOne(x => x.felhasználólista).WithMany(op => op.Lisas).HasForeignKey(@"Felhasználó_id").IsRequired(true);
            modelBuilder.Entity<Lisa>().HasMany(x => x.Megnézendő_filmek).WithOne(op => op.Lisa).HasForeignKey(@"lista_id").IsRequired(true);

            modelBuilder.Entity<Megnézendő_film>().HasOne(x => x.Lisa).WithMany(op => op.Megnézendő_filmek).HasForeignKey(@"lista_id").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}
