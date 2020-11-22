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

namespace L5_WindowsFormsParallelCrawler
{
    public partial class Form1 : Form
    {
        BindingSource resultBindingSource = new BindingSource();
        Crawler crawler = new Crawler();
        Thread thread = null;

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = resultBindingSource;
            crawler.PageDownloaded += Crawler_PageDownloaded;
            crawler.CrawlerStopped += Crawler_CrawlerStopped;
        }

        private void Crawler_CrawlerStopped(Crawler obj)
        {
            Action action = () => label1.Text = "爬虫已停止";
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void Crawler_PageDownloaded(Crawler crawler, int ExploredCount, string ExplorerUrl, string Status)
        {
            
            Action action = () => { resultBindingSource.Add(new { Index = ExploredCount, Url = ExplorerUrl, Status = Status }); };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultBindingSource.Clear();
            crawler.StartUrl = textBox1.Text;

            if (thread != null)
            {
                thread.Abort();
            }
            thread = new Thread(crawler.Crawl);
            thread.Start();
            label1.Text = "爬虫已开始";
        }


    }
}
