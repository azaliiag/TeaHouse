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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseReady
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveToReg_Click(object sender, RoutedEventArgs e)
        {
            Registration RegWin = new Registration();
            RegWin.Show();
            this.Hide();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            using (TeaContext tea = new TeaContext())
            {
                foreach (var item in tea.Пользовательs)
                {
                    if (item.Логин == LoginTextBox.Text && item.Пароль == PasswordTextBox.Text)
                    {
                        var x = new HeadWindow(item);
                        x.Show();
                        this.Hide();
                       
                    }
                   
                }
            }
          
        }
    }
}
