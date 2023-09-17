using System.Data.Common;
using csharp_diary_rewrite;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace csharp_diary_rewriteTests_xunit.Helpers;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
    PostgreSqlContainer postgreSqlContainer;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // remove the options provided in the Program.cs
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<DiaryDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            //start docker container with db
            postgreSqlContainer = new PostgreSqlBuilder()
                .WithImage("postgres:15.4") //use same database version as in production
                .Build();

            postgreSqlContainer.StartAsync().Wait();
            
            // Add my custom DbContextOptions which uses the test container
            var connectionString = postgreSqlContainer.GetConnectionString();
            Console.WriteLine(connectionString);
            services.AddDbContext<DiaryDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            
            //migrate database
            var diaryDbContext = services.BuildServiceProvider().GetService<DiaryDbContext>();
            diaryDbContext!.Database.Migrate();

        });
    }
}