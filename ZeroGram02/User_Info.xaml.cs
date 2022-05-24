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
        public User_Info(MainWindow _mainWindow, int id)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            foreach (var label in Layout.Children.OfType<Label>())
            {
                label.Foreground = Brushes.White;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
            var path = System.IO.Path.GetFullPath(@"..\\..\\data\data.txt");
            StreamReader data = new StreamReader(path);
            using (StreamReader sr = new StreamReader(path))
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

    }
}
