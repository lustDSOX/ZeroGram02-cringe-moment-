using System;
using System.Windows;
using System.Windows.Interop;

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
            SourceInitialized += Window1_SourceInitialized;
        }
        private void Window1_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(WndProc);
        }

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            switch (msg)
            {
                case WM_SYSCOMMAND:
                    int command = wParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                    {
                        handled = true;
                    }
                    break;
                default:
                    break;
            }
            return IntPtr.Zero;
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
            {
                frame.Navigate(new User_Info(this, id));
            }
        }
    }
}
