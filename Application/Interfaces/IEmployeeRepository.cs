using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> Add(Employee employee);
        Task<IEnumerable<Employee>> Get();
    }
}
