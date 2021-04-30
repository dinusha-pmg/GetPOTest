using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Get.Po.Tester
{
    class Program

    {
        static void Main(string[] args)
        {
            //string apiUrlWithParams = "https://cw-po-api-osb-dev.azurewebsites.net/api/purchase-orders?PONo=94122";
            string apiUrlWithParams = "https://integrations-dev.cushwake.com/po-api-dev/api/purchase-orders?PONo=94122";
           
                //Token Generation code
             RunAsync().ConfigureAwait(false);
            
            Console.WriteLine($"API URL={apiUrlWithParams}");
            for (int i = 0; i < 3; i++)
            {
                PoManager.MainAsyncFromApim(apiUrlWithParams, i + 1);
            }
            Console.WriteLine("Done");
            Console.Read();
        }

       

        private static async Task RunAsync()
        {
            AuthenticationConfig config = new AuthenticationConfig();

            config.Instance = "https://login.microsoftonline.com/{0}/oauth2/token";
            config.Tenant = "46c5178e-a0f4-4f4d-8c40-9598e3d11860";
            config.ClientId = "6731de76-14a6-49ae-97bc-6eba6914391e";
            config.ClientSecret = "D-5rEw.~6Ry8lt.X_013-8xcq9h74aQhCt";
            //config.Authority = "";

            // You can run this sample using ClientSecret or Certificate. The code will differ only when instantiating the IConfidentialClientApplication
            //bool isUsingClientSecret = AppUsesClientSecret(config);

            // Even if this is a console application here, a daemon application is a confidential client application
            IConfidentialClientApplication app;

            //if (isUsingClientSecret)
            //{
                app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
                    .WithClientSecret(config.ClientSecret)
                    .WithAuthority(new Uri(config.Authority))
                    .Build();
            //}

            //else
            //{
            //    X509Certificate2 certificate = ReadCertificate(config.CertificateName);
            //    app = ConfidentialClientApplicationBuilder.Create(config.ClientId)
            //        .WithCertificate(certificate)
            //        .WithAuthority(new Uri(config.Authority))
            //        .Build();
            //}

            // With client credentials flows the scopes is ALWAYS of the shape "resource/.default", as the 
            // application permissions need to be set statically (in the portal or by PowerShell), and then granted by
            // a tenant administrator. 
            string[] scopes = new string[] { $"{config.ApiUrl}.default" };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(scopes)
                    .ExecuteAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Token acquired");
                Console.ResetColor();
            }
            catch (MsalServiceException ex) //when (ex.Message.Contains("AADSTS70011"))
            {
                // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
                // Mitigation: change the scope to be as expected
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            if (result != null)
            {
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine($"AuthToken:{JsonConvert.SerializeObject(result)}");
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
        }

        /// <summary>
        /// Display the result of the Web API call
        /// </summary>
        /// <param name="result">Object to display</param>
        private static void Display(JObject result)
        {
            foreach (JProperty child in result.Properties().Where(p => !p.Name.StartsWith("@")))
            {
                Console.WriteLine($"{child.Name} = {child.Value}");
            }
        }

        /// <summary>
        /// Checks if the sample is configured for using ClientSecret or Certificate. This method is just for the sake of this sample.
        /// You won't need this verification in your production application since you will be authenticating in AAD using one mechanism only.
        /// </summary>
        /// <param name="config">Configuration from appsettings.json</param>
        /// <returns></returns>
        //private static bool AppUsesClientSecret(AuthenticationConfig config)
        //{
        //    string clientSecretPlaceholderValue = "[Enter here a client secret for your application]";
        //    string certificatePlaceholderValue = "[Or instead of client secret: Enter here the name of a certificate (from the user cert store) as registered with your application]";

        //    if (!String.IsNullOrWhiteSpace(config.ClientSecret) && config.ClientSecret != clientSecretPlaceholderValue)
        //    {
        //        return true;
        //    }

        //    else if (!String.IsNullOrWhiteSpace(config.CertificateName) && config.CertificateName != certificatePlaceholderValue)
        //    {
        //        return false;
        //    }

        //    else
        //        throw new Exception("You must choose between using client secret or certificate. Please update appsettings.json file.");
        //}

    }
}
