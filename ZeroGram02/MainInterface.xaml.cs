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
        public string tempPath = path + ".tmp";
        public string[] data_array;
        public StreamReader sr = new StreamReader(path);
        public int user_dmg;
        public int max_hp;
        public string imagePath;
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

                        kirby_lv.Text = "KIRBY LV: " + array[6];
                        if (int.Parse(array[6]) >= 1)
                        {
                            Sec_damage("kirby_btn");
                            kirby_dmg.Text = "DPS:" + UnitDamage(kirby_btn.Name, int.Parse(array[6]));
                        }
                        else kirby_dmg.Text = "???";
                        int cost = 50 + (int)(100 * 1.2 * int.Parse(array[6]));
                        kirby_cost.Text = cost.ToString();

                        haruko_lv.Text = "HARUKO LV: " + array[7];
                        if (int.Parse(array[7]) >= 1)
                        {
                            Sec_damage("haruko_btn");
                            haruko_dmg.Text = "DPS:" + UnitDamage(haruko_btn.Name, int.Parse(array[7]));
                        }
                        else haruko_dmg.Text = "???";
                        cost = 100 + (int)(250 * 1.5 * int.Parse(array[7]));
                        haruko_cost.Text = cost.ToString();

                        jiraiya_lv.Text = "JIRAIYA LV: " + array[8];
                        if (int.Parse(array[8]) >= 1)
                        {
                            Sec_damage("jiraiya_btn");
                            jiraiya_dmg.Text = "DPS:" + UnitDamage(jiraiya_btn.Name, int.Parse(array[8]));
                        }
                        else jiraiya_dmg.Text = "???";
                        cost = 150 + (int)(500 * 1.7 * int.Parse(array[8]));
                        jiraiya_cost.Text = cost.ToString();

                        jojo_lv.Text = "JOHNNY LV: " + array[9];
                        if (int.Parse(array[9]) >= 1)
                        {
                            Sec_damage("jojo_btn");
                            jojo_dmg.Text = "DPS:" + UnitDamage(jojo_btn.Name, int.Parse(array[9]));
                        }
                        else jojo_dmg.Text = "???";
                        cost = 200 + (int)(1000 * 1.9 * int.Parse(array[8]));
                        jojo_cost.Text = cost.ToString();

                        sonic_lv.Text = "SANIC LV: " + array[10];
                        if (int.Parse(array[10]) >= 1)
                        {
                            Sec_damage("sonic_btn");
                            sonic_dmg.Text = "DPS:" + UnitDamage(sonic_btn.Name, int.Parse(array[10]));
                        }
                        else sonic_dmg.Text = "???";
                        cost = 250 + (int)(2500 * 2.1 * int.Parse(array[10]));
                        sonic_cost.Text = cost.ToString();

                        pochita_lv.Text = "POCHITA LV: " + array[11];
                        if (int.Parse(array[11]) >= 1)
                        {
                            Sec_damage("pochita_btn");
                            pochita_dmg.Text = "DPS:" + UnitDamage(pochita_btn.Name, int.Parse(array[11]));
                        }
                        else pochita_dmg.Text = "???";
                        cost = 3000 + (int)(3000 * 2.3 * int.Parse(array[11]));
                        sonic_cost.Text = cost.ToString();

                        click_lv.Text = "CLICK LV: " + array[5];
                        user_dmg = UnitDamage("click_btn", int.Parse(array[5]));
                        cost = 50 + (int)(10 * 1.8 * int.Parse(array[5]));
                        click_cost.Text = cost.ToString();
                        click_dmg.Text = "DMG:" + user_dmg;

                        xp.Value = Convert.ToInt32(array[12]);
                        max_hp = 100 + Convert.ToInt32(array[4]) * 50;
                    }
                    line = sr.ReadLine();
                }
            }
            hp.Maximum = max_hp;
            hp.Value = max_hp;
            ZeroTwoDancing.Play();
        }
        public int UnitDamage(string name, int unitLevel)
        {
            if (unitLevel == 1) unitLevel = 0;
            switch (name)
            {
                case "click_btn":
                    return 15 + (6 * unitLevel / 2);
                case "kirby_btn":
                    return 10 + (7 * unitLevel / 2);
                case "haruko_btn":
                    return 50 + (30 * unitLevel / 2);
                case "jiraiya_btn":
                    return 200 + (100 * unitLevel / 2);
                case "jojo_btn":
                    return 500 + (200 * unitLevel / 2);
                case "sonic_btn":
                    return 1000 + (450 * unitLevel / 2);
                case "pochita_btn":
                    return 10000 + (4500 * unitLevel / 2);
            }
            return 0;
        }
        async public void Sec_damage(string name)
        {
            int lvl = 0;
            List<string> list_lvl = new List<string>();
            while (true)
            {
                switch (name)
                {
                    case "kirby_btn":
                        list_lvl = kirby_lv.Text.Split(' ').ToList();
                        break;
                    case "haruko_btn":
                        list_lvl = haruko_lv.Text.Split(' ').ToList();
                        break;
                    case "jiraiya_btn":
                        list_lvl = jiraiya_lv.Text.Split(' ').ToList();
                        break;
                    case "jojo_btn":
                        list_lvl = jojo_lv.Text.Split(' ').ToList();
                        break;
                    case "sonic_btn":
                        list_lvl = sonic_lv.Text.Split(' ').ToList();
                        break;
                    case "pochita_btn":
                        list_lvl = pochita_lv.Text.Split(' ').ToList();
                        break;
                }
                lvl = Convert.ToInt32(list_lvl[2]);
                int unitDamage = 0;
                unitDamage = UnitDamage(name, lvl);

                await Task.Delay(1000);
                if (hp.Value - unitDamage > 0) hp.Value -= unitDamage;
                else if (hp.Value - unitDamage <= 0)
                {
                    string[] userLevel = level.Content.ToString().Split(' ').ToArray();
                    coin_count.Content = Convert.ToInt32(coin_count.Content) + 1 + 1 * (int.Parse(userLevel[1]) / 5);
                    xp.Value += 50;
                    Update_data();
                }
            }
        }

        void Update_data()
        {
            Random random = new Random();
            string[] allfiles = Directory.GetFiles(Path.GetFullPath(@"..\\..\\data\img mobs"));
            int r = random.Next(0, allfiles.Length);
            Mob.Source = new BitmapImage(new Uri(allfiles[r], UriKind.Absolute));
            if (xp.Value == 100)
            {
                string[] array = level.Content.ToString().Split(' ');
                level.Content = array[0] + " " + (Convert.ToInt32(array[1]) + 1);
                max_hp = 100 + (Convert.ToInt32(array[1]) + 1) * 50;
                hp.Maximum = max_hp;
                xp.Value = 0;
            }
            hp.Value = max_hp;
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
                            if (i == 12) writingLine += xp.Value + ";";
                            else if (i == 3) writingLine += coin_count.Content + ";";
                            else if (i == 4) writingLine += level.Content.ToString().Substring(4, level.Content.ToString().Length - 4) + ";";
                            else if (i == data_array.Length - 1) writingLine += data_array[i];
                            else writingLine += data_array[i] + ";";
                        }
                        sw.WriteLine(writingLine);
                    }
                    else
                        sw.WriteLine(line);
                    line = sr.ReadLine();
                }
            }
            File.Delete(path);
            File.Move(tempPath, path);
        }

        private void UserMenu_Click(object sender, RoutedEventArgs e)
        {
            ZeroTwoDancing.Close();
            mainWindow.OpenPage(MainWindow.pages.user_info, ID);
        }

        private void Mob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (hp.Value - user_dmg > 0) hp.Value -= user_dmg;
            else if (hp.Value - user_dmg <= 0)
            {
                string[] userLevel = level.Content.ToString().Split(' ').ToArray();
                coin_count.Content = Convert.ToInt32(coin_count.Content) + 1 + 1 * (int.Parse(userLevel[1]) / 5);
                xp.Value += 50;
                Update_data();
            }

        }

        private void ZeroTwoDancing_MediaEnded(object sender, RoutedEventArgs e)
        {
            ZeroTwoDancing.Position = new TimeSpan(0, 0, 1);
            ZeroTwoDancing.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cost = 0;
            List<string> list_lvl = new List<string>();
            string buttonName = ((Button)sender).Name;
            TextBlock textBlock = null;
            int index = 0;
            switch (buttonName)
            {
                case "click_btn":
                    list_lvl = click_lv.Text.Split(' ').ToList();
                    cost = 50 + (int)(10 * 1.8 * int.Parse(list_lvl[2]));
                    click_cost.Text = cost.ToString();
                    textBlock = click_lv;
                    index = 5;
                    break;
                case "kirby_btn":
                    list_lvl = kirby_lv.Text.Split(' ').ToList();
                    cost =  50 + (int)(100 * 1.2 * int.Parse(list_lvl[2]));
                    kirby_cost.Text = cost.ToString();
                    textBlock = kirby_lv;
                    index = 6;
                    break;
                case "haruko_btn":
                    list_lvl = haruko_lv.Text.Split(' ').ToList();
                    cost = 100 + (int)(250 * 1.5 * int.Parse(list_lvl[2]));
                    haruko_cost.Text = cost.ToString();
                    textBlock = haruko_lv;
                    index = 7;
                    break;
                case "jiraiya_btn":
                    list_lvl = jiraiya_lv.Text.Split(' ').ToList();
                    cost = 150 + (int)(500 * 1.7 * int.Parse(list_lvl[2]));
                    jiraiya_cost.Text = cost.ToString();
                    textBlock = jiraiya_lv;
                    index = 8;
                    break;
                case "jojo_btn":
                    list_lvl = jojo_lv.Text.Split(' ').ToList();
                    cost = 200 + (int)(1000 * 1.9 * int.Parse(list_lvl[2]));
                    jojo_cost.Text = cost.ToString();
                    textBlock = jojo_lv;
                    index = 9;
                    break;
                case "sonic_btn":
                    list_lvl = sonic_lv.Text.Split(' ').ToList();
                    cost = 250 + (int)(2500 * 2.1 * int.Parse(list_lvl[2]));
                    sonic_cost.Text = cost.ToString();
                    textBlock = sonic_lv;
                    index = 10;
                    break;
                case "pochita_btn":
                    list_lvl = pochita_lv.Text.Split(' ').ToList();
                    cost = 3000 + (int)(3000 * 2.3 * int.Parse(list_lvl[2]));
                    sonic_cost.Text = cost.ToString();
                    textBlock = pochita_lv;
                    index = 11;
                    break;
            }

            if (int.Parse(coin_count.Content.ToString()) >= cost)
            {
                string[] array = textBlock.Text.Split(' ');
                array[2] = (Convert.ToInt32(array[2]) + 1).ToString();

                textBlock.Text = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i != 2) textBlock.Text += array[i] + " ";
                    else textBlock.Text += array[i];
                }
                coin_count.Content = int.Parse(coin_count.Content.ToString()) - cost;
                int level_unit = int.Parse(array[2]);
                switch (buttonName)
                {
                    case "click_btn":
                        cost = 50 + (int)(10 * 1.8 * int.Parse(array[2]));
                        click_cost.Text = cost.ToString();
                        click_dmg.Text = "DMG:" + UnitDamage(buttonName, level_unit);
                        user_dmg  = UnitDamage(buttonName, level_unit);
                        break;
                    case "kirby_btn":
                        cost = 50 + (int)(100 * 1.2 * int.Parse(array[2]));
                        kirby_cost.Text = cost.ToString();
                        kirby_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                    case "haruko_btn":
                        cost = 100 + (int)(250 * 1.5 * int.Parse(array[2]));
                        haruko_cost.Text = cost.ToString();
                        haruko_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                    case "jiraiya_btn":
                        cost = 150 + (int)(500 * 1.7 * int.Parse(array[2]));
                        jiraiya_cost.Text = cost.ToString();
                        jiraiya_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                    case "jojo_btn":
                        cost = 200 + (int)(1000 * 1.9 * int.Parse(array[2]));
                        jojo_cost.Text = cost.ToString();
                        jojo_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                    case "sonic_btn":
                        cost = 250 + (int)(2500 * 2.1 * int.Parse(array[2]));
                        sonic_cost.Text = cost.ToString();
                        sonic_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                    case "pochita_btn":
                        cost = 3000 + (int)(3000 * 2.3 * int.Parse(array[2]));
                        sonic_cost.Text = cost.ToString();
                        pochita_dmg.Text = "DPS:" + UnitDamage(buttonName, level_unit);
                        break;
                }
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
                                if (i == 3) writingLine += coin_count.Content + ";";
                                else if (i == index) writingLine += level_unit + ";";
                                else if (i == data_array.Length - 1) writingLine += data_array[i];
                                else writingLine += data_array[i] + ";";
                            }
                            sw.WriteLine(writingLine);
                        }
                        else
                            sw.WriteLine(line);
                        line = sr.ReadLine();
                    }
                }
                File.Delete(path);
                File.Move(tempPath, path);

                if (level_unit == 1 && textBlock.Name != "click_btn")
                {
                    Sec_damage(buttonName);
                }
            }
        }

    }
}
