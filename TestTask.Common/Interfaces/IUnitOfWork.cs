namespace TestTask.Common.Interfaces
{
    /// <summary>
    /// Interface which provides us a logic to make a commit once in a controller.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Method which calls save changes method in context class.
        /// </summary>
        /// <returns></returns>
        Task Commit();
    }
}
