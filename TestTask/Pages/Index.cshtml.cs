using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestTask.Common.Interfaces;
using TestTask.Common.ViewModels;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Models;

namespace TestTask.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IContactService _service;
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(ILogger<IndexModel> logger, IContactService service, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public List<ContactDto> Contacts { get; set; } = new();
        public string? Error { get; set; } = null;

        public int TotalContactsCount = 0;

        /// <summary>
        /// Method which set data to show in table and set an error.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public async Task OnGet(int currentPage = 0, string? error = null)
        {
            var list = await _service.ReadPage(currentPage);
            Contacts = list.Data;
            Error = error;
            TotalContactsCount = list.TotalCount;
        }
     
        /// <summary>
        /// Creating a new contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAddContact(AddingViewModel viewModel)
        {
            var addingResult = await _service.Create(viewModel);
            string? addingError = null;

            if (addingResult.IsFailure)
            {
                addingError = addingResult.Error;
                _logger.LogError(addingError);
            }

            return await ReturnWithCommit(0, addingError);
        }

        /// <summary>
        /// Editing a contact
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostEditContact(EditingViewModel viewModel)
        {
            string? editingError = null;
            var editingResult = await _service.Update(viewModel);
            if (editingResult.IsFailure)
            {
                editingError = editingResult.Error;
                _logger.LogError(editingError);
            }

            return await ReturnWithCommit(0, editingError); 
        }

        /// <summary>
        /// Deleting a contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDeleteContact(Guid id)
        {
            string? deletingError = null;
            var deletingResult = await _service.Delete(id);
            if (deletingResult.IsFailure)
            {
                deletingError = deletingResult.Error;
                _logger.LogError(deletingError);
            }

            return await ReturnWithCommit(0, deletingError);
        }

        /// <summary>
        /// Method which define a page and sends it into onget method
        /// </summary>
        /// <param name="currentPageSent"></param>
        /// <returns></returns>
        public IActionResult OnPostPagedList(int currentPageSent)
        {
            var url = "/Index?currentPage=" + currentPageSent;
            return Redirect(url);
        }

        /// <summary>
        /// Method which commit changes sends an error if one occurs.
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="error"></param>
        /// <returns></returns>

        private async Task<IActionResult> ReturnWithCommit(int currentPage, string? error = null)
        {
            await _unitOfWork.Commit();

            var url = "/Index?currentPage=0";
            url += error == null ? "" : $"&error={error}";
            return Redirect(url);
        }
    }
}