using csharp_diary_rewrite.Model;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace csharp_diary_rewriteTests_xunit.Helpers;


public class TestDatabaseFixture : IDisposable
{
    private PostgreSqlContainer _containerWithTestDatabase;
    public DiaryDbContext DiaryDbContext { get; } //tests can use this to access dbcontext
    
    public TestDatabaseFixture()
    {
        //start docker container with db
        _containerWithTestDatabase = new PostgreSqlBuilder()
            .WithImage("postgres:15.4") //use same database version as in production
            .Build();
        
        _containerWithTestDatabase.StartAsync().ConfigureAwait(false);

        //create dbcontext
        DiaryDbContext = new DiaryDbContext(
            new DbContextOptionsBuilder<DiaryDbContext>()
                .UseNpgsql(_containerWithTestDatabase.GetConnectionString())
                .Options
        );
    }
    
    public void Dispose()
    {
        //delete docker container
        _containerWithTestDatabase.DisposeAsync().AsTask();
    }
}