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
using Automobile_Accounting.BD;

namespace Automobile_Accounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportAddPage.xaml
    /// </summary>
    public partial class ReportAddPage : Page
    {
        public ReportAddPage()
        {
            InitializeComponent();
            Loaded += ReportAddPage_Loaded; 
        }
        private void ReportAddPage_Loaded(object sender, RoutedEventArgs e)
        {
            TBlockReportDate.Text = DateTime.Now.ToString("dd.MM.yyyy"); 
        }

        private void BtnBackRep_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ReportPage());
        }

        private void BtnSaveRep_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = DatePickerStartDate.SelectedDate;
            DateTime? endDate = DatePickerEndDate.SelectedDate;
            if (!startDate.HasValue || !endDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите начальную и конечную даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DateTime start = startDate.Value;
            DateTime end = endDate.Value;
            int reportId;
            if (!int.TryParse(TBoxID.Text, out reportId))
            {
                MessageBox.Show("Пожалуйста, введите корректный ID номер отчета.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            decimal totalQuantityAvto = 0;
            decimal totalSumSale = 0;
            int totalOrders = 0; 

            try
            {
                using (var context = new automobile_accountingBD()) 
                {
                    var filteredOrders = context.order
                        .Where(o => o.date_order >= start && o.date_order <= end)
                        .ToList();
                    totalQuantityAvto = filteredOrders.Sum(o => decimal.Parse(o.quantity_avto)); 
                    totalSumSale = filteredOrders.Sum(o => o.sum_sale);
                    totalOrders = filteredOrders.Count();
                    TBlockTotalQuantityAvto.Text = totalQuantityAvto.ToString();
                    TBlockTotalSumSale.Text = totalSumSale.ToString();
                    TBlockTotalOrders.Text = totalOrders.ToString(); 
                    SaveReport(reportId, start, end, totalQuantityAvto, totalSumSale, totalOrders);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveReport(int reportId, DateTime startDate, DateTime endDate, decimal totalQuantityAvto, decimal totalSumSale, int totalOrders)
        {
            try
            {
                using (var context = new automobile_accountingBD()) 
                {
                    var newReport = new report
                    {
                        Id_report = reportId, 
                        date_report = DateTime.Now, 
                        date_start = startDate,       
                        date_end = endDate,        
                        quantity_avto_sale = totalQuantityAvto,
                        quantity_sum_sale = totalSumSale,  
                        quantity_order = totalOrders  
                    };
                    context.report.Add(newReport);
                    context.SaveChanges();

                    MessageBox.Show("Отчет успешно сохранен в базу данных.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveReport(DateTime startDate, DateTime endDate, decimal totalQuantityAvto, decimal totalSumSale)
        {
            try
            {
                using (var context = new automobile_accountingBD()) 
                {
                    var newReport = new report
                    {
                        date_report = DateTime.Now,
                        date_start = startDate,   
                        date_end = endDate,      
                        quantity_avto_sale = totalQuantityAvto,
                        quantity_sum_sale = totalSumSale,  
                        quantity_order = 0 
                    };
                context.report.Add(newReport);
                context.SaveChanges();

                    MessageBox.Show("Отчет успешно сохранен в базу данных.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
