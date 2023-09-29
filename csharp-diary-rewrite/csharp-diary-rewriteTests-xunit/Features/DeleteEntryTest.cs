using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class DeleteEntryTest: IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    private readonly int _testEntryId;
    public DeleteEntryTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
        
        //create testEntry which I will attempt to delete in the tests
        var response = _diaryApplicationWrapper.SaveEntryAsRegisteredUser(new SaveEntryCommand("test title for DeleteEntryTest", "test text for DeleteEntryTest"));
        var entryInResponse = response.Content.ReadAsAsync<SaveEntryResponse>().Result;
        _testEntryId = entryInResponse.EntryId;
    }

    [Fact]
    public void not_logged_in_user_should_not_be_able_to_delete_any_entries()
    {
        var response = _diaryApplicationWrapper.DeleteEntry(_testEntryId);
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void user_is_unable_to_delete_entry_from_someone_else()
    {
        //register a new user 
        var email = "testuser2@deleteTest.com";
        var pw = "pw2";
        var respone= _diaryApplicationWrapper.RegisterUser(new RegisterUserCommand(email, pw));
        respone.EnsureSuccessStatusCode();
        var jwt = _diaryApplicationWrapper.RetrieveJwtFromRegisteredUser(new LoginUserCommand(email, pw));
        //save entry with new user
        var entrySaveResponse = _diaryApplicationWrapper.SaveEntry(new SaveEntryCommand("test title for DeleteEntryTest", "test text for DeleteEntryTest"), jwt);
        entrySaveResponse.EnsureSuccessStatusCode();
        var savedEntryId = entrySaveResponse.Content.ReadAsAsync<SaveEntryResponse>().Result.EntryId;
        
        //try to delete entry with first user
        var entryDeleteResponse = _diaryApplicationWrapper.DeleteEntryAsRegisteredUser(savedEntryId);
        
        Assert.True(entryDeleteResponse.StatusCode == HttpStatusCode.UnprocessableEntity); //it should be unprocessable because this entry does not exist from the view of user 1
    }

    [Fact]
    public void user_can_delete_his_own_entry()
    {
        var response = _diaryApplicationWrapper.DeleteEntryAsRegisteredUser(_testEntryId);
        var entryInDatabase =_diaryApplicationWrapper.GetDbContext().Entries.Find(_testEntryId);
        
        response.EnsureSuccessStatusCode();
        Assert.Null(entryInDatabase);
    }
}