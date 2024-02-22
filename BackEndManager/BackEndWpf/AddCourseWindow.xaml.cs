using CoreLib.DataBase;
using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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
    /// AddCourseWindow.xaml 的互動邏輯
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        private string _connection;
        private CourseRepository _courseRepo;
        private CourseModel _dataContext;

        public AddCourseWindow()
        {
            InitializeComponent();
            _connection = ((App)Application.Current).ConnectionString;
            _courseRepo = new CourseRepository(_connection);
            _dataContext = new CourseModel();
            this.grid.DataContext = _dataContext;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var result = _courseRepo.GetCourse(this._dataContext.CourseName);
            if (result.Count == 0)
            {
                try
                {
                    _dataContext.Hour = int.Parse(this.hour.Text);
                    _courseRepo.AddCourse(_dataContext);
                    MessageBox.Show("Add Success!");
                    this.Close();
                }
                catch (SqlException ex)
                {
                    StringBuilder errorMessages = new StringBuilder();
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    Console.WriteLine(errorMessages.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Course Hour is not in the right format");
                }               
            }
            else
            {
                MessageBox.Show("The Course is in the course list");
            }
        }
    }
}
