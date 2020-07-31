using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paywall.Application
{
    public abstract class BaseCommand : BaseTransaction
    {
        protected string _serviceAdvisorApiKey;

        public BaseCommand(string url, string accessKey, string serviceAdvisorApiKey) : base(url, accessKey)
        {
            _serviceAdvisorApiKey = serviceAdvisorApiKey;
        }
    }
}
