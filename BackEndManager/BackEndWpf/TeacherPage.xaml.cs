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
    /// TeacherPage.xaml 的互動邏輯
    /// </summary>
    public partial class TeacherPage : Page
    {
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private TeacherModel _teacherSelected;

        public TeacherPage()
        {
            InitializeComponent();
        }

        private void TeacherList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.TeacherList.SelectedItem != null)
            {
                this._teacherSelected = (TeacherModel)this.TeacherList.SelectedItem;
                this.Detail.DataContext = this._teacherSelected;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new TeacherRepository(_connectionString);
            var result = dataBase.GetTeacher(this.Email.Text, this.Name.Text);
            this.TeacherList.ItemsSource = result;
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new TeacherRepository(_connectionString);
            if (this._teacherSelected == null)
                return;
            dataBase.UpdateTeacherData(_teacherSelected);
            MessageBox.Show("Modified!");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new TeacherRepository(_connectionString);
            if (this._teacherSelected == null)
                return;

            //Here for simulate that highest admin cannot be deleted!

            try
            {
                dataBase.DeleteTeacher(this._teacherSelected);
                MessageBox.Show("The data has been deleted!");
                var result = dataBase.GetTeacher(this.Email.Text, this.Name.Text);
                this.TeacherList.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot delete this teacher, probably the teacher might have started a lecture");
            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            var addTeacherWin = new AddTeacherWindow();
            addTeacherWin.ShowDialog();
        }

    }
}
