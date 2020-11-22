using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace L5_ParallelWindowsFormsCrawler
{
    public partial class Form1 : Form
    {
        BindingSource resultBindingSource = new BindingSource();
        Crawler crawler = new Crawler();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = resultBindingSource;
            crawler.PageDownloaded += Crawler_PageDownloaded;
            crawler.CrawlerStopped += Crawler_CrawlerStopped;
        }

        private void Crawler_CrawlerStopped(Crawler obj)
        {
            label1.Text = "爬虫已停止!";
        }

        private void Crawler_PageDownloaded(Crawler crawler, int ExploredCount, string ExplorerUrl, string Status)
        {
            resultBindingSource.Add(new { Index = ExploredCount, Url = ExplorerUrl, Status = Status });
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startUrl = textBox1.Text;
            crawler.IsUrlExplored.Add(startUrl, false);
            crawler.WaitingToExploreQueue.Enqueue(startUrl);
            label1.Text = "爬虫已开始";
            new Thread(crawler.Crawl).Start();
        }
    }
}
