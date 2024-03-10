using Org.BouncyCastle.Crypto.Engines;
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
    /// Логика взаимодействия для UsersEditPage.xaml
    /// </summary>
    public partial class UsersEditPage : Page
    {
        teabase.TeaContext teaContext = new teabase.TeaContext(); //Контекст базы данных
        teabase.Пользователь пользователь = new teabase.Пользователь(); //Контекст пользоваетеля
        public UsersEditPage()
        {
            InitializeComponent();
            ListUser.ItemsSource = teaContext.Пользовательs.ToList();
        }

        private void ListUser_MouseDoubleClick(object sender, MouseButtonEventArgs e) //Получение контекста пользователя и занесение в текстбоксы
        {
            using (teaContext)
            {
                пользователь = (teabase.Пользователь)ListUser.SelectedItem;
                EditLogin.Text = пользователь.Логин;
                EditRole.Text = пользователь.Роль;
                EditPassword.Text = пользователь.Пароль;

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e) //Добавление пользователя
        {
            using (teabase.TeaContext addcontext = new teabase.TeaContext())
            {
                teabase.Пользователь пользователь = new teabase.Пользователь() { Логин = EditLogin.Text, Пароль = EditPassword.Text, Роль = EditRole.Text };
                addcontext.Add(пользователь);
                addcontext.SaveChanges();
                ListUser.ItemsSource = addcontext.Пользовательs.ToList();
            }
        
    
    }
        private void EditButton_Click(object sender, RoutedEventArgs e) //Редактирование пользователя
        {
            using (teabase.TeaContext editcontext = new teabase.TeaContext())
            {
                пользователь.Логин = EditLogin.Text;
                пользователь.Роль = EditRole.Text;
                пользователь.Пароль = EditPassword.Text;
                editcontext.Update(пользователь);
                editcontext.SaveChanges();
                ListUser.ItemsSource = editcontext.Пользовательs.ToList();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e) //Удаление пользователя
        {
            using (teabase.TeaContext deletecontext = new teabase.TeaContext())
            {
                deletecontext.Remove(пользователь);
                deletecontext.SaveChanges();
                ListUser.ItemsSource = deletecontext.Пользовательs.ToList();
                EditLogin.Text = "";
                EditRole.Text = "";
                EditPassword.Text = "";
            }
        }
    }
}
