using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AP.Desktop.Views
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private const string JsonFilePath = @"C:\Users\user\source\repos\AttendanceProject\ApplicationCore\Domain\Dummy.json";
        public LoginView()
        {
            InitializeComponent();
        }
        private (string username, string password) GetAuthCredentials()
        {
            string jsonString = File.ReadAllText(JsonFilePath);
            var jsonDoc = JsonDocument.Parse(jsonString);
            var authNode = jsonDoc.RootElement.GetProperty("Auth");

            string username = authNode.GetProperty("username").GetString();
            string password = authNode.GetProperty("pwd").GetString();

            return (username, password);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var (username, password) = GetAuthCredentials();

            if (txtUser.Text == username && txtPass.Password == password)
            {
                // Login successful, navigate to MainView
                MainView mainView = new MainView();
                mainView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
