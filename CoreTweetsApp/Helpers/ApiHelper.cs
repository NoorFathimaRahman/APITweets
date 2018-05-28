using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTweetsApp.Helpers
{
    /// <summary>
    /// This is a helper class that fetches the date from the API url using the HTTPClient
    /// </summary>
    public class ApiHelper
    {
        /// <summary>
        /// fetching the data from the provided url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FetchAPIData(string url)
        {
            string responseString = string.Empty;
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        responseString = response.Content.ReadAsStringAsync().Result;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseString;
        }
    }
}
