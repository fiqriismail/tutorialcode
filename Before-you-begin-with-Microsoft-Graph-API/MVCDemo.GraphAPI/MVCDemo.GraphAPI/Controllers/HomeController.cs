using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace MVCDemo.GraphAPI.Controllers
{
    public class HomeController : Controller
    {

        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        

        [Authorize]
        public async Task<ActionResult> Users()
        {
            string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

            AuthenticationContext authContext = new AuthenticationContext(authority);
            AuthenticationResult result = null;

            try
            {
                result = await authContext.AcquireTokenAsync("https://graph.microsoft.com",
                    new ClientCredential(clientId, appKey));
            }
            catch (Exception)
            {

                throw;
            }

            //Now call the Graph API
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/users");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            HttpResponseMessage response = await client.SendAsync(request);

            string output = await response.Content.ReadAsStringAsync();

            ViewBag.Result = output;


            return View();
        }
    }
}