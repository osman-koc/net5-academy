using System.Collections.Generic;
using System.Threading.Tasks;

namespace NET5Academy.Services.Discount.Data.Repositories
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Entities.Discount>> GetAll();
        Task<Entities.Discount> GetById(int id);
        Task<Entities.Discount> GetByCode(string code);
        Task<Entities.Discount> GetByCodeAndUserId(string code, string userId);
        Task<int> CreateAndGetId(Entities.Discount entitiy);
        Task<bool> Update(Entities.Discount entitiy);
        Task<bool> DeleteById(int id);
    }
}
