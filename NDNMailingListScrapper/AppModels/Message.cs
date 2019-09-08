using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModels
{
    public class Message
    {
        public string Title { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string PostedDate { get; set; }
        public string MessageText { get; set; }
        public string PageURL { get; set; }
    }
}
