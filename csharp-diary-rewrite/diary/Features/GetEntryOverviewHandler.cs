using System.Security.Claims;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewrite.Features;



public record EntryOverview(int EntryId, string? Title, string? Text, DateTimeOffset Created);

public static class GetEntryOverviewHandler
{
    public static List<EntryOverview> GetEntryOverview(DiaryDbContext dbContext, ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var entryOverviews = dbContext.Entries
            .Where(u => u.Creator.Id == userId)
            .Select(u => new EntryOverview(u.Id, u.Title, u.Text, u.Created))
            .ToList();

        return entryOverviews;
    }
}