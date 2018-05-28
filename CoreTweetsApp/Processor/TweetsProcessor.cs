using CoreTweetsApp.Helpers;
using CoreTweetsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTweetsApp.Processor
{
    /// <summary>
    /// This class is to fetch the tweets from the api and process the same
    /// </summary>
    public class TweetsProcessor
    {
        private const string APIUrl = "https://badapi.iqvia.io/api/v1/Tweets?startDate={0}&endDate={1}";

        /// <summary>
        /// Processing all the tweets
        /// Getting the first 100 records and then setting the new start date as the max stamp that we have received in the response.
        /// calling the Api in loop till we get no records
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<Tweet> ProcessTweets(string startDate, string endDate)
        {
            var finalList = new List<Tweet>();
            List<Tweet> tweetList = GetData(DateTime.Parse(startDate),DateTime.Parse(endDate));
            finalList.AddRange(tweetList);

            // running only when we have got 100 records ,as it suggests we may get more records in the next call
            // if less then hundred then dont do any thing
            while (tweetList.Count != 0 && tweetList.Count==100)
            {
                //adding 1 second to the max date,so that we dont get the same record again and again, and rather
                var newStartDateItem = tweetList.LastOrDefault().id;
                var newStartDate =tweetList.LastOrDefault().stamp;
                tweetList = GetData(newStartDate, DateTime.Parse(endDate));
                finalList.AddRange(tweetList);
            }

           
            return finalList;
        }

        /// <summary>
        /// This method fetches the data from the api and parse the JSON
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<Tweet> GetData(DateTime startDate, DateTime endDate)
        {
            List<Tweet> lstTweets = new List<Tweet>();
            //formating the url and fetching the data
            var responseString = ApiHelper.FetchAPIData(string.Format(APIUrl, FormatDate(startDate), FormatDate(endDate)));
            Newtonsoft.Json.Linq.JArray json = Newtonsoft.Json.Linq.JArray.Parse(responseString);
            lstTweets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tweet>>(responseString);
            return lstTweets.OrderBy(x => x.stamp).ToList();
        }

        /// <summary>
        /// This method formats the date as per the format provided in the API call
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string FormatDate(DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ss") + ".271Z";
        }


    }

    public class ItemEqualityComparer : IEqualityComparer<Tweet>
    {
        public bool Equals(Tweet x, Tweet y)
        {
            // Two items are equal if their keys are equal.
            return x.id == y.id;
        }

        public int GetHashCode(Tweet obj)
        {
            return obj.id.GetHashCode();
        }
    }


}
