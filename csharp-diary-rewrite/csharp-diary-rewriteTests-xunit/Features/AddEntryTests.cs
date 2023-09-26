using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.EntityFrameworkCore;
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
        const string title = "logged_in_user_can_save_entry title";
        const string text = "logged_in_user_can_save_entry text";
        
        var response = _diaryApplicationWrapper.SaveEntryAsRegisteredUser(new SaveEntryCommand(title, text));

        response.EnsureSuccessStatusCode();
        var entry = _diaryApplicationWrapper.GetDbContext().Entries
            .FirstOrDefault(e => e.Title == title);
        Assert.NotNull(entry);

    }

    [Fact]
    public void saved_entry_contains_creation_date()
    {
        const string title = "saved_entry_contains_creation_date title";
        var timeLowerBound = DateTimeOffset.UtcNow.AddMinutes(-5);
        var timeUpperBound = DateTimeOffset.UtcNow.AddMinutes(5);
        
        var response = _diaryApplicationWrapper.SaveEntryAsRegisteredUser(new SaveEntryCommand(title, "test text"));
        var entry = _diaryApplicationWrapper.GetDbContext().Entries
            .Single(e => e.Title == title);

        response.EnsureSuccessStatusCode();
        Assert.InRange(entry.Created, timeLowerBound, timeUpperBound);
    }
    
    [Fact]
    public void saved_entry_is_associated_with_current_user()
    {
        const string title = "saved_entry_is_associated_with_current_user title"; 
        var registeredUserEmail = _diaryApplicationWrapper.UserSavedInDatabase.Email;
        
        var response = _diaryApplicationWrapper.SaveEntryAsRegisteredUser(new SaveEntryCommand(title, "test text"));
        var entry = _diaryApplicationWrapper.GetDbContext().Entries.Include(entry => entry.Creator)
            .Single(e => e.Title == title);
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(registeredUserEmail, entry.Creator.Email);
    }
    
    [Fact]
    public void saved_entry_gets_returned_in_the_response()
    {
        const string title = "saved_entry_gets_returned_in_the_response title";
        var registeredUserEmail = _diaryApplicationWrapper.UserSavedInDatabase.Email;
        
        var response = _diaryApplicationWrapper.SaveEntryAsRegisteredUser(new SaveEntryCommand(title, "test text"));
        var savedEntry = _diaryApplicationWrapper.GetDbContext().Entries.Include(entry => entry.Creator)
            .Single(e => e.Title == title);
        
        response.EnsureSuccessStatusCode();
        var entryInResponse = response.Content.ReadAsAsync<SaveEntryResponse>().Result; //TODO TGIS .Result makes maybe tests slow, test if tests run faster without it as soon as I have more tests

        //during serialization the time gets rounded to the nearest millisecond, so my response is a little bit off.
        var precision = TimeSpan.FromMilliseconds(1);
        Assert.InRange(entryInResponse.Created, 
            savedEntry.Created - precision, 
            savedEntry.Created + precision);        Assert.Equal(entryInResponse.Title, savedEntry.Title);
        Assert.Equal(entryInResponse.Text, savedEntry.Text);
        Assert.Equal(entryInResponse.EntryId, savedEntry.Id);
    }

}