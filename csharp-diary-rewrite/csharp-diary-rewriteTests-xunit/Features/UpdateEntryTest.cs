using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class UpdateEntryTest : IClassFixture<DiaryApplicationWrapperFactory>
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    private readonly DiaryApplicationClient _diaryApplicationClient;
    
    public UpdateEntryTest(DiaryApplicationWrapperFactory diaryApplicationWrapperFactory)
    {
        _unauthenticatedDiaryApplicationClient = diaryApplicationWrapperFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser2;

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