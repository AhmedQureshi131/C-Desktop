using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Paywall.Presentation.ViewModels
{
    public class ViewModelBase : Screen
    {
        protected string _url;
        protected string _accessKey;

        public ViewModelBase()
        {
            _url = ConfigurationManager.AppSettings["WebsiteUrl"];
            _accessKey = ConfigurationManager.AppSettings["AccessKey"];
        }
    }
}
