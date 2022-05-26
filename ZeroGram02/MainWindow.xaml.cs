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
            OpenPage(pages.login, 0);
        }

        public enum pages
        {
            login,
            regin,
            maininterface,
            user_info
        }

        public void OpenPage(pages pages, int id)
        {
            if (pages == pages.login)
            {
                frame.Navigate(new Login(this));
            }
            else if (pages == pages.regin)
            {
                frame.Navigate(new Regin(this));
            }
            else if (pages == pages.maininterface)
            {
                frame.Navigate(new mainInterface(this, id));
            }
            else if (pages == pages.user_info)
                frame.Navigate(new User_Info(this, id));

        }
    }
}
