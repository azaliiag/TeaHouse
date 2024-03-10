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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        teabase.Пользователь currentUser;
        TeaContext teaContext = new TeaContext();
        teabase.Пользователь user;
        teabase.Корзина корзина;
        public HistoryPage(teabase.Пользователь пользователь, teabase.Корзина корз)
        {
            InitializeComponent();       
            user = пользователь;
            DataGridZakaz.ItemsSource = teaContext.Заказs.Where(x => x.ПользовательIdПользователь == user.IdПользователь).ToList();
            корзина = корз;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
               
            HeadWindow.head.FrameMain.Navigate(new GoodsPage(user));
        }
    }
}
