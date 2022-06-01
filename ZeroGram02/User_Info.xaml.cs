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
using System.Drawing;
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
        public string imagePath;
        public bool isPicLoad;
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
                        try
                        {
                            string imagepath = System.IO.Path.GetFullPath(data_array[13]);
                            UserImage.Source = BitmapFromUri(new Uri(imagepath));
                        }
                        catch (Exception) { }
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
            {
                imagePath = openFileDialog.FileName;
                UserImage.Source = BitmapFromUri(new Uri(imagePath));
            }
            isPicLoad = true;
        }
        public static ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
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
            string pathTo = @"..\..\data\avatars\" + ID + "_ava.png";
            if (!File.Exists(pathTo))
            {
                File.Copy(imagePath, pathTo);
            }
            else
            {
                File.Delete(pathTo);
                File.Copy(imagePath, pathTo);
            }

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
                        string writingLine = data_array[0] + ";" + Login.Text + ";" + CurrentPassword.Text;
                        for (int i = 3; i <= data_array.Length; i++)
                        {
                            if (i == 14 && isPicLoad == true)
                            {
                                writingLine += ";" + pathTo;
                            }
                            else
                            {
                                writingLine += ";" + data_array[i];
                            }

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

        private void Login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "") Login.Text = "Login";
            Login.IsReadOnly = true;
        }

        private void CurrentPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CurrentPassword.Text == "") CurrentPassword.Text = "Password";
            CurrentPassword.IsReadOnly = true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.maininterface, ID);
            GC.Collect();
        }

        private void CurrentPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CurrentPassword.Text == "Password") CurrentPassword.Text = "";
        }

        private void Login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "Login") Login.Text = "";
        }
    }
}
