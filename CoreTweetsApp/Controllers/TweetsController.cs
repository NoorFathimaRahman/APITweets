using CoreTweetsApp.Models;
using CoreTweetsApp.Processor;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTweetsApp.Controllers
{
    public class TweetsController: Controller
    {
        private readonly TweetsProcessor _tweetsProcessor;
        public TweetsController(TweetsProcessor tweetsProcessor)
        {
            _tweetsProcessor = tweetsProcessor;
        }

        /// <summary>
        /// get method to load the view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new TweetsViewModel());
        }

        /// <summary>
        /// post method to get the list of tweets from the API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(TweetsViewModel model)
        {
            var tweetsData = _tweetsProcessor.ProcessTweets(model.StartDate, model.EndDate);
            var distinctRecords= tweetsData.Distinct(new ItemEqualityComparer());

            var tweetsViewModel = new TweetsViewModel()
            {
                Tweets = distinctRecords.ToList(),
                Count = tweetsData.ToList().Count,
                DuplicateCount= tweetsData.ToList().Count- distinctRecords.ToList().Count
            };
            return View(tweetsViewModel);
        }
    }
}
