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
        Assert.Fail("Not Implemented");
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