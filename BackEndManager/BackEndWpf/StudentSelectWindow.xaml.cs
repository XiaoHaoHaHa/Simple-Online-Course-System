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
using System.Windows.Shapes;

namespace BackEndWpf
{
    /// <summary>
    /// StudentSelectWindow.xaml 的互動邏輯
    /// </summary>
    public partial class StudentSelectWindow : Window
    {
        private string _stuName;
        private ObservableCollection<StuSelectModel> _stuSelect;

        public StudentSelectWindow(string stuName, ObservableCollection<StuSelectModel> stuSelect)
        {
            InitializeComponent();
            _stuName = stuName;
            _stuSelect = stuSelect;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.stuSelectList.ItemsSource = _stuSelect;
            this.student.Text = _stuName;
        }
    }
}
