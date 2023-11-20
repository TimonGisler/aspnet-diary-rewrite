using csharp_diary_rewrite.Model;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class UpdateEntryTest
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    private readonly DiaryDbContext _diaryDbContext;

    
    public UpdateEntryTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
        _diaryDbContext = testApplicationFactory.DiaryDbContext;
    }
    
    [Fact]
    public void user_cannot_modify_entry_of_another_user()
    {
        Assert.Fail("Not Implemented");
    }

    [Fact]
    public void updating_entry_works()
    {
        Assert.Fail("Not Implemented");
    }
}