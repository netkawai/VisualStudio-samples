using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace WpfApp1
{
    public class HttpHelper
    {
        public static HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            return client; 
        }
    }
}
