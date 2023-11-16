using CSharpFunctionalExtensions;
using TestTask.Common;
using TestTask.Common.ViewModels;
using TestTask.Logic.Models;

namespace TestTask.Logic.Interfaces
{
    /// <summary>
    /// Interface to define contact's process methods.
    /// </summary>
    public interface IContactService
    {
        Task<Result> Create(AddingViewModel contactDto);
        Task<ContactDto?> Read(Guid id);
        Task<Result> Update(EditingViewModel viewModel);
        Task<Result> Delete(Guid id);
        Task<PagedList<ContactDto>> ReadPage(int currentPage);
    }
}
