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

using System.Collections.Concurrent;
using System.Windows.Forms;

namespace L5_WindowsFormsParallelCrawler
{
    class Crawler
    {
        public event Action<Crawler> CrawlerStopped;
        public event Action<Crawler, int, string, string> PageDownloaded;
        public ConcurrentDictionary<string, bool> IsUrlExplored = new ConcurrentDictionary<string, bool>();
        public ConcurrentQueue<string> WaitingToExploreQueue = new ConcurrentQueue<string>();
        List<Task> tasks = new List<Task>();
        public int ExploredCount = 0;
        private const int ExploreTargetCount = 100;
        public string StartUrl;

        public void Crawl()
        {
            IsUrlExplored.Clear();
            WaitingToExploreQueue.Enqueue(StartUrl);

            while (tasks.Count < ExploreTargetCount)
            {
                if (!WaitingToExploreQueue.TryDequeue(out string ExplorerUrl))
                {
                    if (ExploredCount < tasks.Count)
                    {
                        continue;
                    }
                    else
                    {
                       break;//所有任务都完成，队列无url
                    }
                }
                //进行爬取            
                    Task task = Task.Run(() => DownLoadAndProcess(ExplorerUrl));
                    tasks.Add(task);             
            }

            Task.WaitAll(tasks.ToArray());
            CrawlerStopped(this);
        }

        public void DownLoadAndProcess(string ExplorerUrl)
        {
            try
            {
                string ExplorerHtml = DownLoad(ExplorerUrl);//获得对应网址的html文本内容
                IsUrlExplored.TryUpdate(ExplorerUrl, true, true);//将它对应访问标志置高                        
                Process(ExplorerHtml, ExplorerUrl);//处理这个网址的html文本内容
                PageDownloaded(this, ExploredCount, ExplorerUrl, "success");
                ExploredCount++;//计数器++
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                PageDownloaded(this, ExploredCount, ExplorerUrl, "Failed:" + e.Message);
                ExploredCount++;//计数器++
            }

        }
        public string DownLoad(string ExplorerUrl)
        {

            //通过WebClient去获得对应url的html文本内容
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string ExploreHtml = webClient.DownloadString(ExplorerUrl);
            //通过File类将这个网址的html文本内容存到本地
            string fileName = "url-" + ExploredCount+".html";
            File.WriteAllText(fileName, ExploreHtml, Encoding.UTF8);
            return ExploreHtml;

        }

        public void Process(string html, string CurrentHtml_Url)
        {
            //用正则表达式获取html文本内容中属于网址的那部分,并进行处理
            string HtmlToUrlRegex = @"(href|HREF)\s*=\s*[""'](?<url>[^""'#>]+)[""']";
            MatchCollection matches = new Regex(HtmlToUrlRegex).Matches(html);//将html中的网址剥离出来形成一个集合
            //一一做处理
            foreach (Match match in matches)
            {
                string FoundUrl = match.Groups["url"].Value;


                if (FoundUrl.Length == 0||FoundUrl==null)//若为空就进行下一轮for循环
                {
                    continue;
                }

                //相对路径转绝对路径
                FoundUrl = GetAbsolutePath(FoundUrl, CurrentHtml_Url);

                //筛选页面类型, 当爬取的是html，htm，jsp，aspx、php页面时，才执行加入操作,否则就跳出本轮for,去看本html中的下一个url
                string UrlToDetailRegex = @"^(?<site>https?://(?<host>[\w\d.]+)(:\d+)?($|/))([\w\d]+/)*(?<file>[^#?]*)";
                Match UrlDetails = Regex.Match(FoundUrl, UrlToDetailRegex);
                string file = UrlDetails.Groups["file"].Value;
                if (file == "")
                {
                    file = "index.html";
                }

                string FileFilter = @"((.html){1}$|(.htm){1}$|(.jsp){1}$|(.aspx){1}$|(.php){1}$)";

                
                //如果新的网址未知且为指定文件类型,才加入探索队列
                if ((!IsUrlExplored.ContainsKey(FoundUrl))&& Regex.IsMatch(file, FileFilter))//如果新找到的网址未知
                {
                    //将它变为已知状态并加入等待探索队列
                    IsUrlExplored.TryAdd(FoundUrl, false);
                    WaitingToExploreQueue.Enqueue(FoundUrl);
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
