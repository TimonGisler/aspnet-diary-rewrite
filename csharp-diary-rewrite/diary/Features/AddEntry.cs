using csharp_diary_rewrite.Model;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace csharp_diary_rewrite.Features
{
    public class AddEntry
    {
        private record SaveEntryCommand(string Title, string Text);
        
        public long SaveEntry(HttpContext context, DiaryDbContext diaryDbContext)
        {
            Console.WriteLine(context.Request.Body.ToString());
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Console.WriteLine(milliseconds);
            
            Entry entry = new Entry { Title = milliseconds.ToString() };
            
            diaryDbContext.Entries.Add(entry);
            diaryDbContext.SaveChanges();
            
            return milliseconds;
        }
    }
}
