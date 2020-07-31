using Newtonsoft.Json;
using Paywall.Application.Common.Exceptions;
using Paywall.Application.Payment.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Paywall.Application.Commands
{
    //This is used to login the service advisor using his pin code
    public class LoginCommand : BaseTransaction
    {
        public LoginCommand(string url, string accessKey) : base(url, accessKey)
        {
        }

        public LoginVm Login(string pin)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Setup the parameters that get passed to login the service advisor
            var parameters = new Dictionary<string, string> { { "pin", pin }, { "apiKey",_accessKey } };
            //Url encode to be sure however it will always just be the pin code and access key
            var encodedContent = new FormUrlEncodedContent(parameters);

            HttpResponseMessage response = client.PostAsync(String.Concat(_url, "/gs-user/loginServiceAdvisorWithPin"), encodedContent).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                LoginVm loginVm = JsonConvert.DeserializeObject<LoginVm>(responseBody);

                //Get the concat string of encrypted username and iv string
                //TODO should just assign the decoded values to a variable
                //var username = HttpUtility.UrlDecode(loginVm.UserName).Split(':').First();
                //var password = HttpUtility.UrlDecode(loginVm.Password).Split(':').First();
                //var ivUsername = HttpUtility.UrlDecode(loginVm.UserName).Split(':').Last();
                //var ivPassword = HttpUtility.UrlDecode(loginVm.Password).Split(':').Last();

                //Decrypt and override the username and pass on the VM
                //loginVm.UserName = Decrypt(username, ivUsername);
                //loginVm.Password = Decrypt(password, ivPassword);

                return loginVm;
            }

            throw new FailedResponseException();
        }

        public string Decrypt(string cipherData, string ivString)
        {
            SHA256 mySHA256 = SHA256.Create();

            byte[] key = mySHA256.ComputeHash(Encoding.UTF8.GetBytes("kjh34$5k^j3h4&5kjh3#$4k5jh3@#4k5k3j4&5hjk345"));// Convert.FromBase64String("kjh34$5k^j3h4&5kjh3#$4k5jh3@#4k5k3j4&5hjk345");
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);

            byte[] iv = Convert.FromBase64String(ivString);

            try
            {
                using (var rijndaelManaged =
                       new RijndaelManaged { Key = aesKey, IV = iv, Mode = CipherMode.CBC, BlockSize = 128 })
                using (var memoryStream =
                       new MemoryStream(Convert.FromBase64String(cipherData)))
                using (var cryptoStream =
                       new CryptoStream(memoryStream,
                           rijndaelManaged.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            // You may want to catch more exceptions here...
        }
    }
}
