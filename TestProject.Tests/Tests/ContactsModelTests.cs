using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ResultMonad;
using TestTask.Common;
using TestTask.Common.Filters;
using TestTask.Common.Interfaces;
using TestTask.Common.ViewModels;
using TestTask.Logic.Interfaces;
using TestTask.Logic.Models;
using TestTask.Pages;

namespace TestTask.Tests.Pages
{
    [TestFixture]
    public class ContactsModelTests
    {
        private IndexModel _indexModel;
        private Mock<ILogger<IndexModel>> _loggerMock;
        private Mock<IContactService> _contactServiceMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IUrlHelper> _urlHelperMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<IndexModel>>();
            _contactServiceMock = new Mock<IContactService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _urlHelperMock = new Mock<IUrlHelper>();

            _indexModel = new IndexModel(_loggerMock.Object, _contactServiceMock.Object, _unitOfWorkMock.Object)
            {
                Url = _urlHelperMock.Object
            };
        }

        [Test]
        public async Task OnPostAddContact_ValidModel_ReturnsRedirectToPageResult()
        {
            // Arrange
            var viewModel = new AddingViewModel();

            _contactServiceMock.Setup(c => c.Create(viewModel))
                .Returns(Result.Se());

            // Act
            var result = await _indexModel.OnPostAddContact(viewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
        }

        [Test]
        public async Task OnPostEditContact_ValidModel_ReturnsRedirectToPageResult()
        {
            // Arrange
            var viewModel = new EditingViewModel();

            _contactServiceMock.Setup(c => c.Update(viewModel))
                .Returns(Result.Ok());

            // Act
            var result = await _indexModel.OnPostEditContact(viewModel);

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
        }

        [Test]
        public async Task OnPostDeleteContact_ValidId_ReturnsRedirectToPageResult()
        {
            // Arrange
            var id = Guid.NewGuid();

            _contactServiceMock.Setup(c => c.Delete(id))
                .Returns(Result.Ok());

            // Act
            var result = await _indexModel.OnPostDeleteContact(id);

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
        }

        [Test]
        public void OnPostPagedList_ReturnsRedirectToPageResult()
        {
            // Arrange
            var currentPageSent = 1;

            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext { HttpContext = httpContext };

            _urlHelperMock.Setup(u => u.ActionContext)
                          .Returns(actionContext);

            _urlHelperMock.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
                          .Returns("/Index");

            _indexModel.Url = _urlHelperMock.Object;

            // Act
            var result = _indexModel.OnPostPagedList(currentPageSent);

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
        }

    }
}
