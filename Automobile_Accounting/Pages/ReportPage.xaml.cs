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
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            ListReport.ItemsSource = App.Context.report .ToList();
        }

        private void BtnRepDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentReport = (sender as Button).DataContext as BD.report;
            if (currentReport != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этот отчет?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        App.Context.report.Remove(currentReport);
                        App.Context.SaveChanges();
                        RefreshReportList();
                        MessageBox.Show("Отчет успешно удален.", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void RefreshReportList()
        {
            ListReport.ItemsSource = App.Context.report.ToList();
        }
        private void Repot_loaded(object sender, RoutedEventArgs e)
        {
            RefreshReportList();
        }
        private void BtnBackRep_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }

        private void BtnAddRep_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportAddPage());
        }

        private void BtnUpdateRep_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }
    }
}
