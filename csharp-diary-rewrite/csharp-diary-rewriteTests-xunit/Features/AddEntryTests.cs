using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class AddEntryTests
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    private readonly DiaryDbContext _diaryDbContext;
    
    public AddEntryTests(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
        _diaryDbContext = testApplicationFactory.DiaryDbContext;
    }
    
    
    [Fact]
    public void not_logged_in_user_cannot_save_anything()
    {
       var response = _unauthenticatedDiaryApplicationClient.SaveEntry(new SaveEntryCommand("test title for not_logged_in_user_cannot_save_anything", "test text for not_logged_in_user_cannot_save_anything"));
        
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void logged_in_user_can_save_entry()
    {
        const string title = "logged_in_user_can_save_entry title";
        const string text = "logged_in_user_can_save_entry text";
        
        var response = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand(title, text));

        response.EnsureSuccessStatusCode();
        var entry = _diaryDbContext.Entries
            .FirstOrDefault(e => e.Title == title);
        Assert.NotNull(entry);

    }

    [Fact]
    public void saved_entry_contains_creation_date()
    {
        const string title = "saved_entry_contains_creation_date title";
        var timeLowerBound = DateTimeOffset.UtcNow.AddMinutes(-5);
        var timeUpperBound = DateTimeOffset.UtcNow.AddMinutes(5);
        
        var response = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand(title, "test text"));
        var entry = _diaryDbContext.Entries
            .Single(e => e.Title == title);

        response.EnsureSuccessStatusCode();
        Assert.InRange(entry.Created, timeLowerBound, timeUpperBound);
    }
    
    [Fact]
    public void saved_entry_is_associated_with_current_user()
    {
        const string title = "saved_entry_is_associated_with_current_user title"; 
        var registeredUserEmail = _diaryApplicationClientForUser1.UserOfThisClient!.Email;
        
        var response = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand(title, "test text"));
        var entry = _diaryDbContext.Entries.Include(entry => entry.Creator)
            .Single(e => e.Title == title);
        
        response.EnsureSuccessStatusCode();
        Assert.Equal(registeredUserEmail, entry.Creator.Email);
    }
    
    [Fact]
    public void saved_entry_gets_returned_in_the_response()
    {
        const string title = "saved_entry_gets_returned_in_the_response title";
        
        var response = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand(title, "test text"));
        var savedEntry = _diaryDbContext.Entries.Include(entry => entry.Creator)
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