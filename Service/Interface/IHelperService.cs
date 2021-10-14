using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IHelperService
    {
        string GetHash(string input);

        bool VerifiedHash(string hash, string input);
    }
}
