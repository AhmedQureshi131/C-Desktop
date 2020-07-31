using Newtonsoft.Json;
using Paywall.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Application.Invoice.Queries
{
    public class GetOpenRepairOrderInvoicesQuery : BaseTransaction
    {
        public GetOpenRepairOrderInvoicesQuery(string url, string accessKey) : base(url, accessKey)
        {
        }

        public List<GetOpenRepairOrderInvoicesVm> GetData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(String.Concat(_url, "/gs-invoice/getOpenRoInvoices")).Result;
            if (response != null)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<GetOpenRepairOrderInvoicesVm>>(responseBody);
            }

            throw new FailedResponseException();
        }
    }
}

