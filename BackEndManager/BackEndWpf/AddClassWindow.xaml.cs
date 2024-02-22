using CoreLib.DataBase;
using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// AddClassWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddClassWindow : Window
    {
        private string _connection;
        private ClassRepository _classRepo;
        private ClassModel _dataContext;

        public AddClassWindow()
        {
            InitializeComponent();
            _connection = ((App)Application.Current).ConnectionString;
            _classRepo = new ClassRepository(_connection);
            _dataContext = new ClassModel();
            this.grid.DataContext = _dataContext;

            var course = new CourseRepository(_connection);
            this.course.ItemsSource = course.GetCourse();

            var teacher = new TeacherRepository(_connection);
            this.teacher.ItemsSource = teacher.GetTeacher();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_dataContext.CourseID == null)
            {
                return;
            }

            if (_dataContext.TeacherID == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(_dataContext.Location))
            {
                return;
            }

            var addClass = new ClassRepository(_connection);
            addClass.AddClass(_dataContext);
            MessageBox.Show("Add Success!");
            this.Close();
        }
    }
}
