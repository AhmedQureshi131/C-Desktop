using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Presentation.ViewModels.Payment
{
    public class PaymentProgressViewModel : ViewModelBase
    {
        private string _status;

        //This is bound to a text input that will show the status of the transaction
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                NotifyOfPropertyChange(() => this.Status);
            }
        }
    }
}
