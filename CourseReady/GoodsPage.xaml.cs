using CourseReady.teabase;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для GoodsPage.xaml
    /// </summary>
    public partial class GoodsPage : Page

    {
        TeaContext tea = new TeaContext();
        teabase.Пользователь пользователь;
        List<teabase.Товар> aa = new List<teabase.Товар>();
        string name;
        int idd;
        teabase.TeaContext teaContext = new teabase.TeaContext();
        ICollection<Товар> aaa;
        ICollection<Корзина> aaaa;
        teabase.Товар товар = new teabase.Товар();
        teabase.Корзина корзина = new teabase.Корзина();
        teabase.Корзина alice;
        teabase.Корзина alice2;
        teabase.КорзинаHasТовар A = new teabase.КорзинаHasТовар();
        public GoodsPage(teabase.Пользователь пользовательф)
        {
            InitializeComponent();
            ListGoods.ItemsSource = tea.Товарs.ToList();
            пользователь = пользовательф;





            ListGoods.SelectionChanged += ListGoods_SelectionChanged;

        }



        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HeadWindow.head.FrameMain.Navigate(new HistoryPage(пользователь, корзина));
        }
        public void SetUser(teabase.Пользователь юзер)
        {
            пользователь = юзер;
        }
        public teabase.Пользователь GetCurrentUser()
        {
            return пользователь;
        }
        private void CarButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (teabase.TeaContext teaContextt = new teabase.TeaContext())
                {
                    alice = teaContextt.Корзинаs.Where(x => x.ПользовательIdПользователь == пользователь.IdПользователь).FirstOrDefault();
                    alice2 = teaContext.Корзинаs.Include(x => x.КорзинаHasТоварs)
                        .ThenInclude(x=>x.ТоварIdТоварNavigation)
                        .FirstOrDefault(x => x.IdКорзина == alice.IdКорзина);
                }
                CartPage cart = new CartPage(пользователь, alice2);
                cart.Tovar(товар);
                cart.GetUser(пользователь);
                cart.GetCorz(alice2);
                HeadWindow.head.FrameMain.Navigate(cart);
            }
            catch
            {
                MessageBox.Show("Сначала добавьте что-нибудь в корзину");
            }
        }
        private void Caterory1(object sender, RoutedEventArgs e)
        {
            ListGoods.ItemsSource = tea.Товарs.Where(x => x.Категория == "Зеленый").ToList();
        }

        private void Caterory2(object sender, RoutedEventArgs e)
        {
            ListGoods.ItemsSource = tea.Товарs.Where(x => x.Категория == "Черный").ToList();
        }

        private void Caterory3(object sender, RoutedEventArgs e)
        {
            ListGoods.ItemsSource = tea.Товарs.Where(x => x.Категория == "Красный").ToList();
        }

        private void Caterory4(object sender, RoutedEventArgs e)
        {
            ListGoods.ItemsSource = tea.Товарs.ToList();
        }

        private void AddCartButon_Click(object sender, RoutedEventArgs e)
        {

            int _count = 0;

            try
            {
                _count = Convert.ToInt32(kolkol.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите количество товара!");
                return;
            }
            using (teabase.TeaContext teaContext = new teabase.TeaContext())
            {
                try
                {
                    alice = teaContext.Корзинаs.Where(x => x.ПользовательIdПользователь == пользователь.IdПользователь).FirstOrDefault();
                    if (alice == null)
                    {
                        корзина.ПользовательIdПользователь = пользователь.IdПользователь;

                        teaContext.Add(корзина);
                        teaContext.SaveChanges();
                    }
                    alice = teaContext.Корзинаs
                        .Include(x=>x.КорзинаHasТоварs).ThenInclude(x=>x.ТоварIdТоварNavigation)
                        .Where(x => x.ПользовательIdПользователь == пользователь.IdПользователь).FirstOrDefault();                                       
                    if (товар == null)
                    {
                        MessageBox.Show("Выберите товар!");
                        return;
                    }

                    if (alice.КорзинаHasТоварs.Any(x => x.ТоварIdТоварNavigation.IdТовар == товар.IdТовар)){
                        MessageBox.Show("Такой товар уже есть в корзине!");
                        return;
                    }
                    A.КорзинаIdКорзина = alice.IdКорзина;
                    A.ТоварIdТовар = товар.IdТовар;
                    A.Количество = Convert.ToInt32(_count);
                    A.ОбщаяСтоимость = Convert.ToInt32(товар.Стоимость) * Convert.ToInt32(_count);
                    alice.КорзинаHasТоварs.Add(A);
                    teaContext.SaveChanges();
                    MessageBox.Show("Товар добавлен в корзину");
                    _clearOnRefreshConfig();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Такой товар уже есть в корзине \n{ex.Message}");
                }
                
            }
        }
        private void _clearOnRefreshConfig()
        {
            ListGoods.SelectedItem = null;
            товар = null;
        }
        private void ListGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            товар = (Товар)ListGoods.SelectedItem;
        }

    }
}
