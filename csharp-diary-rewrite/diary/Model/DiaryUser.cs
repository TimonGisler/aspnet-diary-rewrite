using Microsoft.AspNetCore.Identity;

namespace csharp_diary_rewrite.Model;

public class DiaryUser : IdentityUser
{
    DateTimeOffset Joined { get; set; }
}