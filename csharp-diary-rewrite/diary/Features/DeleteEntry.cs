using System.Security.Claims;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewrite.Features
{
    public static class DeleteEntryHandler
    {
        public static IResult DeleteEntry(int entryToDeleteId, ClaimsPrincipal user, DiaryDbContext diaryDbContext)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var entryToDelete = diaryDbContext.Entries.SingleOrDefault(e =>
                e.Id == entryToDeleteId && e.Creator.Id == userId);

            if (entryToDelete is null)
            {
                return Results.UnprocessableEntity();
            }

            diaryDbContext.Entries.Remove(entryToDelete);
            diaryDbContext.SaveChanges();

            return Results.NoContent();
        }
    }
}