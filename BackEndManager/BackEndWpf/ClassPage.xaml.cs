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
    /// ClassPage.xaml 的互動邏輯
    /// </summary>
    public partial class ClassPage : Page
    {
        private AdminModel _adminData = ((App)Application.Current).AdminData;
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private ClassModel _classList;

        public ClassPage()
        {
            InitializeComponent();
        }

        private void ClassList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ClassList.SelectedItem != null)
            {
                this._classList = (ClassModel)this.ClassList.SelectedItem;
                this.Detail.DataContext = this._classList;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new ClassRepository(_connectionString);
            var result = dataBase.GetClassData(this.course.Text, this.teacher.Text);
            this.ClassList.ItemsSource = result;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var dataBase = new ClassRepository(_connectionString);
            if (this._classList == null)
                return;

            //Here for simulate that 

            dataBase.DeleteClass(this._classList);
            MessageBox.Show("The class has been deleted!");
            var result = dataBase.GetClassData(this.course.Text, this.teacher.Text);
            this.ClassList.ItemsSource = result;
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            var dataBase = new ClassRepository(_connectionString);

            if (this._classList == null)
                return;

            if (string.IsNullOrEmpty(this._classList.SDate.ToString()))
            {
                MessageBox.Show("Start Day is empty!");
                return;
            }

            if (string.IsNullOrEmpty(this._classList.EDate.ToString()))
            {
                MessageBox.Show("End Day is empty!");
                return;
            }

            if (string.IsNullOrEmpty(this._classList.Location))
            {
                MessageBox.Show("Location is empty!");
                return;
            }

            if (this._classList.SDate > this._classList.EDate)
            {
                MessageBox.Show("The Start Day must be ahead of End Day");
                return;
            }

            dataBase.UpdateClassData(this._classList);
            MessageBox.Show("Modified!");
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var addClass = new AddClassWindow();
            addClass.ShowDialog();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this._classList == null)
                return;
            this.NavigationService.Navigate(new DescriptPage(this, this._classList));
        }
    }
}
