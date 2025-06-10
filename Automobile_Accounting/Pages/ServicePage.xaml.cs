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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            ListService.ItemsSource = App.Context.avto.ToList();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentAvto = (sender as Button).DataContext as BD.avto;
            NavigationService.Navigate(new EditPage(currentAvto));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentAvto = (sender as Button).DataContext as BD.avto;
            if (currentAvto != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот автомобиль?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        App.Context.avto.Remove(currentAvto);
                        App.Context.SaveChanges();
                        RefreshAvtoList();
                        MessageBox.Show("Автомобиль успешно удален.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении автомобиля: {ex.Message}", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Не удалось получить информацию об автомобиле для удаления.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RefreshAvtoList()
        {
            ListService.ItemsSource = App.Context.avto.ToList();
        }
        private void ServicePage_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAvtoList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPage());
        }

        private void BtnAddRole_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRole());
        }
    }
}
