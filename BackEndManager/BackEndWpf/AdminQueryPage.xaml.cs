using CoreLib.DataBase;
using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BackEndWpf
{
    /// <summary>
    /// AdminQueryPage.xaml 的互動邏輯
    /// </summary>
    public partial class AdminQueryPage : Page
    {
        private AdminModel _adminData = ((App)Application.Current).AdminData;
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private AdminModel _adminSelected;

        public AdminQueryPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new AdminRepository(_connectionString);
            var result = dataBase.AdminQuery(this.Email.Text, this.Name.Text);
            this.AdminList.ItemsSource = result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var addAdminWn = new AddAdminWindow();
            addAdminWn.ShowDialog();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (this._adminData.Priority < this._adminSelected.Priority)
            {
                MessageBox.Show("Cannot modify, you don't have the privilege");
                return;
            }

            var dataBase = new AdminRepository(_connectionString);
            if (this._adminSelected == null)
                return;
            dataBase.UpdateAdminData(this._adminSelected);
            MessageBox.Show("Modified!");
        }

        private void AdminList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.AdminList.SelectedItem != null)
            {
                this._adminSelected = (AdminModel)this.AdminList.SelectedItem;
                this.Detail.DataContext = this._adminSelected;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new AdminRepository(_connectionString);
            if (this._adminSelected == null)
                return;

            //Here for simulate that highest admin cannot be deleted!
            if (this._adminData.Priority < this._adminSelected.Priority)
            {
                MessageBox.Show("Cannot Delete, you don't have the privilege");
                return;
            }

            if(this._adminSelected.Id == ((App)Application.Current).AdminData.Id)
            {
                MessageBox.Show("Cannot Delete Yourself!");
                return;
            }
            dataBase.DeleteAdmin(this._adminSelected);
            MessageBox.Show("The data has been deleted!");
            var result = dataBase.AdminQuery(this.Email.Text, this.Name.Text);
            this.AdminList.ItemsSource = result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
