using Microsoft.Win32;
using System;
using System.IO;
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
using System.Windows.Shapes;

namespace ZeroGram02
{
    /// <summary>
    /// Логика взаимодействия для User_Info.xaml
    /// </summary>
    public partial class User_Info : Page
    {
        public MainWindow mainWindow;
        public int ID;
        public StreamReader sr;
        public string path = System.IO.Path.GetFullPath(@"..\\..\\data\data.txt");
        public string[] data_array;
        public User_Info(MainWindow _mainWindow, int id)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            ID = id;
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] data_array = line.Split(';');
                    if (int.Parse(data_array[0]) == ID)
                    {
                        Login.Text = data_array[1];
                        CurrentPassword.Text = data_array[2];
                    }
                    line = sr.ReadLine();
                }

            }
        }
        public void IDDefine(int id) => ID = id;

        private void loadPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите изображение";
            openFileDialog.Filter = "Графические изображения|*.jpg;*.jpeg;*.png|" +
        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
                UserImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
        }

        private void EditLogin_Click(object sender, RoutedEventArgs e)
        {
            Login.IsReadOnly = false;
        }

        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            CurrentPassword.IsReadOnly = false;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
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
                        string writingLine = data_array[0] + ";" + Login.Text + ";" + CurrentPassword.Text + ";" + data_array[3] + ";" + data_array[4];
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

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Login.IsReadOnly = true;
        }

        private void CurrentPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            CurrentPassword.IsReadOnly = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.maininterface, ID);
        }
    }
}
