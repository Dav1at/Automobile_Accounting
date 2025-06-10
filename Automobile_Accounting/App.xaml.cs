using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Automobile_Accounting
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BD.automobile_accountingBD Context
        { get; set; } = new BD.automobile_accountingBD();

        public App()
        {
            GlobalFontSettings.UseWindowsFontsUnderWindows = true; 
        }
        public static BD.role Currentrole = null;
    }
        
}
