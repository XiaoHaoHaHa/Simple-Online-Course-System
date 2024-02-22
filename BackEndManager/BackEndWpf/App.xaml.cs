using CoreLib.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BackEndWpf
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private const string _mutexName = "WPFmain";
        public string ConnectionString { get; set; }
        public AdminModel AdminData { get; set; }
        public App()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CourseDB"].ConnectionString;
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (var mutex = new System.Threading.Mutex(true, _mutexName, out bool createdNew))
            {
                if (createdNew)
                {
                    // 如果createdNew為true，表示這是第一個實例，程式還沒有在執行

                    // 這裡放置您的程式邏輯
                    var windowLogin = new WindowLogin();
                    windowLogin.Show();
                    // 程式執行完畢後，請釋放Mutex
                    mutex.ReleaseMutex();
                }
                else
                {
                    // 如果createdNew為false，表示已經有另一個實例在執行中
                    MessageBox.Show("程式已在執行中，無法啟動多個實例。");
                    Application.Current.Shutdown();
                }
            }
        }
    }
}

