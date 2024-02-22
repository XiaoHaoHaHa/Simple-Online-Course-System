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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackEndWpf
{
    /// <summary>
    /// DescriptPage.xaml 的互動邏輯
    /// </summary>
    public partial class DescriptPage : Page
    {
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private Page _lastPage;
        private ClassModel _dataContext;

        public DescriptPage(Page lastPage, ClassModel classModel)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(classModel.Description))
            {
                classModel.Description = "No Description";
            }
            _dataContext = classModel;
            this.descriptPanel.DataContext = this._dataContext;
            _lastPage = lastPage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(_lastPage);
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new ClassRepository(_connectionString);
            _dataContext.Description = this.modifyDes.Text;
            this.des.Text = this.modifyDes.Text;
            dataBase.UpdateClassData(_dataContext);
            MessageBox.Show("Description Updated");
        }
    }
}
