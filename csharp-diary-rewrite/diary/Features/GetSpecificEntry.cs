using System.Security.Claims;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace csharp_diary_rewrite.Features;

public record EntryData(int Id, string? Title, string? Text, DateTimeOffset Created);

public static class GetSpecificEntryHandler
{
    public static IResult GetSpecificEntry(DiaryDbContext dbContext, ClaimsPrincipal user, int entryToGetId)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;


        var entry = dbContext.Entries
            .SingleOrDefault(e => e.Id == entryToGetId && e.Creator.Id == userId);
        
        return entry == null ? Results.NotFound("this entry does not exist") : Results.Ok(new EntryData(entry.Id, entry.Title, entry.Text, entry.Created));
    }
}