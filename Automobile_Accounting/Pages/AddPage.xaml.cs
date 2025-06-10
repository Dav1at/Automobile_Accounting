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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        public AddPage()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var errorMessage = CheckErrors(); 
            if (errorMessage.Length > 0) 
            {
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var newAvto = new BD.avto()
                {
                    id_avto = int.Parse(TBoxID.Text),
                    serial_number = TBoxSerrialNum.Text,
                    brend = TBoxBrend.Text,
                    model = TBoxModel.Text,
                    configuration = TBoxConfig.Text,
                    year = int.Parse(TBoxYear.Text),
                    kuzov = TBoxkuzov.Text,
                    color = TBoxcolor.Text,
                    price = int.Parse(TBoxprice.Text),
                    quantity = int.Parse(TBoxquantity.Text),
                };

                App.Context.avto.Add(newAvto);
                App.Context.SaveChanges();
                MessageBox.Show("Автомобиль успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении автомобиля: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private string CheckErrors()
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(TBoxID.Text))
            {
                errorMessage += "Поле 'ID' не может быть пустым.\n";
            }
            else if (!int.TryParse(TBoxID.Text, out _))
            {
                errorMessage += "Поле 'ID' должно быть целым числом.\n";
            }
            else if (int.Parse(TBoxID.Text) <= 0) 
            {
                errorMessage += "Поле 'ID' должно быть положительным числом.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxSerrialNum.Text))
            {
                errorMessage += "Поле 'Серийный номер' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxBrend.Text))
            {
                errorMessage += "Поле 'Бренд' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxModel.Text))
            {
                errorMessage += "Поле 'Модель' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxYear.Text))
            {
                errorMessage += "Поле 'Год выпуска' не может быть пустым.\n";
            }
            else if (!int.TryParse(TBoxYear.Text, out _)) 
            {
                errorMessage += "Поле 'Год выпуска' должно быть целым числом.\n";
            }
            else if (int.Parse(TBoxYear.Text) <= 0) 
            {
                errorMessage += "Поле 'Год выпуска' должно быть положительным числом.\n";
            }
            else if (int.Parse(TBoxYear.Text) > DateTime.Now.Year) 
            {
                errorMessage += "Поле 'Год выпуска' не может быть больше текущего года.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxkuzov.Text))
            {
                errorMessage += "Поле 'Кузов' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxcolor.Text))
            {
                errorMessage += "Поле 'Цвет' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxprice.Text))
            {
                errorMessage += "Поле 'Цена' не может быть пустым.\n";
            }
            if (string.IsNullOrWhiteSpace(TBoxquantity.Text))
            {
                errorMessage += "Поле 'Количесвто' не может быть пустым.\n";
            }
            return errorMessage;
        }
    }
}

