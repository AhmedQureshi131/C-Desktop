using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Application.Invoice.Queries
{
    public class GetPartsInvoicesVm
    {
        public string Description { get; set; }

        public string CustomerName { get; set; }

        public string PartNumber { get; set; }

        public decimal Amount { get; set; }

        public string PhoneNumber { get; set; }

        public string InvoiceFileName { get; set; }

        public Guid InvoiceId { get; set; }
    }
}
