using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace csharp_diary_rewrite.Model;

public class DiaryDbContext : IdentityDbContext<DiaryUser>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
        => optionsBuilder.UseNpgsql("Host=localhost:5433;Database=postgres;Username=postgres;Password=Hallo123_");  
    
    public DbSet<Entry> Entries { get; set; }
}