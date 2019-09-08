using AppModels;
using DataAccess.Interface;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper;

namespace CronJob
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckForUpdates();
        }

        static void CheckForUpdates()
        {
            string ndnInterestURL = string.Concat("https://www.lists.cs.ucla.edu/pipermail/ndn-interest/", DateTime.Now.Year.ToString(), "-", DateTime.Now.ToString("MMMM"), "/");
            UpdateNDNInterest(ndnInterestURL);

            // add other mailing list code here 
        }

        static void UpdateNDNInterest(string monthURL)
        {
            try
            {
                MailingListScrapper mailingListScrapper = new MailingListScrapper();
                List<string> urlsInSingleMonth = mailingListScrapper.FetchMontlyURLs(monthURL);

                List<Message> messages = new List<Message>();
                foreach (var item in urlsInSingleMonth)
                {
                    messages.Add(mailingListScrapper.FetchPageContent(item));
                }

                List<DataAccess.NDNInterest> databaseMessages = new List<DataAccess.NDNInterest>();

                foreach (var item in messages)
                {
                    databaseMessages.Add(new DataAccess.NDNInterest()
                    {
                        Title = item.Title,
                        FromEmail = item.FromEmail,
                        FromName = item.FromName,
                        PostedDate = item.PostedDate,
                        MessageText = item.MessageText,
                        PageURL = item.PageURL,
                        CreatedDate = DateTime.Now,
                    });
                }

                INDNInterestRespository _ndnInterestRepository = new NDNInterestRespository();
                foreach (var item in databaseMessages)
                {
                    DataAccess.NDNInterest databaseMessage = _ndnInterestRepository.GetBy(x => x.Title == item.Title && x.PostedDate == item.PostedDate && x.FromEmail == item.FromEmail && x.FromName == item.FromName && x.PageURL == item.PageURL).FirstOrDefault();
                    if (databaseMessage == null)
                    {
                        _ndnInterestRepository.Add(item);
                        _ndnInterestRepository.Save();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
