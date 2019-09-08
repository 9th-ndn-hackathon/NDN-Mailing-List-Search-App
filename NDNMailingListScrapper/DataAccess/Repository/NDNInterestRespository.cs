
using DataAccess.Interface;
using PhysioOnline.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class NDNInterestRespository : GenericRepository<NDNMailingLIstSearchAppEntities, NDNInterest>, INDNInterestRespository
    {
    }
}
