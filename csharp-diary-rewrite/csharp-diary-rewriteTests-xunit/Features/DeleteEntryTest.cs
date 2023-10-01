using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class DeleteEntryTest: IClassFixture<TestApplicationFactory>
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    private readonly DiaryDbContext _diaryDbContext;

    
    private readonly int _testEntryId;
    public DeleteEntryTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
        _diaryDbContext = testApplicationFactory.DiaryDbContext;
        
        //create testEntry which I will attempt to delete in the tests
        var response = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand("test title for DeleteEntryTest", "test text for DeleteEntryTest"));
        var entryInResponse = response.Content.ReadAsAsync<SaveEntryResponse>().Result;
        _testEntryId = entryInResponse.EntryId;
    }

    [Fact]
    public void not_logged_in_user_should_not_be_able_to_delete_any_entries()
    {
        var response = _unauthenticatedDiaryApplicationClient.DeleteEntry(_testEntryId);
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void user_is_unable_to_delete_entry_from_someone_else()
    {
        //save entry with new user
        var entrySaveResponse = _diaryApplicationClientForUser2.SaveEntry(new SaveEntryCommand("test title for DeleteEntryTest", "test text for DeleteEntryTest"));
        entrySaveResponse.EnsureSuccessStatusCode();
        var savedEntryId = entrySaveResponse.Content.ReadAsAsync<SaveEntryResponse>().Result.EntryId;
        
        //try to delete entry with first user
        var entryDeleteResponse = _diaryApplicationClientForUser1.DeleteEntry(savedEntryId);
        
        Assert.True(entryDeleteResponse.StatusCode == HttpStatusCode.UnprocessableEntity); //it should be unprocessable because this entry does not exist from the view of user 1
    }

    [Fact]
    public void user_can_delete_his_own_entry()
    {
        var response = _diaryApplicationClientForUser1.DeleteEntry(_testEntryId);
        var entryInDatabase = _diaryDbContext.Entries.Find(_testEntryId);
        
        response.EnsureSuccessStatusCode();
        Assert.Null(entryInDatabase);
    }
}