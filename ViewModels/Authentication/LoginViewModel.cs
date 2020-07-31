using Caliburn.Micro;
using Paywall.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Paywall.Presentation.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region properties

        private string _pin;


        public string Pin
        {
            get { return _pin; }
            set 
            { 
                _pin = value;
                NotifyOfPropertyChange(() => this.Pin);
            }
        }

        #endregion

        #region commands

        public void Login()
        {
            try
            {
                var command = new LoginCommand(_url, _accessKey);

                //Run the login command which will return a 500 response 
                //for anything other then login success
                var serviceAdvisorDetails = command.Login(Pin);

                App.Current.Properties["ServiceAdvisorApiKey"] = serviceAdvisorDetails.ApiKey;
                App.Current.Properties["ServiceAdvisorDispayName"] = serviceAdvisorDetails.DisplayName;

                IWindowManager windowManager = new WindowManager();
                var mainWindow = new MainWindowViewModel();
                windowManager.ShowWindow(mainWindow, null, null);

                this.TryClose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(App.Current.MainWindow, "Incorrect Pin Code");
            }
        }

        public void ClearPin()
        {
            Pin = "";
        }

        public bool CanEnterPinOne()
        {
            return true;
        }

        public void EnterPinOne(string pinChar = "1")
        {
            Pin = string.Concat(Pin, pinChar);
        }
        #endregion
    }

}
