using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewrite.Features
{
    public record SaveEntryCommand(string Title, string Text);

    public class AddEntry
    {
        public void SaveEntry(SaveEntryCommand saveEntryCommand, DiaryDbContext diaryDbContext, ClaimsPrincipal user)
        {
            var userId = user.FindFirst(claim => claim.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var creator = diaryDbContext.Users.Find(userId);
            var entry = new Entry
            {
                Title = saveEntryCommand.Title, 
                Text = saveEntryCommand.Text, 
                Created = DateTimeOffset.UtcNow,
                Creator = creator!
            };

            diaryDbContext.Entries.Add(entry);
            diaryDbContext.SaveChanges();
        }
    }
}