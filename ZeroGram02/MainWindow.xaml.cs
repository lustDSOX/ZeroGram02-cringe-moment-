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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class User
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public int ID { get; set; }
            public string Email { get; set; }

            public User(string firstName, string lastName, int id, string email)
            {
                
            }
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
            ZeroGramMain zeroGram = new ZeroGramMain();
            zeroGram.Show();
            this.Close();
        }
    }
}
