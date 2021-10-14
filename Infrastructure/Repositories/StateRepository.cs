using Application.Interfaces;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StateRepository : IStateRepository
    {
        private readonly AppDbContext _context;
        public StateRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<State>> Get()
        {
            try
            {
                var sql = "SELECT * FROM TblState ORDER BY Name";
                using (var connection = _context.CreateConnection())
                {
                    var States = await connection.QueryAsync<State>(sql);
                    return States;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
