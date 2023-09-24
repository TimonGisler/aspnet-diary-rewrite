using csharp_diary_rewrite.Model;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace csharp_diary_rewrite.Features
{
    public record SaveEntryCommand(string Title, string Text);
    
    public class AddEntry
    {
        public void SaveEntry(SaveEntryCommand saveEntryCommand, DiaryDbContext diaryDbContext)
        {
            var entry = new Entry { Title = saveEntryCommand.Title, EntryText = saveEntryCommand.Text };
            
            diaryDbContext.Entries.Add(entry);
            diaryDbContext.SaveChanges();
        }
    }
}
