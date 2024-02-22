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
    /// CoursePage.xaml 的互動邏輯
    /// </summary>
    public partial class CoursePage : Page
    {
        private string _connectionString = ((App)Application.Current).ConnectionString;
        private CourseModel _courseSelected;

        public CoursePage()
        {
            InitializeComponent();
        }

        private void CourseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.CourseList.SelectedItem != null)
            {
                this._courseSelected = (CourseModel)this.CourseList.SelectedItem;
                this.Detail.DataContext = this._courseSelected;
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            var addCourseWin = new AddCourseWindow();
            addCourseWin.ShowDialog();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new CourseRepository(_connectionString);
            if (this._courseSelected == null)
                return;

            //Here for simulate that highest admin cannot be deleted!

            try
            {
                dataBase.DeleteCourse(this._courseSelected);
                MessageBox.Show("The data has been deleted!");
                var result = dataBase.GetCourse(this.Name.Text);
                this.CourseList.ItemsSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot delete this course, probably the course might have been started by a teacher");
            }
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new CourseRepository(_connectionString);
            if (this._courseSelected == null)
                return;
            dataBase.UpdateCourseData(_courseSelected);
            MessageBox.Show("Modified!");
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var dataBase = new CourseRepository(_connectionString);
            var result = dataBase.GetCourse(this.Name.Text);
            this.CourseList.ItemsSource = result;
        }
    }
}
