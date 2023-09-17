using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp_diary_rewrite.Model;

public class DiaryDbContext : IdentityDbContext<DiaryUser>
{
    public DiaryDbContext(DbContextOptions<DiaryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Entry> Entries { get; set; }
}