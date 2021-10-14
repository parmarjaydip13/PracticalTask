using Application.Interfaces;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Employee employee)
        {
            try
            {

                employee.CreatedDate = DateTime.Now;

                string insertSql = @"INSERT INTO TblEmployee( Firstname,Lastname,StateId,CityId,DateOfBirth,TotalExperience,Salary,CreatedDate)
                                                 VALUES( 
                                                        @Firstname,
                                                        @Lastname,
                                                        @StateId,
                                                        @CityId,
                                                        @DateOfBirth,
                                                        @TotalExperience,
                                                        @Salary,
                                                        @CreatedDate
                                                       );
                                     SELECT CAST(SCOPE_IDENTITY() AS INT)";

                using (var connection = _context.CreateConnection())
                {
                    var rows = await connection.QueryAsync<int>(insertSql, employee);
                    return rows.Single();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            try
            {
                string selectSql = @"select emp.*,c.Name as [City],st.Name as [State] from TblEmployee as emp
                                                    join TblCity as c on emp.CityId = c.Id 
                                                    join TblState as st on st.Id = c.StateId";

                using (var connection = _context.CreateConnection())
                {
                    var rows = await connection.QueryAsync<Employee>(selectSql);
                    return rows;
                }
            }
            catch (Exception ex)
            {
               
                //TODO: Log Error
                throw;
            }
        }
    }
}
