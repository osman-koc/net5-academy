using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace NET5Academy.Services.Discount.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDbConnection _dbConnection;
        public DiscountRepository(IConfiguration configuration)
        {
            _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<IEnumerable<Entities.Discount>> GetAll()
        {
            return await _dbConnection.QueryAsync<Entities.Discount>("SELECT * FROM discounts WHERE isDeleted=0");
        }

        public async Task<Entities.Discount> GetById(int id)
        {
            return await _dbConnection.QueryFirstAsync<Entities.Discount>("SELECT * FROM discounts WHERE isDeleted=0 and id=@Id", new { Id = id });
        }

        public async Task<Entities.Discount> GetByCodeAndUserId(string code, string userId)
        {
            return await _dbConnection.QueryFirstAsync<Entities.Discount>("SELECT * FROM discounts WHERE isDeleted=0 and code=@Code and userId=@UserId", new { Code = code, UserId = userId });
        }

        public async Task<int> CreateAndGetId(Entities.Discount entity)
        {
            return await _dbConnection.QueryFirstAsync<int>(@"
                    INSERT INTO discounts (userId,rate,code,createdDate,startDate,endDate,usedDate,isDeleted)
                    VALUES (@UserId,@Rate,@Code,@CreatedDate,@StartDate,@EndDate,@UsedDate,@IsDeleted)
                    RETURNING id
            ", entity);
        }

        public async Task<bool> Update(Entities.Discount entity)
        {
            var status = await _dbConnection.ExecuteAsync(@"
                    UPDATE discounts
                    SET  userId=@UserId
                        ,rate=@Rate
                        ,code=@Code
                        ,startDate=@StartDate
                        ,endDate=@EndDate
                    WHERE id=@Id
            ", entity);
            return status > 0;
        }

        public async Task<bool> DeleteById(int id)
        {
            var status = await _dbConnection.ExecuteAsync(
                "UPDATE discounts SET isDeleted=1,deletedDate=@DeletedDate WHERE id=@Id", 
                new { Id = id, DeletedDate = DateTime.UtcNow });

            return status > 0;
        }
    }
}
