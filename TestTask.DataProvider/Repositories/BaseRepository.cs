using Microsoft.EntityFrameworkCore;
using TestTask.Common;
using TestTask.Common.Filters;
using TestTask.DataProvider.Context;
using TestTask.DataProvider.Interfaces;
using TestTask.DataProvider.Models;

namespace TestTask.DataProvider.Repositories
{
    /// <summary>
    /// Base repository to implement common behavior
    /// </summary>
    /// <typeparam name="T">Entity class</typeparam>
    internal class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        private DbSet<T> _set;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }
        public async Task Create(T entity) => await _set.AddAsync(entity);

        public async Task<T?> Read(Guid id) => await _set.FirstOrDefaultAsync(e => e.Id == id);

        public void Delete(T entity) => _set.Remove(entity);
        public async Task<PagedList<T>> ReadPage(BaseFilter filter)
        {
            var skipSize = filter.PageSize * (filter.CurrentPage);
            return new PagedList<T>
            {
                CurrentPage = filter.CurrentPage,
                PageSize = filter.PageSize,
                TotalCount = _set.Count(),
                Data = await _set.Skip(skipSize).Take(filter.PageSize).ToListAsync()
            };
        }
    }
}
