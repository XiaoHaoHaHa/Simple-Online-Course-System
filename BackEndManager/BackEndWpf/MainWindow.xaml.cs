using CoreLib.Model;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackEndWpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.userTitle.Text = ((App)Application.Current).AdminData.Name;
        }

        #region For All Window Controll
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                this.btnMaxControll.Icon = FontAwesomeIcon.WindowMaximize;
            }
            else
            {
                this.btnMaxControll.Icon = FontAwesomeIcon.WindowRestore;
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnMinimize_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int clickTimes = e.ClickCount;
            if (clickTimes == 2)
            {
                if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    WindowState = WindowState.Normal;
                }
            }
        }
        #endregion

        //The Following Evevnt Is For Main Function
        private void changePwd_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("ChangPasswordPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).AdminData = null;
            WindowLogin windowLogin = new WindowLogin();
            windowLogin.Show();
            this.Close();
        }

        private void adminManage_Checked(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("AdminQueryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void adminManage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("AdminQueryPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void studentManage_Checked(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("StudentManagePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void studentManage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("StudentManagePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void teacherManage_Checked(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("TeacherPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void teacherManage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("TeacherPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void classManage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("ClassPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void classManage_Checked(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("ClassPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void courseManage_Checked(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("CoursePage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void courseManage_Click(object sender, RoutedEventArgs e)
        {
            this.Main.Navigate(new System.Uri("CoursePage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
