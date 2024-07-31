using Microsoft.EntityFrameworkCore;

namespace blog_website;

public interface IDbSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(TContext context);
}