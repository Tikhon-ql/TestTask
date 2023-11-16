using Microsoft.EntityFrameworkCore;
using TestTask.DataProvider.Context;
using TestTask.DataProvider.Interfaces;
using TestTask.DataProvider.Models;

namespace TestTask.DataProvider.Repositories
{
    /// <summary>
    /// IContactRepository implementation
    /// </summary>
    internal class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }
        /// <summary>
        /// Method to read an object by its mobile phone
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public async Task<Contact?> ReadByMobilePhone(string mobilePhone) => await _context.Contacts.FirstOrDefaultAsync(c => c.MobilePhone.Equals(mobilePhone));
    }
}
