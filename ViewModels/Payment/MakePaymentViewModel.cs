using ANetEmvDesktopSdk;
using ANetEmvDesktopSdk.Common;
using ANetEmvDesktopSdk.Models;
using ANetEmvDesktopSdk.SDK;
using ANetEmvDesktopSdk.Services;
using ANetEmvDesktopSdk.UI;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using Paywall.Application.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace Paywall.Presentation.ViewModels.Payment
{
    /**
     * This will be used for reading the transaction from the machine.
     * Once the transaction is completed mark the payment on paywall
     */
    public class MakePaymentViewModel : ViewModelBase,SdkListener
    {
        #region properties

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

        private decimal _amount;

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => this.Amount);
            }
        }

        private string _invoiceNumber;

        //This is bound to a text input that will show the status of the transaction
        public string InvoiceNumber
        {
            get { return _invoiceNumber; }
            set
            {
                _invoiceNumber = value;
                NotifyOfPropertyChange(() => this.InvoiceNumber);
            }
        }


        public static string _merchantName = null;
        public static string _merchantID = null;
        private string _sessionToken = null;
        private string _deviceID = "123456789WINSDK";
        private string _currencyCode = "840";
        private string _terminalID = "00222273";
        #endregion

        mobileDeviceLoginResponse response;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLogin;
        private SdkLauncher launcher = null;
        private MakePaymentCommand _command;
        IWindowManager _windowManager;
        MainWindowViewModel _popUpStatus;

        public MakePaymentViewModel()
        {
            _command = new MakePaymentCommand(_merchantName, _merchantID, _sessionToken, _deviceID, _currencyCode, _terminalID);
            _windowManager = new WindowManager();
            _popUpStatus = new MainWindowViewModel();

            //Setup the tasks that will run for the different events 
            backgroundWorkerLogin = new BackgroundWorker();
            this.backgroundWorkerLogin.DoWork += new DoWorkEventHandler(this.backgroundWorkerLogin_GetAuthenticationToken);
            this.backgroundWorkerLogin.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorkerLogin_GetAuthenticationTokenProgress);
            this.backgroundWorkerLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorkerLogin_GetAuthenticationTokenCompleted);
        }

        public void MakePayment()
        {
            this.launcher.setTerminalMode( TerminalMode.Insert_or_swipe);
            this.launcher.setConnection(ConnectionMode.USB);
            this.launcher.startEMVTransaction(_command.getRequest(Amount,InvoiceNumber), SDKTransactionType.GOODS, this);
        }

        private void backgroundWorkerLogin_GetAuthenticationToken(object sender, DoWorkEventArgs e)
        {
            string[] workerArguments = e.Argument as string[];

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = workerArguments[0],
                Item = workerArguments[1],
                ItemElementName = ItemChoiceType.password
            };

            //For testing just use sandbox update to productoin when we are ready to deploy
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            mobileDeviceLoginRequest request = new mobileDeviceLoginRequest()
            {
                merchantAuthentication = new merchantAuthenticationType()
                {
                    name = workerArguments[0],
                    Item = workerArguments[1],
                    mobileDeviceId = workerArguments[2],
                    ItemElementName = ItemChoiceType.password
                }
            };

            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                //Show the status window 
                _windowManager.ShowWindow(_popUpStatus, null, null);
                mobileDeviceLoginController controller = new mobileDeviceLoginController(request);
                e.Result = controller.ExecuteWithApiResponse();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Execption Message" + exception.Message);
            }
            finally
            {

            }
        }

        private void backgroundWorkerLogin_GetAuthenticationTokenProgress(object sender, ProgressChangedEventArgs e)
        {           
           
        }

        private void backgroundWorkerLogin_GetAuthenticationTokenCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mobileDeviceLoginResponse response = e.Result as mobileDeviceLoginResponse;

            if (!object.ReferenceEquals(response, null) && !object.ReferenceEquals(response.sessionToken, null))
            {
                this.response = response;
                Debug.WriteLine("RunnerBackground completed" + response.sessionToken);
                _popUpStatus.TryClose();
                //this.Hide();

                try
                {
                    //This is when the transaction is completed we need to show some kind of screen to show the payment is done
                    //Needs to make a call to mark the payment as completed

                    /*App.Current.Invoke(() =>
                    {
                        MainController.merchantName = this.response.merchantContact.merchantName;  // optional
                        MainController.merchantID = "********0108 "; // optional
                        mainController = new MainController(this.sdkEnvironment,
                            "840",
                            "00222273",
                            this.skipSignatureValue,
                            this.showReceiptValue,
                            "123456789WINSDK",
                            this.response.sessionToken);
                        Application.Current.MainWindow = mainController;
                        this.Close();
                        mainController.ShowDialog();
                    });*/
                }
                catch
                (Exception exception)
                {
                    Debug.Write("Exception in main window" + exception.Message + " ");
                }
                finally
                {
                }
            }
            else
            {

            }
        }

        /*
         * This method updates the status var which is bound to the UI
         */
        private void updateTransactionStatus(TransactionStatus iStatus)
        {
            Debug.Write("updateTransactionStatus" + iStatus);
            switch (iStatus)
            {
                case TransactionStatus.NoUSBSwiperDeviceConnected:
                    Status = "No swiper device connected.";
                    break;
                case TransactionStatus.WaitingForCard:
                    Status = "Please insert or swipe card.";
                    break;
                case TransactionStatus.CardReadError:
                    Status = "Could not read the card, please remove the card and press ok to try again.";
                    break;
                case TransactionStatus.RetrySwipe:
                    Status = "Please swipe again.";
                    break;
                case TransactionStatus.SwipeCard:
                    Status = "Please swipe..";
                    break;
                case TransactionStatus.ICCCard:
                    Status = "Please insert the card.";
                    break;
                case TransactionStatus.NotICCCard:
                    Status = "Non Chip Card, please swipe.";
                    break;
                case TransactionStatus.ProcessingTransaction:
                    Status = "Processing transaction...";
                    break;
                case TransactionStatus.RemoveCard:
                    Status = "Please remove the card";
                    break;
            }
        }

        private void selectApplicationAction(object sender, ApplicationSelectedEventArgs e)
        {
            Debug.Write("MainController:selectApplicationAction");
            this.launcher.selectApplication(e.SelectedApplication);
        }

        private void cancelApplicationSelectionAction(object sender, EventArgs e)
        {
            Debug.Write("MainController:cancelApplicationSelectionAction");
            this.launcher.cancelSelectApplication();
        }

        public void transactionCompleted(createTransactionResponse response, bool isSuccess, string customerSignature, ErrorResponse errorResponse)
        {
            throw new NotImplementedException();
        }

        public void transactionStatus(TransactionStatus iTransactionStatus)
        {
            throw new NotImplementedException();
        }

        public void transactionCanceled()
        {
            throw new NotImplementedException();
        }

        public void hideCancelTransaction()
        {
            throw new NotImplementedException();
        }

        void SdkListener.processCardProgress(TransactionStatus iProgress)
        {
            Debug.Write("MainController:processCardProgress" + "\n" + iProgress);
            this.updateTransactionStatus(iProgress);
        }

        public void processCardCompletedWithStatus(bool iStatus)
        {
            throw new NotImplementedException();
        }

        public void requestSelectApplication(List<string> appList)
        {
            Debug.Write("MainController:requestSelectApplication");
            ApplicationSelectionWindow applicationSelectionWindow = new ApplicationSelectionWindow(appList);
            applicationSelectionWindow.selectionApplicationEvent += new ApplicationSelectionEventHandler(selectApplicationAction);
            applicationSelectionWindow.cancelEvent += new SDKEventHandler(cancelApplicationSelectionAction);
            applicationSelectionWindow.ShowDialog();
        }

        public void readerDeviceInfo(Dictionary<string, string> iDeviceInfo)
        {
            throw new NotImplementedException();
        }

        public void OTAUpdateRequired(Tuple<OTAUpdateResult, OTAUpdateResult> iCheckUpdateStatus, string iErrorMessage)
        {
            throw new NotImplementedException();
        }

        public void OTAUpdateProgress(double iPercentage, OTAUpdateType iOTAUpdateType)
        {
            throw new NotImplementedException();
        }

        public void OTAUpdateCompleted(Tuple<OTAUpdateResult, OTAUpdateResult> iUpdateStatus, string iErrorMessage)
        {
            throw new NotImplementedException();
        }

        public void BTPairedDevicesScanResult(List<BTDeviceInfo> iPairedDevicesList)
        {
            throw new NotImplementedException();
        }

        public void BTConnected(BTDeviceInfo iDeviceInfo)
        {
            throw new NotImplementedException();
        }

        public void BTConnectionFailed()
        {
            throw new NotImplementedException();
        }

        public void isAugustaReaderDeviceConnected(Dictionary<string, string> iDeviceInfo)
        {
            throw new NotImplementedException();
        }
    }
}
