﻿using Paywall.Presentation.ViewModels;
using Paywall.Presentation.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Paywall.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            DataContext = new OpenRepairOrdersViewModel();
        }

        private void ButtonPowerOff_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonShowOpenRos_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OpenRepairOrdersViewModel();
        }

        private void ButtonShowParts_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PartsViewModel();
        }

        private void ButtonShowCustom_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new CustomViewModel();
        }

        private void ButtonShowLastSevenDaysPayments_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LastSevenDaysPaymentsViewModel();
        }
    }
}
