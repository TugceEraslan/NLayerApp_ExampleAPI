using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /* Miras alacacağım yerlerde AppDbContext'e erişebilmem lazım o yüzden de  private yerine protected olarak tanımlıyorum 
         * Protected erişim belirleyicisine "sadece miras aldığımız yerde erişebiliriz". 
         * Bizde AppDbContext bu context'i miras alınan sınıflardan erişilmesini istediğimizden protected olarak belirledik */

        protected readonly AppDbContext _context; 

        /* DbSet belirleyeceğiz.DbSet entity'me yani veritabanında ki tabloma tarşılık geliyor.
         * Onu da tanımlayalım.
         * readonly olarak tanımlama sebebim;
         * Ya tanımlama ya da ctor oluştururken değer atamamız gerekiyor. 
         * Diğer türlü metod içinde değer set etmeye çalışırsak hata verir ki zaten öyle olmasını istediğimiz  için yapıyoruz.
         * Yani değer atama işleminin sadece tanımlarken ya da ctor oluşturulurken yapılabilmesi , başka türlü yapılamaması için
         */

        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            /* AsNoTracking() ile çektiğim veriyi hemen memory'e almıyorum. Bunu demezsek memory' alır 
             ve durumlarını anlık olarak izler. Bu yüzden de performansını düşürür */
            return _dbSet.AsNoTracking().AsQueryable(); 
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            /* _context.Entry(entity).State = EntityState.Deleted aşağıdakiyle aynısı. 
             * Property'sini set ettiğimiz Remove özelliğinin o yüzden async olmasına gerek yok.*/
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
