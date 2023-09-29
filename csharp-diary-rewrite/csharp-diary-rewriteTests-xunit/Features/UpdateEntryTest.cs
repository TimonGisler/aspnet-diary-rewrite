using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class UpdateEntryTest : IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public UpdateEntryTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
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