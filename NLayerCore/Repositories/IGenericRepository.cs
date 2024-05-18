using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);  /* id ye göre data dön. Asenkron metod olduğu için "GetByIdAsync" Async kelimesini ekledik */
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        // productRepository.Where(x=>x.id>5).OrderBy.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);  /* T entity si için id si 5 ten büyük ve sonucu true dönmesi için hazırlanan metod*/
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);  // T entity si eklemek için olan metod   
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
