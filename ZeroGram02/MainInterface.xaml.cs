using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для mainInterface.xaml
    /// </summary>
    public partial class mainInterface : Page
    {
        public MainWindow mainWindow;
        public int ID;
        public static string path = Path.GetFullPath(@"..\\..\\data\data.txt");
        public string[] data_array;
        public StreamReader sr = new StreamReader(path);
        public int click_dmg;
        public int max_hp;
        public mainInterface(MainWindow _mainWindow, int id)
        {
            mainWindow = _mainWindow;
            InitializeComponent();
            Coin.Height += 100;
            ID = id;
            using (sr)
            {
                string line = sr.ReadLine();
                while (line != null && line != "")
                {
                    string[] array = line.Split(';');
                    if (Convert.ToInt32(array[0]) == ID)
                    {
                        coin_count.Content = Convert.ToInt32(array[3]);
                        level.Content = "LV: " + array[4];
                        kirby_lv.Text = "KIRBY LV: " + array[5];
                        haruko_lv.Text = "HARUKO LV: " + array[6];
                        jiraiya_lv.Text = "JIRAIYA LV: " + array[7];
                        jojo_lv.Text = "JOHNNY LV: " + array[8];
                        sonic_lv.Text = "SANIC LV: " + array[9];
                        pochita_lv.Text = "POCHITA LV: " + array[10];
                        xp.Value = Convert.ToInt32(array[11]);
                        max_hp = 100 + Convert.ToInt32(array[4]) * 50;
                    }
                    line = sr.ReadLine();
                }
            }
            hp.Maximum = max_hp;
            hp.Value = max_hp;
            ZeroTwoDancing.Play();
        }
        public double UnitDamage(string name, int unitLevel)
        {
            switch (name)
            {
                case "kirby_btn":
                    return 10;
                case "haruko_btn":
                    return 50;
                case "jiraiya_btn":
                    return 200;
                case "jojo_btn":
                    return 500;
                case "sonic_btn":
                    return 1000;
                case "pochita_btn":
                    return 10000;
            }
            return 0;
        }
        async public void Sec_damage(double unitDamage)
        {
            while (true)
            {
                await Task.Delay(1000);
                if (hp.Value - unitDamage > 0) hp.Value -= unitDamage;
                else if (hp.Value - unitDamage <= 0)
                {
                    hp.Value = 100;
                    coin_count.Content = Convert.ToInt32(coin_count.Content) + 1;
                    xp.Value += 50;
                    if (xp.Value == 100)
                    {
                        string[] array = level.Content.ToString().Split(' ');
                        level.Content = array[0] + " " + (Convert.ToInt32(array[1]) + 1);
                        xp.Value = 0;
                        Update_data();
                    }
                }
            }
        }

        void Update_data()
        {
            Random random = new Random();
            string[] allfiles = Directory.GetFiles(Path.GetFullPath(@"..\\..\\data\img mobs"));
            int r = random.Next(0, allfiles.Length);
            Mob.Source = new BitmapImage(new Uri(allfiles[r], UriKind.Absolute));
            string[] array = level.Content.ToString().Split(' ');
            level.Content = array[0] + " " + (Convert.ToInt32(array[1]) + 1);
            xp.Value = 0;
            string tempPath = path + ".tmp";
            using (sr = new StreamReader(path))
            using (StreamWriter sw = new StreamWriter(tempPath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] data_array = line.Split(';');
                    if (int.Parse(data_array[0]) == ID)
                    {
                        string writingLine = "";
                        for (int i = 0; i < data_array.Length; i++)
                        {
                            if (i == 11) writingLine += xp.Value;
                            else if (i == 3) writingLine += coin_count.Content + ";";
                            else if (i == 4) writingLine += level.Content.ToString().Substring(4, level.Content.ToString().Length - 4) + ";";
                            else writingLine += data_array[i] + ";";
                        }
                        sw.WriteLine(writingLine);
                    }
                    else
                        sw.WriteLine(line);
                    line = sr.ReadLine();
                }
            }
            sr.Close();
            using (StreamWriter sw = new StreamWriter(path))
            using (StreamReader srTemp = new StreamReader(tempPath))
            {
                string lineTemp = srTemp.ReadLine();
                while (lineTemp != null)
                {
                    string[] data_array = lineTemp.Split(';');
                    sw.WriteLine(lineTemp);
                    lineTemp = srTemp.ReadLine();
                }
            }
            hp.Maximum += 50;
            max_hp += 50;
        }

        private void UserMenu_Click(object sender, RoutedEventArgs e)
        {
            ZeroTwoDancing.Close();
            mainWindow.OpenPage(MainWindow.pages.user_info, ID);
        }

        private void Mob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (hp.Value - click_dmg > 0) hp.Value -= click_dmg;
            else if (hp.Value - click_dmg <= 0)
            {
                hp.Value = max_hp;
                coin_count.Content = Convert.ToInt32(coin_count.Content) + 1;
                xp.Value += 10;
                if (xp.Value == 100)
                {
                    Update_data();
                }
            }

        }

        private void ZeroTwoDancing_MediaEnded(object sender, RoutedEventArgs e)
        {
            ZeroTwoDancing.Position = new TimeSpan(0, 0, 1);
            ZeroTwoDancing.Play();
        }

        private void Kirby_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 50)
            {
                string[] array = kirby_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                kirby_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) kirby_lv.Text += array[i] + " ";
                    else kirby_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 50;
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(kirby_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
        private void Haruko_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 100)
            {
                string[] array = haruko_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                haruko_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) haruko_lv.Text += array[i] + " ";
                    else haruko_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 100;
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(haruko_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
        private void Jiraiya_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 150)
            {
                string[] array = jiraiya_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                jiraiya_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) jiraiya_lv.Text += array[i] + " ";
                    else jiraiya_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 150;
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(jiraiya_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
        private void Johnny_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 200)
            {
                string[] array = jojo_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                jojo_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) jojo_lv.Text += array[i] + " ";
                    else jojo_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 200;
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(jojo_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
        private void Sonic_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 250)
            {
                string[] array = sonic_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                sonic_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) sonic_lv.Text += array[i] + " ";
                    else sonic_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 250; 
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(sonic_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
        private void Pochita_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.Parse(coin_count.Content.ToString()) >= 300)
            {
                string[] array = pochita_lv.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();
                pochita_lv.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) pochita_lv.Text += array[i] + " ";
                    else pochita_lv.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - 300;
                int level = int.Parse(array[2]);
                double unitDamage = UnitDamage(pochita_btn.Name, level);
                Sec_damage(unitDamage);
            }
        }
    }
}
