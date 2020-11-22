using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace L5_Crawler
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Crawler crawler = new Crawler();
            string startUrl = "http://www.cnblogs.com/";
            crawler.IsUrlExplored.Add(startUrl, false);
            crawler.WaitingToExploreQueue.Enqueue(startUrl);
            new Thread(crawler.Crawl).Start();

        }


    }
    
}
