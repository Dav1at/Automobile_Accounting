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
    /// Логика взаимодействия для PartnerManPage.xaml
    /// </summary>
    public partial class PartnerManPage : Page
    {
        public PartnerManPage()
        {
            InitializeComponent();
            ListPartners.ItemsSource = App.Context.partners.ToList();
        }

        private void BtnBackPartnerMan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerPage());
        }
    }
}
