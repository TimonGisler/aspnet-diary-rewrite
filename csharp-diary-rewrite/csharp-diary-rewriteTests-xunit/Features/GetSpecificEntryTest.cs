using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class GetSpecificEntryTest : IClassFixture<DiaryApplicationWrapperFactory>
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    private readonly DiaryApplicationClient _diaryApplicationClient;

    public GetSpecificEntryTest(DiaryApplicationWrapperFactory diaryApplicationWrapperFactory)
    {
        _unauthenticatedDiaryApplicationClient =
            diaryApplicationWrapperFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser2;
    }

    [Fact]
    public void user_can_only_get_his_own_entries()
    {
        Assert.Fail("Not Implemented");
    }

    [Fact]
    public void retrieved_entry_contains_all_information()
    {
        Assert.Fail("Not Implemented");
    }
}