using System;
using Paywall.Application.Invoice.Queries;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Presentation.ViewModels
{
    class OpenRepairOrdersViewModel : ViewModelBase
    {
        private ObservableCollection<GetOpenRepairOrderInvoicesVm> _GetOpenRepairOrderInvoicesVms;

        public OpenRepairOrdersViewModel()
        {
            var query = new GetOpenRepairOrderInvoicesQuery(_url, _accessKey);

            _GetOpenRepairOrderInvoicesVms = new ObservableCollection<GetOpenRepairOrderInvoicesVm>(query.GetData());
        }

        public ObservableCollection<GetOpenRepairOrderInvoicesVm> GetOpenRepairOrderInvoicesVms
        {
            get { return _GetOpenRepairOrderInvoicesVms; }
        }
    }
}
