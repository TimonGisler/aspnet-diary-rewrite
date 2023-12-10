using System.Text;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using csharp_diary_rewrite.Settings;
using csharp_diary_rewrite.Swashbuckle;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace csharp_diary_rewrite;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DiaryDbContext>(options =>
            options.UseNpgsql("Host=localhost:5433;Database=postgres;Username=postgres;Password=Hallo123_"));

        //add jwt authentication
        builder.Services
            .AddAuthentication() //useAuthentication gets automatically called https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0#enabling-authentication-in-minimal-apps
            .AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = DiaryAppSettings.GetIssuer(),
                    ValidAudience = DiaryAppSettings.GetAudience(),
                    IssuerSigningKey = DiaryAppSettings.GetJwtKey(),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        builder.Services
            .AddAuthorization(); //useAuthorization gets automatically called https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0#enabling-authentication-in-minimal-apps

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
        builder.Services.AddSwaggerGen(c =>
        {
            // https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2036
            c.SchemaFilter<RequiredNotNullableSchemaFilter>();
            c.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
            c.UseAllOfToExtendReferenceSchemas(); // Allows $ref enums to be nullable
            c.UseAllOfForInheritance();  // Allows $ref objects to be nullable
        });

        var app = builder.Build();

        app.MapGroup("/api") //create a group
            .MapApiRoutes(); //map all the routes

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseSwagger(); //http://localhost:5281/swagger/v1/swagger.json
        app.UseSwaggerUI(); //http://localhost:5281/swagger/index.html

        app.Run();
    }
}

public static class ApiRoutesExtensionFunctions
{
    //Extension method to map all the routes
    public static IEndpointRouteBuilder MapApiRoutes(this IEndpointRouteBuilder apiEndpoints)
    {
        //TODO TGIS, move into dependency injection -> meh prolly not even encessary static class prolly good enough ngl
        AddEntry addEntryHandler = new AddEntry();

        apiEndpoints.MapPost("/entry", addEntryHandler.SaveEntry)
            .RequireAuthorization();
        apiEndpoints.MapDelete("/entry/{entryToDeleteId:int}", DeleteEntryHandler.DeleteEntry)
            .RequireAuthorization();
        apiEndpoints.MapGet("/entry", GetEntryOverviewHandler.GetEntryOverview)
            .RequireAuthorization();
        apiEndpoints.MapGet("/entry/{entryToGetId:int}", GetSpecificEntryHandler.GetSpecificEntry)
            .RequireAuthorization();
        apiEndpoints.MapPost("/entry/{entryToUpdateId:int}", UpdateEntryHandler.UpdateEntry)
            .RequireAuthorization();
        
        
        apiEndpoints.MapPost("/register", RegisterUserHandler.RegisterUser);
        apiEndpoints.MapPost("/login", LoginUserHandler.LoginUser);


        return apiEndpoints;
    }
    
}