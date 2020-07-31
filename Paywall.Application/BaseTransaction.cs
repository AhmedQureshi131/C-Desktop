using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace Paywall.Application
{
    public abstract class BaseTransaction
    {
        protected string _url;
        protected string _accessKey;

        public BaseTransaction(string url, string accessKey)
        {
            _url = url;
            _accessKey = accessKey;
        }
    }
}
