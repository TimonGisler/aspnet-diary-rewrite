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
       var response = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand("test title for not_logged_in_user_cannot_save_anything", "test text for not_logged_in_user_cannot_save_anything"));
        
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void logged_in_user_can_save_entry()
    {
        var title = "logged_in_user_can_save_entry title";
        var text = "logged_in_user_can_save_entry text";
        
        var jwt = _diaryApplicationWrapper.RetrieveJwtFromRegisteredUser();
        var response = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand(title, text), jwt);

        response.EnsureSuccessStatusCode();
        var entry = _diaryApplicationWrapper.GetDbContext().Entries
            .FirstOrDefault(e => e.Title == title);
        Assert.NotNull(entry);

    }

    [Fact]
    public void saved_entry_contains_creation_date()
    {
        var title = "saved_entry_contains_creation_date title"; //TODO TGIS, probably should the api return the id of the entry or smth
        var jwt = _diaryApplicationWrapper.RetrieveJwtFromRegisteredUser();
        DateTimeOffset timeLowerBound = DateTimeOffset.UtcNow.AddMinutes(-5);
        DateTimeOffset timeUpperBound = DateTimeOffset.UtcNow.AddMinutes(5);
        
        var response = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand(title, "test text"), jwt);
        response.EnsureSuccessStatusCode();
        var entry = _diaryApplicationWrapper.GetDbContext().Entries
            .FirstOrDefault(e => e.Title == title);

        Assert.InRange(entry!.Created, timeLowerBound, timeUpperBound);
    }
    
    [Fact]
    public void saved_entry_is_associated_with_current_user()
    {
        Assert.Fail("Not Implemented");
    }
}