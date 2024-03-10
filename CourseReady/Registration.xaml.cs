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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void MoveToMain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWin = new MainWindow();
            MainWin.Show();
            this.Hide();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            using (TeaContext tea = new TeaContext())
            {             
                    Пользователь пользователь = new Пользователь() { Логин = RegLogin.Text, Пароль = RegPassword.Text, Роль = "Пользователь" };
                    tea.Add(пользователь);
                    tea.SaveChanges();
                    MainWindow MainWin = new MainWindow();
                    MainWin.Show();
                    this.Hide();                
            }
               
        }
    }
}
