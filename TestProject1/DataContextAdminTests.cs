using blog_website.Data;
using blog_website.Models.classes;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using TestProject1.Preparations;

namespace TestProject1;

public class DataContextAdminTests : TestFixtureBase
{
    public DataContextAdminTests(BlogAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task ShouldAddAdmin_Successfully()
    {
        //Arrange
        var context = Scope.ServiceProvider.GetService<ApplicationDbCon>();

        //Act
        EntityEntry<User> entity = await context.Admins.AddAsync(new User
        {
            Name = "erkan",
            Password = "1234"
        });

        await context.SaveChangesAsync();
        
        //assert

        var expected = 0;
        int actual = entity.Entity.Id;
        Assert.NotEqual(expected, actual);
    }
}