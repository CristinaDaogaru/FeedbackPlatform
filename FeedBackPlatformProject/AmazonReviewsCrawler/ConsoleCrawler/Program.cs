using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            ReviewsCrawler.CrawlerHelper.GetReviews("B0018RSEMU", 6);
        }
    }
}
