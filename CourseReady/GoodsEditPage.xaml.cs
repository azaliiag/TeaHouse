using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для GoodsEditPage.xaml
    /// </summary>
    public partial class GoodsEditPage : Page
    {
        teabase.TeaContext tea = new teabase.TeaContext();
        teabase.Товар товар = new teabase.Товар();
        byte[] a = null;
        byte[] image_bytes;
        public GoodsEditPage()
        {
            InitializeComponent();
            ListGoodsEdit.ItemsSource = tea.Товарs.ToList();
        }

        private void ListGoodsEdit_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (tea)
            {
                товар = (teabase.Товар)ListGoodsEdit.SelectedItem;
                NameEdit.Text = товар.Название;
                PriceEdit.Text = товар.Стоимость.ToString();
                CatEdit.Text = товар.Категория;
                QuantityEdit.Text = товар.Количество.ToString();
                MemoryStream memorystream = new MemoryStream();
                memorystream.Write(товар.Фото, 0, (int)товар.Фото.Length);
                if (memorystream.Length != 0)
                {
                    memorystream.Seek(0, SeekOrigin.Begin);
                    BitmapImage imgsource = new BitmapImage();
                    imgsource.BeginInit();
                    imgsource.StreamSource = memorystream;
                    imgsource.EndInit();
                    Photo.Source = imgsource;
                    a = товар.Фото;
                } 
            }
        }

        private void PhotoEdit_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.jpg)|*.jpg|(*.png)|*.png";
            if (ofd.ShowDialog() == true)
            {
                a = File.ReadAllBytes(ofd.FileName);
                MemoryStream ms = new MemoryStream();
                ms.Write(a, 0, (int)a.Length);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage imgsource = new BitmapImage();
                imgsource.BeginInit();
                imgsource.StreamSource = ms;
                imgsource.EndInit();
               Photo.Source = imgsource;
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (teabase.TeaContext del = new teabase.TeaContext())
            {
                del.Remove(товар);
                del.SaveChanges();
                ListGoodsEdit.ItemsSource = del.Товарs.ToList();
                NameEdit.Text = "";
                PriceEdit.Text = "";
                CatEdit.Text = "";
                QuantityEdit.Text = "";
                Photo.Source = null;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (teabase.TeaContext addc = new teabase.TeaContext())
            {
                teabase.Товар товар = new teabase.Товар() { Название = NameEdit.Text, Стоимость = Convert.ToInt32(PriceEdit.Text), Категория = CatEdit.Text, Количество = Convert.ToInt32(QuantityEdit.Text), Фото = a };
                addc.Add(товар);
                addc.SaveChanges();
                ListGoodsEdit.ItemsSource = addc.Товарs.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            using (teabase.TeaContext ed = new teabase.TeaContext())
            {
                var tov = ed.Товарs.FirstOrDefault(x => x.IdТовар == товар.IdТовар);
                if (tov == null)
                    return;
                tov.Название = NameEdit.Text;
                tov.Стоимость = Convert.ToInt32(PriceEdit.Text);
                tov.Категория = CatEdit.Text;
                tov.Количество = Convert.ToInt32(QuantityEdit.Text);
                tov.Фото = a;
                ed.SaveChanges();
                ListGoodsEdit.ItemsSource = ed.Товарs.ToList();
            }
        }
    }
}
