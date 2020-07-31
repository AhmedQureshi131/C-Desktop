using Caliburn.Micro;
using Paywall.Application.Payment.Queries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Presentation.ViewModels
{
    class LastSevenDaysPaymentsViewModel : ViewModelBase
    {
        private ObservableCollection<GetLastSevenDaysPaymentVm> _GetLastSevenDaysPaymentVms;

        public LastSevenDaysPaymentsViewModel()
        {
            var query = new GetLastSevenDaysPaymentsQuery(_url, _accessKey);

            _GetLastSevenDaysPaymentVms = new ObservableCollection<GetLastSevenDaysPaymentVm>(query.GetData());
        }

        public ObservableCollection<GetLastSevenDaysPaymentVm> GetLastSevenDaysPaymentVms
        {
            get { return _GetLastSevenDaysPaymentVms; }
        }

    }
}
