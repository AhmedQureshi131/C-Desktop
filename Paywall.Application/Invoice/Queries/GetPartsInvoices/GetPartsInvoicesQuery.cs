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
    public class GetPartsInvoicesQuery : BaseTransaction
    {
        public GetPartsInvoicesQuery(string url, string accessKey) : base(url, accessKey)
        {
        }

        public List<GetPartsInvoicesVm> GetData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(String.Concat(_url, "/gs-invoice/getPartsInvoices")).Result;
            if (response != null)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<GetPartsInvoicesVm>>(responseBody);
            }

            throw new FailedResponseException();
        }
    }
}
