using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using CoreLib.DataBase;
using CoreLib.Service;

namespace BackEndWpf
{
    /// <summary>
    /// WindowLogin.xaml 的互動邏輯
    /// </summary>
    public partial class WindowLogin : Window
    {
        private string _connection;
        private AdminRepository _adminRepo;
        public WindowLogin()
        {
            _connection = ((App)Application.Current).ConnectionString;
            _adminRepo = new AdminRepository(_connection);
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginProcess();
        }

        private void LoginProcess()
        {
            if (!string.IsNullOrEmpty(this.txtUser.Text) && !string.IsNullOrEmpty(this.txtPass.Password))
            {
                var adminData = _adminRepo.GetAdminData(this.txtUser.Text);

                if (adminData == null)
                {
                    MessageBox.Show("Wrong Email Or Password!");
                    return;
                }

                string pwdHash = HashService.PwdSHA256Demo(this.txtPass.Password, adminData.Id);
                if (pwdHash != adminData.Password)
                {
                    MessageBox.Show("Wrong Email Or Password!");
                    return;
                }

                //Save admin data to global (app.xaml.cs)
                ((App)Application.Current).AdminData = adminData;

                var window = new MainWindow();
                window.Show();
                this.Close();
                return;
            }
            else
                MessageBox.Show("Please Enter Email And Password!");
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginProcess();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoginProcess();
        }
    }
}
