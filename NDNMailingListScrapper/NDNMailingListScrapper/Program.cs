using AppModels;
using DataAccess.Interface;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper;

namespace NDNMailingListScrapper
{
    class Program
    {
        static void Main(string[] args)
        {

            // AddNDNInterestMailingList("https://www.lists.cs.ucla.edu/pipermail/ndn-interest/"); // Messages from this mailing list have been fectched, no need to run this line again 

            // add other mailing list code here 


            Console.ReadKey();
        }
        static void AddNDNInterestMailingList(string url)
        {
            try
            {
                MailingListScrapper mailingListScrapper = new MailingListScrapper();
                List<Message> messages = mailingListScrapper.FetchCompleteMailingList(url);

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
