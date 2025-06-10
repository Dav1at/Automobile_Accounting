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

namespace Automobile_Accounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBoxLogin.Text) || string.IsNullOrWhiteSpace(PBoxPassword.Password))
            {
                MessageBox.Show("Данные не введны, заполните логин и пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var currentrole = App.Context.role.FirstOrDefault(p => p.login == TBoxLogin.Text && p.password == PBoxPassword.Password);
            if (currentrole != null)
            {
                App.Currentrole = currentrole;
                switch (currentrole.name) 
                {
                    case "Администратор":
                        NavigationService.Navigate(new ServicePage());
                        break;
                    case "Менеджер":
                        NavigationService.Navigate(new ManagerPage());
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TBoxLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PBoxPassword.Focus();
            }
        }

        private void PBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, new RoutedEventArgs());
            }
        }

        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("За восстанавление доступа обратитесь к Администратору базы данных. Вы можете обратиться на почту: abduzhalilovdavlat@gmail.com", "Восстановление доступа",MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
