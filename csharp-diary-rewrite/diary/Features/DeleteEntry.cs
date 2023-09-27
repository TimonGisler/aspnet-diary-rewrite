using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewrite.Features
{
    public static class DeleteEntryHandler
    {
        public static void DeleteEntry(int entryToDeleteId, ClaimsPrincipal user,
            DiaryDbContext diaryDbContext)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var entryToDelete = diaryDbContext.Entries.Single(e =>
                e.Id == entryToDeleteId && e.Creator.Id == userId);
            
            diaryDbContext.Entries.Remove(entryToDelete);
            diaryDbContext.SaveChanges();
        }
    }
}