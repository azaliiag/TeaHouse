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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public static CartPage cartpage;
        teabase.TeaContext teaContext = new teabase.TeaContext();
        teabase.Товар тов = new teabase.Товар();
        teabase.Корзина корзина = new teabase.Корзина();
        teabase.Пользователь пользователь;
        teabase.КорзинаHasТовар haskor;
         string a = null;
        int? TotalPrice = 0;
        int? TotalKolvo = 0;
        public CartPage(teabase.Пользователь польз, teabase.Корзина корз)
        {
            InitializeComponent();
            пользователь = польз;
            корзина = корз;
         
        }
        private void CartBackButton_Click(object sender, RoutedEventArgs e)
        {
            GoodsPage goodsPage = new GoodsPage(пользователь);
            goodsPage.SetUser(this.пользователь);     
            HeadWindow.head.FrameMain.Navigate(new GoodsPage(пользователь));
        }
        public void Tovar(teabase.Товар Товар)
        {
            тов = Товар;
        }
        public void GetUser(teabase.Пользователь поль)
        {
            пользователь = поль;
        }

        public void GetCorz(teabase.Корзина корз)
        {
            корзина = корз;
            LoadInfo();
            UpdateInfo();
        }
        public void LoadInfo()
        {
            ListTovCorzina.ItemsSource = корзина.КорзинаHasТоварs.ToList();
        }
        public void UpdateInfo()
        {
            TotalPrice = 0;
            TotalKolvo = 0;
            foreach (var item in ListTovCorzina.Items)
            {
                haskor = (teabase.КорзинаHasТовар)item;
                if (haskor.ОбщаяСтоимость != null)
                {
                    TotalPrice = TotalPrice + haskor.ОбщаяСтоимость;
                    TotalKolvo = TotalKolvo + haskor.Количество;
                }
            }
            ItogPrice.Content = "Итого: " + TotalPrice.ToString();
            TotalTov.Content = "Товаров: "+TotalKolvo.ToString();
        }
       
        //private void DelTovar(object sender, RoutedEventArgs e)
        //{
           
        //}
        public void GetHasKorz(teabase.КорзинаHasТовар корз)
        {
            haskor = корз;
        }
        public void OplataZakaz()
        {           
        }

        private void CardPay_Click(object sender, RoutedEventArgs e)
        {
            using (TeaContext mag = new TeaContext())
            {
                teabase.Товар товар = new Товар();
                List<teabase.КорзинаHasТовар> list2 = корзина.КорзинаHasТоварs.ToList();
                foreach (var item in list2)
                {
                    a = a + item.Название + " " + item.Количество + " шт" + "; ";
                    товар = item.ТоварIdТоварNavigation;
                    товар.Количество = товар.Количество - item.Количество;
                    mag.Update(товар);
                }
                teabase.Заказ заказ = new Заказ() { Адрес = adres.Text, СпособОплаты = "Карта", ОбщаяСтоимость = TotalPrice, ПользовательIdПользователь = пользователь.IdПользователь, Товар = a };
                mag.Add(заказ);
                List<teabase.КорзинаHasТовар> Ф = mag.КорзинаHasТоварs.Where(x => x.КорзинаIdКорзина == корзина.IdКорзина).ToList();
                foreach (var item in Ф)
                {
                    mag.Remove(item);
                }
                mag.SaveChanges();
                MessageBox.Show("Заказ успешно офомлен", null, MessageBoxButton.OK);
                GoodsPage goodsPage = new GoodsPage(пользователь);
                goodsPage.SetUser(this.пользователь);
                HeadWindow.head.FrameMain.Navigate(new GoodsPage(пользователь));
            }
        }

        private void NalPay_Click(object sender, RoutedEventArgs e)
        {
            using (TeaContext mag = new TeaContext())
            {
                teabase.Товар товар = new Товар();
                List<teabase.КорзинаHasТовар> list2 = корзина.КорзинаHasТоварs.ToList();
                foreach (var item in list2)
                {
                    a = a + item.Название + " " + item.Количество + " шт" + "; ";
                    товар = item.ТоварIdТоварNavigation;
                    товар.Количество = товар.Количество - item.Количество;
                    mag.Update(товар);
                }
                teabase.Заказ заказ = new Заказ() { Адрес = adres.Text, СпособОплаты = "Наличные", ОбщаяСтоимость = TotalPrice, ПользовательIdПользователь = пользователь.IdПользователь, Товар = a };
                mag.Add(заказ);
                List<teabase.КорзинаHasТовар> Ф = mag.КорзинаHasТоварs.Where(x => x.КорзинаIdКорзина == корзина.IdКорзина).ToList();
                foreach (var item in Ф)
                {
                    mag.Remove(item);
                }
                mag.SaveChanges();
                MessageBox.Show("Заказ успешно офомлен", null, MessageBoxButton.OK);
                GoodsPage goodsPage = new GoodsPage(пользователь);
                goodsPage.SetUser(this.пользователь);
                HeadWindow.head.FrameMain.Navigate(new GoodsPage(пользователь));
            }
        }

        private void DelTovar_Click(object sender, RoutedEventArgs e)
        {
            using (teabase.TeaContext MCAA = new teabase.TeaContext())
            {
                haskor = (teabase.КорзинаHasТовар)ListTovCorzina.SelectedItem;
                MCAA.Remove(haskor);
                MCAA.SaveChanges();
                ListTovCorzina.ItemsSource = корзина.КорзинаHasТоварs.ToList();
                UpdateInfo();
            }
        }

        private void ListTovCorzina_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (teabase.TeaContext MC = new teabase.TeaContext())
            {
                //корзина = (sakila.Корзина)ListTovCorzina.SelectedItem;
                //корзина.Товарs.GetEnumerator();
                //MC.Update(корзина);
                //MC.SaveChanges();
                //ListTovCorzina.ItemsSource = MC.Корзинаs.Where(d => d.IdПользователь == пользователь.IdПользователь).ToList();

            }
        }
    }
}
