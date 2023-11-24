using System.Security.Claims;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace csharp_diary_rewrite.Features;

public record UpdateEntryCommand(string? NewTitle, string? NewText);

public static class UpdateEntryHandler
{
    public static IResult UpdateEntry(DiaryDbContext dbContext, ClaimsPrincipal user, int entryToUpdateId, UpdateEntryCommand updateEntryCommand)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var entryToUpdate = dbContext.Entries.SingleOrDefault(e =>
            e.Id == entryToUpdateId && e.Creator.Id == userId);

        if (entryToUpdate is null)
        {
            return Results.UnprocessableEntity();
        }

        entryToUpdate.Title = updateEntryCommand.NewTitle;
        entryToUpdate.Text = updateEntryCommand.NewText;
        dbContext.SaveChanges();
        
        return Results.NoContent();
    }
}