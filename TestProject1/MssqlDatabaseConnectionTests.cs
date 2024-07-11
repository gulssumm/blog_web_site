using Microsoft.Data.SqlClient;
using TestProject1.Preparations;

namespace TestProject1;

public class MssqlDatabaseConnectionTests : TestFixtureBase
{
    private readonly BlogAppFactory factory;

    public MssqlDatabaseConnectionTests(BlogAppFactory factory) : base(factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task TestDb()
    {
        string connectionString = factory.MyDatabase.GetConnectionString();
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        await using SqlCommand? command = connection.CreateCommand();
        command.CommandText = "SELECT 1;";

        var actual = await command.ExecuteScalarAsync() as int?;
        Assert.Equal(1, actual.GetValueOrDefault());
    }
}