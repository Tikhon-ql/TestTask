using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using TestTask.Common;
using TestTask.Common.Filters;
using TestTask.Common.ViewModels;
using TestTask.DataProvider.Interfaces;
using TestTask.DataProvider.Models;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Models;

namespace TestTask.Logic.Services
{
    /// <summary>
    /// IContactService implementation
    /// </summary>
    internal class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IConfiguration _configuration;

        public ContactService(IContactRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        /// <summary>
        /// Phone number verification
        /// </summary>
        /// <param name="mobilePhone">Phone number</param>
        /// <returns></returns>
        private async Task<Result> IsMobilePhoneUsed(string mobilePhone)
        {
            var contact = await _repository.ReadByMobilePhone(mobilePhone);
            if (contact == null)
                return Result.Success();
            return Result.Failure("You can't set this mobile phone.");
        }

        public async Task<Result> Create(AddingViewModel viewModel)
        {

            var result = await IsMobilePhoneUsed(viewModel.MobilePhone);
            if (result.IsFailure)
                return Result.Failure(result.Error);

            var contact = new Contact
            {
                Name = viewModel.Name,
                JobTitle = viewModel.JobTitle,
                MobilePhone = viewModel.MobilePhone,
                BirthDate = viewModel.Birthdate
            };
            await _repository.Create(contact);
            return Result.Success();
        }

        public async Task<Result> Delete(Guid id)
        {
            var contact = await _repository.Read(id);
            if (contact == null)
                return Result.Failure("Bad contact id");

            _repository.Delete(contact);
            return Result.Success();
        }

        public async Task<ContactDto?> Read(Guid id)
        {
            var contact = await _repository.Read(id);
            if (contact == null)
                return null;

            var contactDto = new ContactDto
            {
                Id = contact.Id,
                BirthDate = contact.BirthDate,
                JobTitle = contact.JobTitle,
                MobilePhone = contact.MobilePhone,
                Name = contact.Name,
            };
            return contactDto;
        }
        /// <summary>
        /// Update method which reads an object, edit it's fields and when, in contoller, we call commit method to save changes.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<Result> Update(EditingViewModel viewModel)
        {
            var contact = await _repository.Read(viewModel.Id);
            if (contact == null)
                return Result.Failure("Bad contact id");

            var validateUpdateResult = await ValidateUpdate(contact, viewModel);
            if (validateUpdateResult.IsFailure)
                return Result.Failure(validateUpdateResult.Error);

            contact.Name = viewModel.Name;
            contact.MobilePhone = viewModel.MobilePhone;
            contact.BirthDate = viewModel.Birthdate;
            contact.JobTitle = viewModel.JobTitle;

            return Result.Success();
        }

        /// <summary>
        /// Validating: If the user wants to change their phone number to an existing one we will return a Fail.
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private async Task<Result> ValidateUpdate(Contact contact, EditingViewModel viewModel)
        {
            Result validationResult = Result.Success();
            var cont = await _repository.ReadByMobilePhone(viewModel.MobilePhone);
            if(cont != null)
            {
                if (contact.Name != cont.Name)
                    validationResult = Result.Failure("You can't set this mobile phone.");
            }
            return validationResult;
        }

        public async Task<PagedList<ContactDto>> ReadPage(int currentPage)
        {
            var filter = new BaseFilter
            {
                CurrentPage = currentPage,
                PageSize = int.Parse(_configuration.GetSection("Pagination").GetSection("PageSize").Value),
            };
            

            var list = await _repository.ReadPage(filter);
            var resultList = new PagedList<ContactDto>()
            {
                PageSize = list.PageSize,
                CurrentPage = list.CurrentPage,
                TotalCount = list.TotalCount,
                Data = list.Data.Select(c => new ContactDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    MobilePhone = c.MobilePhone,
                    BirthDate = c.BirthDate,
                    JobTitle = c.JobTitle
                }).ToList(),
            };
            return resultList;
        }
    }
}
