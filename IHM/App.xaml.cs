using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TP_POO;
using System.IO;
using Microsoft.Win32;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static NavigationService navigator;

        void App_Navigated(object sender, NavigationEventArgs e)
        {
            Page page = e.Content as Page;
            if (page != null)
                ApplicationHelper.NavigationService = page.NavigationService;

        }
    }
}
