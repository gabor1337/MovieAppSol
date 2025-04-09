using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MovieDB
{

    public partial class MovieDB : DbContext
    {

        private readonly IConfiguration _configuration;

        public MovieDB() :
            base()
        {
            OnCreated();
        }

        public MovieDB(DbContextOptions<MovieDB> options, IConfiguration configuration) :
            base(options)
        {
            _configuration = configuration;
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=Robi\SQL2022;Database=MovieDB;Integrated Security=True;TrustServerCertificate=True;"
                );
            }
        }
        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Watchlist> Watchlists { get; set; }
    public DbSet<Movie_Watchlist> Movie_Watchlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.UserMapping(modelBuilder);
            this.CustomizeUserMapping(modelBuilder);

            this.WatchlistMapping(modelBuilder);
            this.CustomizeWatchlistMapping(modelBuilder);

            this.Movie_WatchlistMapping(modelBuilder);
            this.CustomizeMovie_WatchlistMapping(modelBuilder);

            this.ReviewMapping(modelBuilder);
            this.CustomizeReviewMapping(modelBuilder);

            this.MovieMapping(modelBuilder);
            this.CustomizeMovieMapping(modelBuilder);

            RelationshipsMapping(modelBuilder);
            CustomizeMapping(ref modelBuilder);
        }

        #region User Mapping

        private void UserMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable(@"Users");
            modelBuilder.Entity<User>().Property(x => x.user_id).HasColumnName(@"user_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(x => x.username).HasColumnName(@"username").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<User>().Property(x => x.passwd).HasColumnName(@"passwd").IsRequired().ValueGeneratedNever().HasMaxLength(255);
            modelBuilder.Entity<User>().HasKey(@"user_id");
        }

        partial void CustomizeUserMapping(ModelBuilder modelBuilder);

        #endregion

        #region Watchlist Mapping

        private void WatchlistMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>().ToTable(@"Watchlists");
            modelBuilder.Entity<Watchlist>().Property(x => x.watchlist_id).HasColumnName(@"watchlist_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Watchlist>().Property(x => x.list_name).HasColumnName(@"list_name").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<Watchlist>().Property(x => x.create_date).HasColumnName(@"create_date").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Watchlist>().Property(x => x.isDefault).HasColumnName(@"isDefault").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Watchlist>().Property(x => x.movie_count).HasColumnName(@"movie_count").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Watchlist>().Property<short>(@"user_id").HasColumnName(@"user_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Watchlist>().HasKey(@"watchlist_id");
        }

        partial void CustomizeWatchlistMapping(ModelBuilder modelBuilder);

        #endregion

        #region Movie_Watchlist Mapping

        private void Movie_WatchlistMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie_Watchlist>().ToTable(@"Movie_watchlists");
            modelBuilder.Entity<Movie_Watchlist>().Property<short>(@"movie_id").HasColumnName(@"movie_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie_Watchlist>().Property<short>(@"watchlist_id").HasColumnName(@"watchlist_id").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie_Watchlist>().Property(x => x.added_date).HasColumnName(@"added_date").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie_Watchlist>().HasKey(@"movie_id", @"watchlist_id");
        }

        partial void CustomizeMovie_WatchlistMapping(ModelBuilder modelBuilder);

        #endregion

        #region Review Mapping

        private void ReviewMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().ToTable(@"Reviews");
            modelBuilder.Entity<Review>().Property(x => x.review_id).HasColumnName(@"review_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>().Property(x => x.content).HasColumnName(@"content").ValueGeneratedNever().HasMaxLength(500);
            modelBuilder.Entity<Review>().Property(x => x.publish_date).HasColumnName(@"publish_date").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Review>().Property(x => x.stars).HasColumnName(@"stars").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Review>().Property(x => x.movie_id).HasColumnName(@"movie_id").IsRequired();
            modelBuilder.Entity<Review>().Property(x => x.user_id).HasColumnName(@"user_id").IsRequired();
            modelBuilder.Entity<Review>().HasKey(@"review_id");

        }

        partial void CustomizeReviewMapping(ModelBuilder modelBuilder);

        #endregion

        #region Movie Mapping

        private void MovieMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable(@"Movies");
            modelBuilder.Entity<Movie>().Property(x => x.movie_id).HasColumnName(@"movie_id").IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Movie>().Property(x => x.avg_rating).HasColumnName(@"avg_rating").IsRequired().ValueGeneratedNever().HasPrecision(5, 1);
            modelBuilder.Entity<Movie>().Property(x => x.title).HasColumnName(@"title").IsRequired().ValueGeneratedNever().HasMaxLength(100);
            modelBuilder.Entity<Movie>().Property(x => x.genre).HasColumnName(@"genre").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            modelBuilder.Entity<Movie>().Property(x => x.releaseYear).HasColumnName(@"releaseYear").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie>().Property(x => x.review_count).HasColumnName(@"review_count").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie>().Property(x => x.runTime).HasColumnName(@"runTime").IsRequired().ValueGeneratedNever();
            modelBuilder.Entity<Movie>().HasKey(@"movie_id");
        }

        partial void CustomizeMovieMapping(ModelBuilder modelBuilder);

        #endregion

        private void RelationshipsMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Watchlists).WithOne(op => op.User).HasForeignKey(@"user_id").IsRequired(true);
            modelBuilder.Entity<User>().HasMany(x => x.Reviews).WithOne(op => op.User).HasForeignKey(@"user_id").OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Watchlist>().HasOne(x => x.User).WithMany(op => op.Watchlists).HasForeignKey(@"user_id").IsRequired(true);
            modelBuilder.Entity<Watchlist>().HasMany(x => x.Movie_Watchlists1).WithOne(op => op.Watchlist).HasForeignKey(@"watchlist_id").IsRequired(true);

            modelBuilder.Entity<Movie_Watchlist>().HasOne(x => x.Movie).WithMany(op => op.Movie_Watchlists).HasForeignKey(@"movie_id").IsRequired(true);
            modelBuilder.Entity<Movie_Watchlist>().HasOne(x => x.Watchlist).WithMany(op => op.Movie_Watchlists1).HasForeignKey(@"watchlist_id").IsRequired(true);

            modelBuilder.Entity<Review>().HasOne(x => x.Movie).WithMany(op => op.Reviews).HasForeignKey(@"movie_id").IsRequired(true);
            modelBuilder.Entity<Review>().HasOne(x => x.User).WithMany(op => op.Reviews).HasForeignKey(@"user_id").IsRequired(true);

            modelBuilder.Entity<Movie>().HasMany(x => x.Reviews).WithOne(op => op.Movie).HasForeignKey(@"movie_id").OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Movie>().HasMany(x => x.Movie_Watchlists).WithOne(op => op.Movie).HasForeignKey(@"movie_id").IsRequired(true);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }


        partial void OnCreated();
    }
}
