using System;
using System.Collections.Generic;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;
namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWindow;
        public int ID;
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
        }

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
            var path = System.IO.Path.GetFullPath(@"..\\..\\data\data.txt");
            StreamReader data = new StreamReader(path);
            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] data_array = line.Split(';');
                    if (data_array[1] == login_text.Text && data_array[2] == password_text.Text)
                    {
                        data.Close();
                        mainInterface maininterface = new mainInterface(mainWindow, int.Parse(data_array[0]));
                        mainWindow.OpenPage(MainWindow.pages.maininterface);
                    }
                }
            }
        }

        private void sign_inBTN_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.regin);
        }
    }
}
