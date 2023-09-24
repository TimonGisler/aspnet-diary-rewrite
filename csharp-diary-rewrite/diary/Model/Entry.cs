using Microsoft.EntityFrameworkCore;

namespace csharp_diary_rewrite.Model
{
    public class Entry //TODO TGIS, data class?
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Created { get; set; }
    }
}
