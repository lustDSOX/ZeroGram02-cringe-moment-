using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWindow;
        public Login(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            InitializeComponent();
        }

        public class User
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public int ID { get; set; }

            public User(string login, string password, int id)
            {
                Login = login;
                Password = password;
                ID = id;
            }
        }

        //public IEnumerable<User> ReadCSV()
        //{
        //    //string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension("data", ".csv"));
        //    //return lines.Select(line =>
        //    //{
        //    //    string[] data = line.Split(';');
        //    //    return new User(data[0], data[1], Convert.ToInt32(data[2]));
        //    //});
        //}

        private void login_text_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void login_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (login_text.Text == "Login") login_text.Text = "";
        }

        private void login_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (login_text.Text == "") login_text.Text = "Login";
        }

        private void password_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password_text.Text == "Password") password_text.Text = "";
        }

        private void password_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (password_text.Text == "") password_text.Text = "Password";
        }

        private void log_inBTN_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in ReadCSV())
            {
                if (item.Login == login_text.Text && item.Password == password_text.Text)
                {
                    mainWindow.OpenPage(MainWindow.pages.maininterface); ;
                }
            }
        }

        private void sign_inBTN_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in ReadCSV())
            {
                if (item.Login == login_text.Text && item.Password == password_text.Text)
                {
                    mainWindow.OpenPage(MainWindow.pages.maininterface);
                }
            }
        }
    }
}
