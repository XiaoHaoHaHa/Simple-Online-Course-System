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
    /// StudentManagePage.xaml 的互動邏輯
    /// </summary>
    public partial class StudentManagePage : Page
    {
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private StudentModel _stuSelected;

        public StudentManagePage()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_stuSelected != null)
            {
                var result = MessageBox.Show("Are you sure to delete this student?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result.ToString() == "Yes")
                {
                    var dataBase = new StudentRepository(_connectionString);
                    dataBase.DeleteStudent(this._stuSelected);
                    MessageBox.Show("The student has been deleted");

                    var result2 = dataBase.StudentQuery(this.Email.Text, this.Name.Text);
                    this.StuList.ItemsSource = result2;
                }
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new StudentRepository(_connectionString);
            var result = dataBase.StudentQuery(this.Email.Text, this.Name.Text);
            this.StuList.ItemsSource = result;
        }

        private void StuList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.StuList.SelectedItem != null)
            {
                this._stuSelected = (StudentModel)this.StuList.SelectedItem;
                this.Detail.DataContext = this._stuSelected;
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            if (this.StuList.SelectedItem != null)
            {
                var dataBase = new StudentRepository(_connectionString);
                var stuSelectResult = dataBase.StuSelect(_stuSelected.Id);
                if (stuSelectResult.Count == 0)
                {
                    MessageBox.Show("The student has no course selection");
                    return;
                }
                var stuSelectWin = new StudentSelectWindow(_stuSelected.Name, stuSelectResult);
                stuSelectWin.ShowDialog();
            }
        }
    }
}
