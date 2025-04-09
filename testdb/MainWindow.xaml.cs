using Microsoft.EntityFrameworkCore;
using MovieDB;
using System.Windows;

namespace testdb;

public partial class MainWindow : Window
{
    readonly MovieDB.MovieDB cn;

    public MainWindow()
    {
        InitializeComponent();
        //init database
        cn = new MovieDB.MovieDB();
        DBInic();
    }

    private void DBInic()
    {
        try
    {
        //make sure its created
        cn.Database.EnsureCreated();  // Correct method name (camelCase)
        SeedData();
        //test display 
        DisplayData();
        }
    catch (Exception ex)
    {
        MessageBox.Show($"Database initialization failed: {ex.Message}");
    }
    }

    //saves every database changes
    public void SeedData()
    {
        cn.Database.EnsureCreated();

        
            // Seed users first and save
            SeedUsers();
            cn.SaveChanges();  // Save users first

            // Seed movies and save
            SeedMovies();
            cn.SaveChanges();  // Save movies

            // Now seed dependent entities
            SeedWatchlists();
            cn.SaveChanges();

            SeedReviews();
            cn.SaveChanges();  // Final save
        
    }

    private void SeedUsers()
    {
        var users = new[]
        {
            new User { username = "john_doee", passwd = "BCrypt.HashPassword(Password123!" },
            new User { username = "jane_smith", passwd = "BCrypt.HashPassword(SecurePass456!" },
            new User { username = "movie_lover", passwd = "BCrypt.HashPassword(ILoveMovies789!" }
        };

        foreach (var user in users)
        {
            // Check if the username already in the Users database
            var existingUser = cn.Users
                .FirstOrDefault(r => (r.username == user.username));

            if (existingUser != null)
            {
                // Update existing user

                // Mark as modified
                //cn.Entry(existingUser).State = EntityState.Modified;
            }
            else
            {               
                cn.Users.Add(user);
                cn.SaveChanges();
            }
        }
    }

    private void SeedMovies()
    {
        var movies = new[]
        {
            new Movie
            {
                title = "The Shawshank Redemption",
                genre = "Drama",
                releaseYear = 1994,
                runTime = 142
                
            },
            new Movie
            {
                title = "The Godfather",
                genre = "Crime",
                releaseYear = 1972,
                runTime = 175
            },
            new Movie
            {
                title = "Inception",
                genre = "Sci-Fi",
                releaseYear = 2010,
                runTime = 148,
            },
            new Movie
            {
                title = "Parasite",
                genre = "Thriller",
                releaseYear = 2019,
                runTime = 132
            }
        };

        foreach (var movie in movies)
        {
            // Checks if this movie already in the Movies database
            //checks the title, in the future we have to be careful because there could movies with the same title. (Maybe check the runTime and releaseDate too)
            var existingMovie = cn.Movies
                .FirstOrDefault(r => (r.title == movie.title));

            if (existingMovie != null)
            {
                
            }
            else
            {
                cn.Movies.Add(movie);
                cn.SaveChanges();
            }
        }
    }

    private void SeedWatchlists()
    {
        var users = cn.Users.ToList();
        var movies = cn.Movies.ToList();

        var watchlists = new[]
        {
            new Watchlist
            {
                User = users[0],
                Movie_Watchlists1 = new[]
                {
                    new Movie_Watchlist
                    {
                        Movie = movies[0],
                    },
                    new Movie_Watchlist
                    {
                        Movie = movies[2],
                    },new Movie_Watchlist
                    {
                        Movie = movies[2],
                    }
                }
            },
            new Watchlist
            {
                isDefault = false,
                list_name = "sajatcucc",
                User = users[1],
                Movie_Watchlists1 = new[]
                {
                    new Movie_Watchlist
                    {
                        Movie = movies[1]
                    }
                }
            },new Watchlist
            {
                isDefault = false,
                list_name = "sajat cucc",
                User = users[1],
                Movie_Watchlists1 = new[]
                {
                    new Movie_Watchlist
                    {
                        Movie = movies[1]
                    }
                }
            },new Watchlist
            {
                User = users[1],
                Movie_Watchlists1 = new[]
                {
                    new Movie_Watchlist
                    {
                        Movie = movies[1]
                    }
                }
            }
        };
        foreach (var wl in watchlists)
        {
            // Find or create watchlist
            var watchlist = cn.Watchlists
                .Include(w => w.Movie_Watchlists1)
                .ThenInclude(mw => mw.Movie) // Ensure movies are loaded
                .FirstOrDefault(w => w.User.user_id == wl.User.user_id &&
                                   w.list_name == wl.list_name);

            //create a new watchlist if its not created yet
            if (watchlist == null)
            {
                watchlist = new Watchlist
                {
                    User = wl.User,
                    isDefault = wl.isDefault,
                    list_name = wl.list_name,
                    Movie_Watchlists1 = new List<Movie_Watchlist>()
                };
                cn.Watchlists.Add(watchlist);
            }

            // Add movies that aren't already in the watchlist
            foreach (var movie in wl.Movie_Watchlists1)
            {
                // check both movie_id to make sure its a different movie
                if (!watchlist.Movie_Watchlists1.Any(mw => mw.Movie.movie_id == movie.Movie.movie_id))
                {
                    watchlist.AddMovie(movie.Movie);
                    cn.SaveChanges();
                }
            }
        }

        
    }

    private void SeedReviews()
    {
        var users = cn.Users.ToList();
        var movies = cn.Movies.Include(m => m.Reviews).ToList();

        var reviews = new[]
        {
            new Review
            {
                content = "One of the greatest movies ever made!",
                stars = 5,
                Movie = movies[0],
                User = users[0]

            },
            new Review
            {
                content = "Masterpiece of storytelling",
                stars = 5,
                Movie = movies[1],
                User = users[1]
            },
            new Review
            {
                content = "Masterpiece of storytelling",
                stars = 2,
                Movie = movies[1],
                User = users[1]
            },
            new Review
            {
                content = "szar",
                stars = 9,
                Movie = movies[1],
                User = users[1]
            },
            new Review
            {
                content = "Mind-bending and visually stunning",
                stars = 4,
                Movie = movies[2],
                User = users[2]
            }
        };

        foreach (var review in reviews)
        {
            // Check if this user already has a review for this movie
            var existingReview = cn.Reviews
                .FirstOrDefault(r => (r.Movie.movie_id == review.Movie.movie_id &&
                                   r.User.user_id == review.User.user_id));

            if (existingReview !=null)
            {
                // Update existing review
                existingReview.content = review.content;
                existingReview.stars = review.stars;
                existingReview.publish_date = DateTime.Now;

                // Mark as modified
                cn.Entry(existingReview).State = EntityState.Modified;
            }
            else
            {
                // Add new review
                review.publish_date = DateTime.Now;
                review.Movie.AddReview(review);
                cn.Reviews.Add(review);
                cn.SaveChanges();
            }
        }

    }


    //its just for testing
    private void DisplayData()
    {
        var output = "";

        // Ensure all necessary data is loaded
        var movies = cn.Movies
            .Include(m => m.Reviews)
                .ThenInclude(r => r.User)
            .Include(m => m.Movie_Watchlists)
                .ThenInclude(mw => mw.Watchlist)
                    .ThenInclude(w => w.User)
            .ToList();

        foreach (var movie in movies)
        {
            output += $"{movie.title} ({movie.releaseYear}), {movie.genre}, {movie.runTime} mins\n";
            output += $"Avg Rating: {movie.avg_rating}\n";

            if (movie.Reviews?.Any() == true)
            {
                output += "Reviews:\n";
                foreach (var review in movie.Reviews)
                {
                    output += $"- {review.content} ({review.stars} stars) by {review.User?.username} on {review.publish_date:yyyy-MM-dd}\n";
                }
            }

            if (movie.Movie_Watchlists?.Any() == true)
            {
                output += "In Watchlists:\n";
                foreach (var mw in movie.Movie_Watchlists)
                {
                    output += $"- {mw.Watchlist?.User?.username}'s watchlist (added: {mw.added_date:yyyy-MM-dd})\n";
                }
            }

            output += "\n";
        }

        var users = cn.Users
            .Include(u => u.Watchlists)
                .ThenInclude(w => w.Movie_Watchlists1)
                    .ThenInclude(mw => mw.Movie)
            .ToList();

        foreach (var user in users)
        {
            output += $"\nUser: {user.username}\n";
            output += $"Watchlists ({user.Watchlists?.Count ?? 0}):\n";

            foreach (var watchlist in user.Watchlists ?? Enumerable.Empty<Watchlist>())
            {
                output += $"- Watchlist created {watchlist.create_date:yyyy-MM-dd} ({(watchlist.isDefault ? "default" : "custom")})\n";
                output += $"  Movies ({watchlist.Movie_Watchlists1?.Count ?? 0}):\n";

                foreach (var mw in watchlist.Movie_Watchlists1 ?? Enumerable.Empty<Movie_Watchlist>())
                {
                    output += $"  - {mw.Movie?.title} (added: {mw.added_date:yyyy-MM-dd})\n";
                }
            }
        }

        MessageBox.Show(output);
    }


}