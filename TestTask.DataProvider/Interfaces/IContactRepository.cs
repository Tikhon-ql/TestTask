using TestTask.DataProvider.Models;

namespace TestTask.DataProvider.Interfaces
{
    /// <summary>
    /// Contact repository interface
    /// </summary>
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact?> ReadByMobilePhone(string mobilePhone);
    }
}
