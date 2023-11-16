using TestTask.Common;
using TestTask.Common.Filters;

namespace TestTask.DataProvider.Interfaces
{
    /// <summary>
    /// Interface to implement CRUD operations
    /// </summary>
    /// <typeparam name="T">Any class you want</typeparam>
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task<T?> Read(Guid id);
        void Delete(T entity);
        /// <summary>
        /// Method which returns pagedlist: list which contains a part of data we want to
        /// </summary>
        /// <param name="filter">Class which contains current page and page size</param>
        /// <returns></returns>
        Task<PagedList<T>> ReadPage(BaseFilter filter);
    }
}
