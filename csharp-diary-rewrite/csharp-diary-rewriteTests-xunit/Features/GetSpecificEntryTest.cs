using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class GetSpecificEntryTest : IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public GetSpecificEntryTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
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