using TestTask.Common.Interfaces;
using TestTask.DataProvider.Context;

namespace TestTask.DataProvider
{
    /// <summary>
    /// IUnitOfWork implementation
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
