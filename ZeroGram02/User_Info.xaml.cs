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
        public User_Info(MainWindow _mainWindow, int id)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            ID = id;
            StreamReader data = new StreamReader(path);
            using (sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] data_array = line.Split(';');
                    if (int.Parse(data_array[0]) == ID)
                    {
                        Login.Text = data_array[1];
                        CurrentPassword.Text = data_array[2];
                    }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    string[] data_array = line.Split(';');
                    if (int.Parse(data_array[0]) == ID)
                    {
                        data_array[1] = Login.Text;
                        data_array[2] = CurrentPassword.Text;
                    }
                }
            }
        }

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            Login.IsReadOnly = true;
        }

        private void CurrentPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            CurrentPassword.IsReadOnly = true;
        }
    }
}
