using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class AddEntryTests : IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public AddEntryTests(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
    }
    
    
    [Fact]
    public void not_logged_in_user_cannot_save_anything()
    {
       var response = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand("test title", "test text"));
        
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void logged_in_user_can_save_entry()
    {
        var jwt = _diaryApplicationWrapper.RetrieveJwtFromRegisteredUser();
        var response = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand("test title", "test text"), jwt);

        response.EnsureSuccessStatusCode();
        var entry = _diaryApplicationWrapper.GetDbContext().Entries
            .FirstOrDefault(e => e.Title == "test title");
        Assert.NotNull(entry);

    }

    [Fact]
    public void saved_entry_contains_creation_date()
    {
        Assert.Fail("Not Implemented");
    }
    
    [Fact]
    public void saved_entry_is_associated_with_current_user()
    {
        Assert.Fail("Not Implemented");
    }
}