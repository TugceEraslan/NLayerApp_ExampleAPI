using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);  /* id ye göre data dön. Asenkron metod olduğu için "GetByIdAsync" Async kelimesini ekledik */
        Task<IEnumerable<T>> GetAllAsync();

        // productRepository.Where(x=>x.id>5).OrderBy.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);  /* T entity si için id si 5 ten büyük ve sonucu true dönmesi için hazırlanan metod*/
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);  // T entity si eklemek için olan metod   
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        void RemoveRangeAsync(IEnumerable<T> entities);
    }
}
