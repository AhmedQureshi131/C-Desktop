using Paywall.Application.Invoice.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Presentation.ViewModels
{
    class CustomViewModel : ViewModelBase
    {
        private ObservableCollection<GetCustomInvoicesVm> _GetCustomInvoicesVms;

        public CustomViewModel()
        {
            var query = new GetCustomInvoicesQuery(_url, _accessKey);

            _GetCustomInvoicesVms = new ObservableCollection<GetCustomInvoicesVm>(query.GetData());
        }

        public ObservableCollection<GetCustomInvoicesVm> GetCustomInvoicesVms
        {
            get { return _GetCustomInvoicesVms; }
        }
    }
}
