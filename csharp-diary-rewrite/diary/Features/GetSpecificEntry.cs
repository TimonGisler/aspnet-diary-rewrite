using System.Security.Claims;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace csharp_diary_rewrite.Features;

public record EntryData(int Id, string? Title, string? Text, DateTimeOffset Created);

public record ErrorReason(string Reason); //TODO TGIS, move this to a central place to reuse

public static class GetSpecificEntryHandler
{
    public static Results<Ok<EntryData>, NotFound<ErrorReason>> GetSpecificEntry(DiaryDbContext dbContext, ClaimsPrincipal user, int entryToGetId)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;


        var entry = dbContext.Entries
            .SingleOrDefault(e => e.Id == entryToGetId && e.Creator.Id == userId);
        
        return entry == null ? TypedResults.NotFound(new ErrorReason("this entry does not exist")) : TypedResults.Ok(new EntryData(entry.Id, entry.Title, entry.Text, entry.Created));      }
}