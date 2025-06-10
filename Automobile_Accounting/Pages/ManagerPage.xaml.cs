using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
            ListManager.ItemsSource = App.Context.order.ToList();
        }

        private void BtnManEdit_Click(object sender, RoutedEventArgs e)
        {
            var currentOrder = (sender as Button).DataContext as BD.order;
            NavigationService.Navigate(new EditManPage(currentOrder));
        }

        private void BtnManDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentOrder = (sender as Button).DataContext as BD.order;
            if (currentOrder != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту заявку?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        App.Context.order.Remove(currentOrder);
                        App.Context.SaveChanges();
                        RefreshOrderList();
                        MessageBox.Show("Заявка успешно удалена.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заявки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Не удалось получить информацию о заявке для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnUpdateMan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }

        private void BtnPartnerMan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PartnerManPage());
        }

        private void BtnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PartnerPage());
        }
        private void Btnreport_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }

        private void RefreshOrderList()
        {
            ListManager.ItemsSource = App.Context.order.ToList();
        }
        private void ManagerPage_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshOrderList();
        }

    }
}
