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
using System.Data.Entity.Validation;

namespace Automobile_Accounting.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddRole.xaml
    /// </summary>
    public partial class AddRole : Page
    {
        public AddRole()
        {
            InitializeComponent();
            InitializeComboBox();
        }
        private void InitializeComboBox()
        {
            ComboBoxRole.Items.Add("Администратор");
            ComboBoxRole.Items.Add("Менеджер");
            ComboBoxRole.SelectedIndex = 0; 
        }

        private void BtnSendAdd_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";

            var validationErrors = CheckEmployeeErrors();
            if (validationErrors.Length > 0)
            {
                MessageBox.Show(validationErrors, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                int selectedRoleId;
                string roleName = "";

                if (ComboBoxRole.SelectedItem is string selectedRoleName)
                {
                    switch (selectedRoleName)
                    {
                        case "Администратор":
                            selectedRoleId = 1;
                            roleName = "Администратор"; 
                            break;
                        case "Менеджер":
                            selectedRoleId = 2;
                            roleName = "Менеджер"; 
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                    }
                }
                else
                {
                    MessageBox.Show("Не выбрана роль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string password = TBoxPasswordEmployee.Password;

                var newEmployee = new BD.role() 
                {
                    id_role = selectedRoleId,
                    name = roleName, 
                    fio = TBoxFIOEmployee.Text,
                    login = TBoxLoginEmployee.Text,
                    password = password,
                };

                App.Context.role.Add(newEmployee);
                App.Context.SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (DbEntityValidationException ex)
            {
                errorMessage = "Произошла ошибка при добавлении сотрудника:\n";
                foreach (var validationErrors2 in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors2.ValidationErrors)
                    {
                        errorMessage += $"Свойство: {validationError.PropertyName} Ошибка: {validationError.ErrorMessage}\n";
                    }
                }
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                errorMessage = $"Произошла общая ошибка: {ex.Message}";
                MessageBox.Show(errorMessage, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string CheckEmployeeErrors()
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(TBoxFIOEmployee.Text))
            {
                errorMessage += "Необходимо ввести ФИО.\n";
            }

            if (string.IsNullOrWhiteSpace(TBoxLoginEmployee.Text))
            {
                errorMessage += "Необходимо ввести логин.\n";
            }

            if (string.IsNullOrWhiteSpace(TBoxPasswordEmployee.Password))
            {
                errorMessage += "Необходимо ввести пароль.\n";
            }
            else if (TBoxPasswordEmployee.Password.Length < 6)
            {
                errorMessage += "Пароль должен содержать не менее 6 символов.\n";
            }
            return errorMessage;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicePage());
        }
    }
}
