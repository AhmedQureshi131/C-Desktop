using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for LastSevenDaysPayments.xaml
    /// </summary>
    public partial class LastSevenDaysPayments : UserControl
    {
        public LastSevenDaysPayments()
        {
            InitializeComponent();

            //Loaded += LastSevenDaysPayments_Loaded;
        }

        private async void LastSevenDaysPayments_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
