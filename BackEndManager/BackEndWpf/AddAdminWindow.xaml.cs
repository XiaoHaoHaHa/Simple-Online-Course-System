using CoreLib.DataBase;
using CoreLib.Model;
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
using System.Windows.Shapes;

namespace BackEndWpf
{
    /// <summary>
    /// AddAdminWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddAdminWindow : Window
    {
        private string _connection;
        private AdminRepository _adminRepo;
        private AdminModel _dataContext;

        public AddAdminWindow()
        {          
            InitializeComponent();
            _connection = ((App)Application.Current).ConnectionString;
            _adminRepo = new AdminRepository(_connection);
            _dataContext = new AdminModel();
            this.grid.DataContext = _dataContext;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var result = _adminRepo.AdminQuery(_dataContext.Email, null);
            if (result.Count == 0)
            {
                _adminRepo.AddAdmin(_dataContext);
                MessageBox.Show("Add Success!");
                this.Close();
            }
            else
            {
                MessageBox.Show("The account has been registered");
            }
        }
    }
}
