using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для mainInterface.xaml
    /// </summary>
    public partial class mainInterface : Page
    {
        public MainWindow mainWindow;
        public int ID;
        public mainInterface(MainWindow _mainWindow, int id)
        {
            mainWindow = _mainWindow;
            InitializeComponent();
            Coin.Height += 100;
            ID = id;
            ZeroTwoDancing.Play();
            Coin.Play();
            Sec_damage();
        }

        async public void Sec_damage()
        {
            while (true)
            {
                await Task.Delay(1000);
                if (hp.Value - 10 > 0) hp.Value -= 10;
                else if (hp.Value - 10 <= 0)
                {
                    hp.Value = 100;
                    coin_count.Content = Convert.ToInt32(coin_count.Content) + 1;
                    xp.Value += 50;
                    if (xp.Value == 100)
                    {
                        string[] array = level.Content.ToString().Split(' ');
                        level.Content = array[0] + " " + (Convert.ToInt32(array[1]) + 1);
                        xp.Value = 0;
                    }
                }

            }
        }

        private void UserMenu_Click(object sender, RoutedEventArgs e)
        {
            //User_Info user_Info = new User_Info(mainWindow, ID);
            ZeroTwoDancing.Close();
            mainWindow.OpenPage(MainWindow.pages.user_info, ID);


        }

        private void Mob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (hp.Value - 50 > 0) hp.Value -= 50;
            else if (hp.Value - 50 <= 0)
            {
                hp.Value = 100;
                coin_count.Content = Convert.ToInt32(coin_count.Content) + 1;
                xp.Value += 50;
                if (xp.Value == 100)
                {
                    string[] array = level.Content.ToString().Split(' ');
                    level.Content = array[0] + " " + (Convert.ToInt32(array[1]) + 1);
                    xp.Value = 0;
                }
            }
        }

        private void ZeroTwoDancing_MediaEnded(object sender, RoutedEventArgs e)
        {
            ZeroTwoDancing.Position = TimeSpan.Zero;
            ZeroTwoDancing.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] array = kirby_lv.Text.Split(' ');
            array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
            kirby_lv.Text = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i != 2) kirby_lv.Text += array[i] + " ";
                else kirby_lv.Text += array[i];
            }
            kirby_btn.Content = "UP";
        }

        private void Coin_MediaEnded(object sender, RoutedEventArgs e)
        {
            Coin.Position = TimeSpan.Zero;
            Coin.Play();
        }
    }
}
