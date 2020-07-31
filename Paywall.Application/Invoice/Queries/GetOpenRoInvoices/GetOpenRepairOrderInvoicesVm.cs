using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Application.Invoice.Queries
{
    public class GetOpenRepairOrderInvoicesVm
    {
        public string OrderNumber { get; set; }

        public string CustomerName { get; set; }

        public decimal Amount { get; set; }

        public string Vehicle { get; set; }

        public string Cellphone { get; set; }

        public Guid InvoiceId { get; set; }
    }
}
