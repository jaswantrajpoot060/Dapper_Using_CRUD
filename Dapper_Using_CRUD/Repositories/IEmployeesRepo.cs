using Dapper;
using Dapper_Using_CRUD.Models;
using System.Data.SqlClient;
using System.Data;

namespace Dapper_Using_CRUD.Repositories
{
    public interface IEmployeesRepo
    {
        Task<IEnumerable<Employees>> GetEmployee();
        Task<IEnumerable<Employees>> GetByIdEmployee(int Id);
        Task<Employees> GetByIdEmployeeData(int Id);
        Task<Employees> Add(Employees obj);
        Task<Employees> Update(int Id, Employees obj);
        Task<int> Delete(int Id);
    }
}
