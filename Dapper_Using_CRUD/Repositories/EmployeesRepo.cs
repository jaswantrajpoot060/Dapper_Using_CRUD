using Dapper;
using Dapper_Using_CRUD.Models;
using System.Data;
using System.Data.SqlClient;

namespace Dapper_Using_CRUD.Repositories
{
    public class EmployeesRepo : IEmployeesRepo
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _sqlConnection;

        public EmployeesRepo(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<IEnumerable<Employees>> GetEmployee()
        {
            var list = await _sqlConnection.QueryAsync<Employees>(@"Usp_Employee_GetAll", commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<IEnumerable<Employees>> GetByIdEmployee(int Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);
            var list = await _sqlConnection.QueryAsync<Employees>(@"Usp_Employee_GetById", parameters, commandType: CommandType.StoredProcedure);
            return list;
        }
        public async Task<Employees> GetByIdEmployeeData(int Id)
        {
            var obj = await _sqlConnection.QueryFirstAsync<Employees>(@"Usp_Employee_GetById", Id, commandType: CommandType.StoredProcedure);
            return obj;
        }
        public async Task<Employees> Add(Employees obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeId", obj.EmployeeId);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@Description", obj.Description);
            parameters.Add("@PhoneNumber", obj.PhoneNumber);
            parameters.Add("@Address", obj.Address);
            parameters.Add("@City", obj.City);
            parameters.Add("@Region", obj.Region);
            parameters.Add("@PostalCode", obj.PostalCode);
            parameters.Add("@Country", obj.Country);
            parameters.Add("@CreatedBy", "");
            await _sqlConnection.ExecuteAsync(@"Usp_Employee_Insert", parameters, commandType: CommandType.StoredProcedure);
            return obj;
        }
        public async Task<Employees> Update(int Id, Employees obj)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);
            parameters.Add("@EmployeeId", obj.EmployeeId);
            parameters.Add("@Name", obj.Name);
            parameters.Add("@Description", obj.Description);
            parameters.Add("@PhoneNumber", obj.PhoneNumber);
            parameters.Add("@Address", obj.Address);
            parameters.Add("@City", obj.City);
            parameters.Add("@Region", obj.Region);
            parameters.Add("@PostalCode", obj.PostalCode);
            parameters.Add("@Country", obj.Country);
            await _sqlConnection.ExecuteAsync(@"Usp_Employee_Update", parameters, commandType: CommandType.StoredProcedure);
            return obj;
        }
        public async Task<int> Delete(int Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", Id);
            return await Task.Run(() => _sqlConnection.ExecuteAsync(@"Usp_Employee_Delete", parameters, commandType: CommandType.StoredProcedure));
        }
    }
}
