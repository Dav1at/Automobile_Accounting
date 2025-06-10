using Automobile_Accounting.Pages;
using PdfSharp.Fonts;
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

namespace Automobile_Accounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameNavigatin.Navigated += FrameNavigatin_Navigated;
            FrameNavigatin.Navigate(new Pages.LoginPage());
            GlobalFontSettings.UseWindowsFontsUnderWindows = true;
        }
        private void FrameNavigatin_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Pages.LoginPage)
            {
                TBlockRole.Visibility = Visibility.Collapsed;
                TBlockRoleName.Visibility = Visibility.Collapsed;
                BtnExit.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (App.Currentrole != null)
                {
                    TBlockRole.Text = $"Роль: {App.Currentrole.name}";
                    TBlockRoleName.Text = App.Currentrole.fio ;
                }
                else
                {
                    TBlockRole.Text = "Роль не определена";
                    TBlockRoleName.Text = "ФИО не определено";
                }
                TBlockRole.Visibility = Visibility.Visible;
                TBlockRoleName.Visibility = Visibility.Visible;
                BtnExit.Visibility = Visibility.Visible;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameNavigatin.NavigationService.Navigate(new LoginPage());
        }
    }
}
