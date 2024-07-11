using Xunit;
using blog_website;
using blog_website.Models.classes;
using blog_website.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestProject1
{
    public class blogwebsite_IsSameId
    {
        #region IsSameId_IdIs1_ReturnFalse
        [Fact]
        public async Task IsSameId_IdIs1_ReturnFalse()
        {
            // Arrange
            var controller = new AdminController(new blog_website.Data.ApplicationDbCon());

            // Act
            var result = controller.Index();

            // Assert
            //var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());

        }
        #endregion
    }
}