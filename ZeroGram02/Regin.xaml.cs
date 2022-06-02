using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        public MainWindow mainWindow;
        public Regin(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
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

        private void Confirm_password_text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (confirm_password_text.Text == "")
            {
                confirm_password_text.Text = "Confirm password";
            }
        }

        private void Confirm_password_text_GotFocus(object sender, RoutedEventArgs e)
        {
            if (confirm_password_text.Text == "Confirm password")
            {
                confirm_password_text.Text = "";
            }
        }


        private void sign_inBTN_Click(object sender, RoutedEventArgs e)
        {
            if (password_text.Text == confirm_password_text.Text)
            {
                string path = System.IO.Path.GetFullPath(@"..\\..\\data\data.txt");
                bool check = true;
                int count = 1;
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadLine();
                    while (line != null && line != "")
                    {
                        string[] data_array = line.Split(';');
                        if (data_array[1] == login_text.Text)
                        {
                            already_exist.Content = "Такой пользователь уже существует";
                            check = false;
                            sr.Close();
                            break;
                        }
                        else
                        {
                            count++;
                            line = sr.ReadLine();
                        }
                    }
                }
                if (check == true)
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine($"{count++};{login_text.Text};{password_text.Text};0;1;1;0;0;0;0;0;0;0;0");
                        sw.Close();
                    }
                    mainWindow.OpenPage(MainWindow.pages.login, count);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
            else { already_exist.Content = "Пароли не совпадают"; }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.login, 0);
            GC.Collect();
        }
    }
}
