using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void login_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (login_text.Text == "Login")
            {
                login_text.Text = "";
            }
        }

        private void login_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (login_text.Text == "")
            {
                login_text.Text = "Login";
            }
        }

        private void password_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password_text.Text == "Password")
            {
                password_text.Text = "";
            }
        }

        private void password_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (password_text.Text == "")
            {
                password_text.Text = "Password";
            }
        }

        private void log_inBTN_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetFullPath(@"..\\..\\data\data.txt");
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] data_array = line.Split(';');
                    if (data_array[1] == login_text.Text && data_array[2] == password_text.Text)
                    {
                        sr.Close();
                        ID = int.Parse(data_array[0]);
                        mainWindow.OpenPage(MainWindow.pages.maininterface, ID);
                        break;
                    }
                    line = sr.ReadLine();
                }
            }
        }

        private void sign_inBTN_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.regin, 0);
            GC.Collect();
        }

        private void login_text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                log_inBTN_Click(this, null);
            }
        }
    }
}
