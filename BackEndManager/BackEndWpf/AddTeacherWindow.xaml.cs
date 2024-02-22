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
    /// AddTeacherWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddTeacherWindow : Window
    {
        private string _connection;
        private TeacherRepository _teacherRepo;
        private TeacherModel _dataContext;

        public AddTeacherWindow()
        {
            InitializeComponent();
            _connection = ((App)Application.Current).ConnectionString;
            _teacherRepo = new TeacherRepository(_connection);
            _dataContext = new TeacherModel();
            this.grid.DataContext = _dataContext;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var result = _teacherRepo.GetTeacher(_dataContext.Email, null);
            if (result.Count == 0)
            {
                _teacherRepo.AddTeacher(_dataContext);
                MessageBox.Show("Add Success!");
                this.Close();
            }
            else
            {
                MessageBox.Show("The teacher account has been registered");
            }
        }
    }
}
