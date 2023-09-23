using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace csharp_diary_rewrite;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<DiaryDbContext>(options =>
            options.UseNpgsql("Host=localhost:5433;Database=postgres;Username=postgres;Password=Hallo123_"));
        
        //aspnet identity
        builder.Services.AddIdentityCore<DiaryUser>()
            .AddEntityFrameworkStores<DiaryDbContext>();
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
            
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
        });
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        
        app.MapGroup("/api") //create a group
            .MapApiRoutes(); //map all the routes

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseSwagger(); //http://localhost:5000/swagger/v1/swagger.json
        app.UseSwaggerUI();
        
        app.Run();
    }
}


public static class ApiRoutesExtensionFunctions
{
    //Extension method to map all the routes
    public static IEndpointRouteBuilder MapApiRoutes(this IEndpointRouteBuilder endpoints)
    {
        //TODO TGIS, move into dependency injection -> meh prolly not even encessary static class prolly good enough ngl
        AddEntry addEntryHandler = new AddEntry();

        endpoints.MapGet("/api", Handler());
        endpoints.MapGet("/saveEntry", addEntryHandler.SaveEntry);
        endpoints.MapPost("/register", RegisterUserHandler.RegisterUser);
        endpoints.MapPost("/login", LoginUserHandler.LoginUser);



        return endpoints;
    }

    private static Func<string> Handler()
    {
        Console.WriteLine("lol");
        return () => { Console.WriteLine("CALLED"); return "HELLO"; };
    }
}
