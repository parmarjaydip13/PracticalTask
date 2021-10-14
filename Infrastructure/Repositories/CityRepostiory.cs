using Application.Interfaces;
using Dapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CityRepostiory  : ICityRepository
    {
        private readonly AppDbContext _context;
        public CityRepostiory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> Get()
        {
            try
            {
                var sql = "SELECT * FROM TblCity ORDER BY Name";
                using (var connection = _context.CreateConnection())
                {
                    var cities = await connection.QueryAsync<City>(sql);
                    return cities;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<City>> GetByState(int stateId)
        {
            try
            {
                var sql = "SELECT * FROM TblCity where StateId = @stateId ORDER BY Name";
                using (var connection = _context.CreateConnection())
                {
                    var cities = await connection.QueryAsync<City>(sql, new { stateId });
                    return cities;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
