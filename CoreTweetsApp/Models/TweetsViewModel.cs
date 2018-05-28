using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTweetsApp.Models
{
    public class TweetsViewModel
    {
        public TweetsViewModel()
        {
            Tweets = new List<Tweet>();
        }
        public List<Tweet> Tweets { get; set; }
        public int Count { get; set; }

        public int DuplicateCount { get; set; }

        [Required]
        public string StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }
    }
}
