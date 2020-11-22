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


namespace L5_ParallelWindowsFormsCrawler
{
    class Crawler
    {
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, int, string, string> PageDownloaded;

        //是一个dictionary
        //key记录已经知晓的网址
        //value记录key对应网址是否被访问过
        //所以把一个url作为键去访问哈希表的值会获得3种结果,分别对应url的3种状态
        //1. value=null, 该key是未知晓的url; 2. value=false, 该key是已知晓但未探索的url; 3. value=true, 该key是已知晓且已探索的url;
        public Dictionary<string, bool> IsUrlExplored = new Dictionary<string, bool>();
       
        //待探索队列
        public Queue<string> WaitingToExploreQueue = new Queue<string>();
        
        //是一个计数器,用于记录查询了几个网址
        public int ExploredCount = 0;
        
        //是一个界限,标识探索多少个网址后结束
        private const int ExploreTargetCount = 20;

        public void Crawl()
        {
            Console.WriteLine("开始爬行了!");

            while (true)
            {
                //从待访问网址里取排在字典序最后一个没被访问的url赋给current
                //访问过就直接新一轮for循环,没访问过就赋值current后再新一轮for循环,所以current前面赋的值的会被后面赋的值覆盖,这个本质上是一个从后往前遍历的url数组
                string ExplorerUrl = null;
                ExplorerUrl = WaitingToExploreQueue.Dequeue();
                while(IsUrlExplored[ExplorerUrl]&& WaitingToExploreQueue.Count>0)
                {
                    ExplorerUrl = WaitingToExploreQueue.Dequeue();
                }

                //进行爬取
                try
                {
                    Console.WriteLine($"{ExploredCount}: 正在爬取{ExplorerUrl}页面!");
                    string ExplorerHtml = DownLoad(ExplorerUrl);//获得对应网址的html文本内容
                    IsUrlExplored[ExplorerUrl] = true;//将它对应访问标志置高
                    ExploredCount++;//计数器++
                    Process(ExplorerHtml, ExplorerUrl);//处理这个网址的html文本内容
                    PageDownloaded(this, ExploredCount, ExplorerUrl, "success");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    PageDownloaded(this, ExploredCount, ExplorerUrl, "Failed:" + e.Message);

                }

                //退出条件: 包括遍历完已知晓数组后没有未探索的(都探索完了)||探索量达到预期
                if (WaitingToExploreQueue.Count == 0 || ExploredCount > ExploreTargetCount-1)
                    break;
            }

            Console.WriteLine("爬行结束!");
            CrawlerStopped(this);
        }

        public string DownLoad(string ExplorerUrl)
        {
            
                //通过WebClient去获得对应url的html文本内容
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string ExploreHtml = webClient.DownloadString(ExplorerUrl);
                //通过File类将这个网址的html文本内容存到本地
                string fileName = "html-" + ExploredCount;
                File.WriteAllText(fileName, ExploreHtml, Encoding.UTF8);
                return ExploreHtml;
            
        }

        public void Process(string html,string CurrentHtml_Url)
        {
            //用正则表达式获取html文本内容中属于网址的那部分,并进行处理
            string HtmlToUrlRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
            MatchCollection matches = new Regex(HtmlToUrlRegex).Matches(html);//将html中的网址剥离出来形成一个集合
            //一一做处理
            foreach (Match match in matches)
            {
                string FoundUrl = match.Groups["url"].Value;


                if (FoundUrl.Length == 0)//若为空就进行下一轮for循环
                {
                    continue;
                }

                //相对路径转绝对路径
                FoundUrl = GetAbsolutePath(FoundUrl, CurrentHtml_Url);

                //筛选页面类型, 当爬取的是html，htm，jsp，aspx、php页面时，才执行加入操作,否则就跳出本轮for,去看本html中的下一个url
                string UrlToDetailRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
                Match UrlDetails = Regex.Match(FoundUrl, UrlToDetailRegex);
                string file = UrlDetails.Groups["file"].Value;
                if(file=="")
                {
                    file = "index.html";
                }

                string FileFilter = @"((.html){1}$|(.htm){1}$|(.jsp){1}$|(.aspx){1}$|(.php){1}$)";

                if (!Regex.IsMatch(file,FileFilter))
                {
                    continue;
                }

                //去看看拿到的新网址的访问标志为什么

                if (!IsUrlExplored.Keys.Contains(FoundUrl))//如果新找到的网址未知
                {
                    //将它变为已知状态并加入等待探索队列
                    IsUrlExplored[FoundUrl] = false;
                    WaitingToExploreQueue.Enqueue(FoundUrl);
                }
                else if(IsUrlExplored[FoundUrl] == false)//如果新找到的网址已知,但未探索
                {
                    //什么也不做
                }
                else//如果新找到的网址已被探索
                {
                    //什么也不做
                }
            }
        }

        public string GetAbsolutePath(string RelativePath, string CurrentLocationPath)
        {

            string UrlToDetailRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
    
            if (RelativePath.Contains("://"))//如果本身就是绝对路径
            {
                //直接返回
                return RelativePath;
            }
            if (RelativePath.StartsWith("//"))//如果是绝对路径的依赖协议写法
            {
                //改成一般写法
                return "http:" + RelativePath;
            }
            if (RelativePath.StartsWith("/"))//如果是服务器根目录的相对路径
            {
                //把服务器根目录通过正则筛出来,在相对路径前补上服务器根目录的地址
                Match urlMatch = Regex.Match(CurrentLocationPath, UrlToDetailRegex);
                String site = urlMatch.Groups["site"].Value;
                return site.EndsWith("/") ? site + RelativePath.Substring(1) : site + RelativePath;
            }

            if (RelativePath.StartsWith("../"))//如果是父目录的相对路径
            {
                //把父目录通过'/'匹配筛出来,在相对路径前补上父目录的地址
                RelativePath = RelativePath.Substring(3);
        return GetAbsolutePath(RelativePath, CurrentLocationPath.Substring(0, CurrentLocationPath.LastIndexOf('/')));
            }

            if (RelativePath.StartsWith("./"))//如果是当前目录的相对路径
            {
                //在相对路径前补上当前目录的地址
                return GetAbsolutePath(RelativePath.Substring(2), CurrentLocationPath);
            }

            int end = CurrentLocationPath.LastIndexOf("/");
            return CurrentLocationPath.Substring(0, end) + "/" + RelativePath;
        }
    }
}

