using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetByState(int stateId);
        Task<IEnumerable<City>> Get();
    }
}
