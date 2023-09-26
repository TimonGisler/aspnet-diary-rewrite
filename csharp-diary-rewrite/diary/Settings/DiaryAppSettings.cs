using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace csharp_diary_rewrite.Settings;

public static class DiaryAppSettings
{
    //use env variables probably
    //lazy initialization might be an idea: https://learn.microsoft.com/en-us/dotnet/framework/performance/lazy-initialization
    static string jwtKey = "incredible_secure_key_trust_me"; //TODO TGIS
    static string issuer = "issuer is my server e.g. diaryApp.com";//TODO TGIS
    static string audience = "audience is my server e.g. diaryApp.com";//TODO TGIS

    public static SymmetricSecurityKey GetJwtKey()
    {
        return  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    }
    
    public static string GetIssuer()
    {
        return issuer;
    }
    
    public static string GetAudience()
    {
        return audience;
    }
}