using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyRSSReader
{
    class RssModel
    {
        public string Img { get; set; }

        public string Title { get; set; }

        public string PublishedDate { get; set; }

        public string Url { get; set; }

        public string MainSummary { get; set; }

        public RssModel(string title, string summary, string publishedDate, string url)
        {
            Title = title;
            PublishedDate = publishedDate;
            Url = url;
            //获取主要文本
            MainSummary = WebUtility.HtmlDecode(Regex.Replace(summary, "<[^>]+?>", ""));
            //获取图片地址
            Img = (summary.Remove(summary.IndexOf(".jpg") + 4)).Remove(0, summary.IndexOf('/', summary.IndexOf("img")) + 2);
        }
    }
}
