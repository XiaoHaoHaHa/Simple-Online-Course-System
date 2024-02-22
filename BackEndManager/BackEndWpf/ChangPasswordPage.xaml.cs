using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreLib.Model;
using CoreLib.Service;
using CoreLib.DataBase;

namespace BackEndWpf
{
    /// <summary>
    /// ChangPasswordPage.xaml 的互動邏輯
    /// </summary>
    public partial class ChangPasswordPage : Page
    {
        private AdminModel _adminData = ((App)Application.Current).AdminData;
        private string _connectionString = ((App)Application.Current).ConnectionString;
        public ChangPasswordPage()
        {
            InitializeComponent();
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.NewPassword.Password))
            {
                MessageBox.Show("The new password must have values");
                return;
            }

            if (this.NewPassword.Password != this.CheckPassword.Password)
            {
                MessageBox.Show("The password and check password are not consistant");
                return;
            }

            var originHashPwd = HashService.PwdSHA256Demo(this.OriginPassword.Password, _adminData.Id);
            if (originHashPwd != _adminData.Password)
            {
                MessageBox.Show("Wrong password");
                return;
            }

            var newHashPwd = HashService.PwdSHA256Demo(this.NewPassword.Password, _adminData.Id);
            var dataBase = new AdminRepository(_connectionString);
            dataBase.UpdateAdminData(_adminData.Id, newHashPwd);
            _adminData.Password = newHashPwd;
            MessageBox.Show("Change success!");
        }
    }
}
