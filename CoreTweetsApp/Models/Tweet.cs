using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTweetsApp.Models
{
    public class Tweet
    {
        public Int64 id { get; set; }
        public DateTime stamp { get; set; }
        public string text { get; set; }
    }
}
