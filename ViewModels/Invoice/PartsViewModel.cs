using Paywall.Application.Invoice.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Presentation.ViewModels
{
    class PartsViewModel : ViewModelBase
    {
        private ObservableCollection<GetPartsInvoicesVm> _GetPartsInvoicesVms;

        public PartsViewModel()
        {
            var query = new GetPartsInvoicesQuery(_url, _accessKey);

            _GetPartsInvoicesVms = new ObservableCollection<GetPartsInvoicesVm>(query.GetData());
        }

        public ObservableCollection<GetPartsInvoicesVm> GetPartsInvoicesVms
        {
            get { return _GetPartsInvoicesVms; }
        }
    }
}
