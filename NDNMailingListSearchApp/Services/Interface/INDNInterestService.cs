using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface INDNInterestsService
    {
        List<NDNInterest> GetAllNDNInterest();


        NDNInterest GetNDNInterestBy(int id);

        List<NDNInterest> GetNDNInterestAfter(int id);

        void AddNDNInterest(NDNInterest ndnInterest);

        bool EditNDNInterest(NDNInterest ndnInterest);

        bool DeleteNDNInterest(NDNInterest ndnInterest);

        List<NDNInterest> Search(string keyword);
    }
}
