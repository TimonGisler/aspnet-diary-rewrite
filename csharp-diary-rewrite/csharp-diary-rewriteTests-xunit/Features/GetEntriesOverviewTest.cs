using System.Net;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class GetEntriesOverviewTest : IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public GetEntriesOverviewTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
    }
    
    [Fact]
    public void not_logged_in_user_should_not_be_able_to_fetch_any_entries()
    {
        var response = _diaryApplicationWrapper.GetEntriesOverview();
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void overview_only_returns_entries_of_this_user()
    {
        Assert.Fail("Not Implemented");
    }
}