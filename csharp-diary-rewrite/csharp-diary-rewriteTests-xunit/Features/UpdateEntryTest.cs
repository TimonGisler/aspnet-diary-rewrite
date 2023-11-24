using System.Net;
using csharp_diary_rewrite.Features;
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
        //save entry for user 2
        var saveEntryResponse = _diaryApplicationClientForUser2.SaveEntry(new SaveEntryCommand("user_cannot_modify_entry_of_another_user", "text")).Content.ReadAsAsync<SaveEntryResponse>().Result;
        
        //attempt to modify entry of user 2 as user 1
        var updateEntryCommand = new UpdateEntryCommand("new title", "new text");
        var updateAttemptResponse = _diaryApplicationClientForUser1.UpdateEntry(updateEntryCommand, saveEntryResponse.EntryId);
        
        Assert.True(updateAttemptResponse.StatusCode == HttpStatusCode.UnprocessableEntity); //it should be unprocessable because this entry does not exist from the view of user 1
        
    }

    [Fact]
    public void updating_entry_works()
    {
        //Save entry
        var saveEntryResponse = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand("updating_entry_works", "text")).Content.ReadAsAsync<SaveEntryResponse>().Result;
        
        //Update entry
        var updateEntryCommand = new UpdateEntryCommand("new title", "new text");
        var updateAttemptResponse = _diaryApplicationClientForUser1.UpdateEntry(updateEntryCommand, saveEntryResponse.EntryId);
        
        //get the entry and assert that the title and text have been updated
        var entryFromDb = _diaryDbContext.Entries.Find(saveEntryResponse.EntryId)!;
        Assert.Equal(updateEntryCommand.NewTitle, entryFromDb.Title);
    }
}