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
    /// Логика взаимодействия для EditManPage.xaml
    /// </summary>
    public partial class EditManPage : Page
    {
        private BD.order currentorder = null;

        public EditManPage()
        {
            InitializeComponent();
        }

        public EditManPage(BD.order orderq)
        {
            InitializeComponent();
            currentorder = orderq;
            TBoxNumOrder.Text = currentorder.num_order.ToString();
            TBoxDateOrder.Text = currentorder.date_order.ToString();
            TBoxBrendAvto.Text = currentorder.name_brend;
            TBoxModelOrder.Text = currentorder.name_model;
            TBoxConfigOrder.Text = currentorder.name_configuration;
            TBoxСolorOrder.Text = currentorder.name_color;
            TBoxQuantityOrder.Text = currentorder.quantity_avto.ToString();
            TBoxSumOrder.Text = currentorder.sum_sale.ToString();
            TBoxPartnerOrder.Text = currentorder.name_partner;
            TBoxIDParnerOrder.Text = currentorder.fio_partner;
            TBoxDiscountOrder.Text = currentorder.discount.ToString();
        }

        private void BtnBackMan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }

        private void BtnSaveMan_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors();
            if (errorMessage.Length > 0)
            {
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (currentorder == null)
                    {
                        var newOrder = new BD.order()
                        {
                            num_order = int.Parse(TBoxNumOrder.Text),
                            date_order = DateTime.Parse(TBoxDateOrder.Text),
                            name_brend = TBoxBrendAvto.Text,
                            name_model = TBoxModelOrder.Text,
                            name_configuration = TBoxConfigOrder.Text,
                            name_color = TBoxСolorOrder.Text,
                            quantity_avto = TBoxQuantityOrder.Text,
                            sum_sale = int.Parse(TBoxSumOrder.Text),
                            name_partner = TBoxPartnerOrder.Text,
                            fio_partner = TBoxIDParnerOrder.Text,
                            discount = int.Parse(TBoxDiscountOrder.Text)
                        };
                        App.Context.order.Add(newOrder);
                        App.Context.SaveChanges();
                    }
                    else
                    {
                        currentorder.num_order = int.Parse(TBoxNumOrder.Text);
                        currentorder.date_order = DateTime.Parse(TBoxDateOrder.Text); 
                        currentorder.name_brend = TBoxBrendAvto.Text;
                        currentorder.name_model = TBoxModelOrder.Text;
                        currentorder.name_configuration = TBoxConfigOrder.Text;
                        currentorder.name_color = TBoxСolorOrder.Text;
                        currentorder.quantity_avto = TBoxQuantityOrder.Text;
                        currentorder.sum_sale = int.Parse(TBoxSumOrder.Text);
                        currentorder.name_partner = TBoxPartnerOrder.Text;
                        currentorder.fio_partner = TBoxIDParnerOrder.Text;
                        currentorder.discount = int.Parse(TBoxDiscountOrder.Text);

                        App.Context.SaveChanges();
                        MessageBox.Show("Заявка успешно отредактирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string CheckErrors()
        {
            string errorMessage = "";

            if (string.IsNullOrWhiteSpace(TBoxSumOrder.Text))
            {
                errorMessage += "Поле 'ID номер заявки' не может быть пустым.\n";
            }
            else if (!int.TryParse(TBoxSumOrder.Text, out _))
            {
                errorMessage += "Поле 'ID номер заявки' должно быть целым числом.\n";
            }

            if (string.IsNullOrWhiteSpace(TBoxNumOrder.Text))
            {
                errorMessage += "Поле 'Номер  заявки' не может быть пустым.\n";
            }
            else if (!int.TryParse(TBoxNumOrder.Text, out _))
            {
                errorMessage += "Поле 'Номер  заявки' должно быть целым числом.\n";
            }
            return errorMessage;
        }
    }
}
