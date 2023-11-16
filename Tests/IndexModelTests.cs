using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTask.Common.Interfaces;
using TestTask.Common.ViewModels;
using TestTask.Logic.Interfaces;
using TestTask.Pages;
using TestTask.Logic.Models;
using TestTask.Common;
using CSharpFunctionalExtensions;

namespace Tests
{
    [TestFixture]
    public class IndexModelTests
    {
        [Test]
        public async Task OnGet_SetsPropertiesCorrectly()
        {
            // Arrange
            var logger = A.Fake<ILogger<IndexModel>>();
            var contactService = A.Fake<IContactService>();
            var unitOfWork = A.Fake<IUnitOfWork>();
            var indexModel = new IndexModel(logger, contactService, unitOfWork);

            A.CallTo(() => contactService.ReadPage(A<int>._))
                .Returns(Task.FromResult(new PagedList<ContactDto>
                {
                    Data = new System.Collections.Generic.List<ContactDto>
                    {
                        new ContactDto
                        {
                            Name="Tikhon",
                            BirthDate = DateTime.Parse("11.08.2003"),
                        }
                    },
                    TotalCount = 1
                }));

            // Act
            await indexModel.OnGet();

            // Assert
            Assert.IsNotNull(indexModel.Contacts);
            Assert.AreEqual(1, indexModel.TotalContactsCount);
            Assert.IsNull(indexModel.Error);
        }

        [Test]
        public async Task OnPostAddContact_WithValidData_ReturnsRedirectResult()
        {
            // Arrange
            var logger = A.Fake<ILogger<IndexModel>>();
            var contactService = A.Fake<IContactService>();
            var unitOfWork = A.Fake<IUnitOfWork>();
            var indexModel = new IndexModel(logger, contactService, unitOfWork);

            var viewModel = new AddingViewModel
            {
                Name = "Test",
                MobilePhone = "+375257175402",
                Birthdate = DateTime.Parse("11.08.2003"),
            };

            A.CallTo(() => contactService.Create(A<AddingViewModel>._))
                .Returns(Task.FromResult(Result.Success()));

            // Act
            var result = await indexModel.OnPostAddContact(viewModel) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("/Index?currentPage=0", result.Url);
        }

        [Test]
        public async Task OnPostEditContact_WithValidData_ReturnsRedirectResult()
        {
            // Arrange
            var logger = A.Fake<ILogger<IndexModel>>();
            var contactService = A.Fake<IContactService>();
            var unitOfWork = A.Fake<IUnitOfWork>();
            var indexModel = new IndexModel(logger, contactService, unitOfWork);

            var viewModel = new EditingViewModel
            {
                Name = "Test",
                MobilePhone = "+375257175402",
                Birthdate = DateTime.Parse("11.08.2003"),
            };

            A.CallTo(() => contactService.Update(A<EditingViewModel>._))
                .Returns(Task.FromResult(Result.Success()));

            // Act
            var result = await indexModel.OnPostEditContact(viewModel) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("/Index?currentPage=0", result.Url);
        }

        [Test]
        public async Task OnPostDeleteContact_WithValidData_ReturnsRedirectResult()
        {
            // Arrange
            var logger = A.Fake<ILogger<IndexModel>>();
            var contactService = A.Fake<IContactService>();
            var unitOfWork = A.Fake<IUnitOfWork>();
            var fakeUrlHelper = A.Fake<IUrlHelper>();
            var indexModel = new IndexModel(logger, contactService, unitOfWork);

            var contactId = Guid.NewGuid();

            A.CallTo(() => contactService.Delete(contactId))
                .Returns(Task.FromResult(Result.Success()));

            // Act
            var result = await indexModel.OnPostDeleteContact(contactId) as RedirectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("/Index?currentPage=0", result.Url);
        }
    }
}
