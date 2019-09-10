using AppModels;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public class MailingListScrapper
    {
        #region public

        public List<Message> FetchCompleteMailingList(string url)
        {
            List<Message> messages = new List<Message>();
            // mailing list url
            List<string> mailingListURLS = FetchMailingListMonths(url);
            foreach (var monthURL in mailingListURLS)
            {
                List<string> urlsInSingleMonth = FetchMontlyURLs(monthURL);
                foreach (var item in urlsInSingleMonth)
                {
                    messages.Add(FetchPageContent(item));
                }
            }

            return messages;
        }

        public List<string> FetchMontlyURLs(string monthURL)
        {
            try
            {
                List<string> urls = new List<string>();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(monthURL);
                HtmlNodeCollection nodeCollection = document.DocumentNode.SelectSingleNode("//body").ChildNodes;
                List<HtmlNode> allUlList = nodeCollection.Where(x => x.Name == "ul").ToList();
                if (allUlList.Count > 1)
                {
                    HtmlNode urlsULNode = allUlList[1]; // fetching ul
                    foreach (var liItem in urlsULNode.ChildNodes.Where(x => x.Name == "li").ToList())
                    {
                        HtmlNode urlNode = liItem;
                        urls.Add(liItem.FirstChild.Attributes["href"].Value);
                        if (liItem.ChildNodes.FirstOrDefault(x => x.Name == "ul") != null)
                        {

                            RecursiveFetchMontlyURL(liItem, urls);
                        }
                    }
                }

                // construct full urls
                for (int i = 0; i < urls.Count; i++)
                {
                    urls[i] = monthURL.Substring(0, monthURL.LastIndexOf('/') + 1) + urls[i];
                    Console.WriteLine(urls[i]);
                }


                return urls;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        // Following code will fetch the content from the page for example: https://www.lists.cs.ucla.edu/pipermail/ndn-interest/2019-June/002473.html
        public Message FetchPageContent(string pageURL)
        {
            try
            {
                Message message = new Message();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(pageURL);
                HtmlNodeCollection nodeCollection = document.DocumentNode.SelectSingleNode("//body").ChildNodes;


                HtmlNode titleNode = nodeCollection.FirstOrDefault(x => x.Name == "h1");
                if (titleNode != null)
                {
                    message.Title = titleNode.InnerText;
                }
                HtmlNode fromNameNode = nodeCollection.FirstOrDefault(x => x.Name == "b");
                if (fromNameNode != null)
                {
                    message.FromName = fromNameNode.InnerText;
                }
                HtmlNode fromEmailNode = nodeCollection.FirstOrDefault(x => x.Name == "a");
                if (fromEmailNode != null)
                {
                    message.FromEmail = fromEmailNode.InnerText;
                }
                HtmlNode postedDateNode = nodeCollection.FirstOrDefault(x => x.Name == "i");
                if (postedDateNode != null)
                {
                    message.PostedDate = postedDateNode.InnerText;
                }
                HtmlNode messageNode = nodeCollection.FirstOrDefault(x => x.Name == "pre");
                if (messageNode != null)
                {
                    message.MessageText = messageNode.InnerText;
                }

                message.PageURL = pageURL;
                return message;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion

        #region private

        private void RecursiveFetchMontlyURL(HtmlNode urlsLINode, List<string> urls)
        {
            if (urlsLINode.ChildNodes.FirstOrDefault(x => x.Name == "ul") != null)
            {
                HtmlNode urlsULNode = urlsLINode.ChildNodes.FirstOrDefault(x => x.Name == "ul");
                foreach (var liItem in urlsULNode.ChildNodes.Where(x => x.Name == "li").ToList())
                {
                    HtmlNode urlNode = liItem;
                    urls.Add(liItem.FirstChild.Attributes["href"].Value);
                    if (liItem.ChildNodes.FirstOrDefault(x => x.Name == "ul") != null)
                    {
                        RecursiveFetchMontlyURL(liItem, urls);
                    }
                }

            }
            else
            {
                urls.Add(urlsLINode.FirstChild.Attributes["href"].Value);
            }
        }
        private List<string> FetchMailingListMonths(string mailingListURL)
        {
            try
            {
                List<string> urls = new List<string>();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(mailingListURL);
                HtmlNodeCollection nodeCollection = document.DocumentNode.SelectSingleNode("//body").ChildNodes;
                List<HtmlNode> TableList = nodeCollection.Where(x => x.Name == "table").ToList();
                foreach (var cell in document.DocumentNode.SelectNodes("//table/tr/td"))
                {
                    HtmlNode threadNode = cell.ChildNodes.FirstOrDefault(x => x.Name == "a" && x.InnerText == "[ Thread ]");
                    if (threadNode != null)
                    {
                        string url = threadNode.Attributes["href"].Value;
                        urls.Add(url);
                    }



                }

                // construct full urls
                for (int i = 0; i < urls.Count; i++)
                {
                    urls[i] = mailingListURL + urls[i];
                    Console.WriteLine(urls[i]);
                }

                return urls;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
