using DataAccess.Interface;
using DataAccess.Repository;
using Models.DatabaseModels;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Operation
{
    public class NDNInterestService : INDNInterestsService
    {
        INDNInterestRepository _ndnInterestsRepository;

        public NDNInterestService()
        {
            _ndnInterestsRepository = new NDNInterestRepository();
        }

        #region NDNInterest

        #region Get

        public List<NDNInterest> GetAllNDNInterest()
        {
            try
            {
                return _ndnInterestsRepository.GetAll();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
                return null;
            }
        }
        public List<NDNInterest> Search(string keyword)
        {
            try
            {
                return _ndnInterestsRepository.GetBy(x => x.FromEmail.Contains(keyword)||x.FromName.Contains(keyword)||x.MessageText.Contains(keyword)||x.PostedDate.Contains(keyword)||x.Title.Contains(keyword)).ToList();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
                return null;
            }
        }

        public NDNInterest GetNDNInterestBy(int id)
        {
            try
            {
                return _ndnInterestsRepository.GetBy(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
                return null;
            }
        }

        public List<NDNInterest> GetNDNInterestAfter(int id)
        {
            try
            {
                return _ndnInterestsRepository.GetBy(x => x.Id > id).ToList();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
                return null;
            }
        }

        #endregion

        #region Add

        public void AddNDNInterest(NDNInterest ndnInterest)
        {
            try
            {
                ndnInterest.CreatedDate = DateTime.UtcNow;
                _ndnInterestsRepository.Add(ndnInterest);
                _ndnInterestsRepository.Save();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
            }
        }


        #endregion

        #region Edit

        public bool EditNDNInterest(NDNInterest ndnInterest)
        {
            try
            {
                _ndnInterestsRepository.Edit(ndnInterest);
                _ndnInterestsRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                //LogHelper.WriteExceptionLog(ex);
                return false;
            }
        }

        #endregion

        #region Delete

        public bool DeleteNDNInterest(NDNInterest ndnInterest)
        {
            try
            {
                _ndnInterestsRepository.Delete(ndnInterest);
                _ndnInterestsRepository.Save();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

   





        #endregion

        #endregion




    }
}
