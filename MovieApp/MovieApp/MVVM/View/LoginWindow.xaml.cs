using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MovieApp.MVVM.View;

namespace MovieApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /* public class MovieAppContext : DbContext -> ide kell majd az adatb neve
         {
            // public DbSet<User> Users { get; set; }
         }*/

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                    builder.Append(b.ToString("x2")); // Hexadecimális formátum
                return builder.ToString();
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text.Trim();
            string password = PasswordTextBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Kérlek töltsd ki mindkét mezőt!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new MovieAppContext())
            {
                bool userExists = context.Users.Any(u => u.Username == username);

                if (userExists)
                {
                    MessageBox.Show("Ez a felhasználónév már létezik!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string hashedPassword = HashPassword(password);
                    var newUser = new User { Username = username, Password = hashedPassword };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    MessageBox.Show("Sikeres regisztráció!", "Register", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }



        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string username = UserTextBox.Text.Trim();
            string password = PasswordTextBox.Password;
            string hashedPassword = HashPassword(password);

            using (var context = new MovieAppContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    MessageBox.Show("Nincs ilyen felhasználó!", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (user.Password != hashedPassword)
                {
                    MessageBox.Show("Hibás jelszó!", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Sikeres bejelentkezés!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();        
                    this.Close();
                }
            }
        }


    }
}
