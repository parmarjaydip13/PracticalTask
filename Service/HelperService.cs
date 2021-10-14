using Service.Interface;
using System;
using System.Web.Helpers;

namespace Service
{
    public class HelperService : IHelperService
    {

        //Currenlty using simple hash
        public string GetHash(string input)
        {
            try
            {
                return Crypto.HashPassword(input);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool VerifiedHash(string hash, string input)
        {
            try
            {
                var result = Crypto.VerifyHashedPassword(hash, input);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
