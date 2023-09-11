using Microsoft.EntityFrameworkCore;

namespace csharp_diary_rewrite.Model
{
    public class Entry //TODO TGIS, data class?
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? EntryText { get; set; }
        public string? Created { get; set; }
        public string? Updated { get; set; }
    }
}
