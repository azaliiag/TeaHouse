using CourseReady.teabase;
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
using System.Windows.Shapes;

namespace CourseReady
{
    /// <summary>
    /// Логика взаимодействия для HeadWindow.xaml
    /// </summary>
    public partial class HeadWindow : Window
    {
        public static HeadWindow head;
        teabase.Пользователь currentUser;
        public HeadWindow(teabase.Пользователь oldUser)
        {
            currentUser = oldUser;
            InitializeComponent();
            head = this;
            using (TeaContext tea = new TeaContext())
            {
                    if (currentUser.Роль == "Пользователь")
                    {                       
                        FrameMain.Navigate(new GoodsPage(currentUser));
                    }
                    if (currentUser.Роль == "Сотрудник")
                    {
                        FrameMain.Navigate(new GoodsEditPage());
                    }
                    if (currentUser.Роль == "Администратор")
                    {
                        FrameMain.Navigate(new UsersEditPage());
                    }               
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
