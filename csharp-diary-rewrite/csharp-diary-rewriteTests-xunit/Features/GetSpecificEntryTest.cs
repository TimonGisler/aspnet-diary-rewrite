using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class GetSpecificEntryTest : IClassFixture<TestApplicationFactory>
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    public GetSpecificEntryTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
    }

    [Fact]
    public void user_cannot_access_entry_of_other_user()
    {
        var savedEntryResponse = _diaryApplicationClientForUser2.SaveEntry(new SaveEntryCommand("user2 title", "user_can_only_get_his_own_entries"));
        savedEntryResponse.EnsureSuccessStatusCode();
        var savedEntryId = savedEntryResponse.Content.ReadAsAsync<SaveEntryResponse>().Result.EntryId;
        
        var retrievedEntry = _diaryApplicationClientForUser1.GetSpecificEntry(savedEntryId);
        
        Assert.True(retrievedEntry.StatusCode == HttpStatusCode.NotFound);
    }

    [Fact]
    public void retrieved_entry_contains_all_information()
    {
        //save entry
        const string title = "title retrieved_entry_contains_all_information";
        const string text = "test text";
        var creationTime = DateTimeOffset.UtcNow;
        var savedEntryResponse = _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand(title, text));
        savedEntryResponse.EnsureSuccessStatusCode();
        var savedEntryId = savedEntryResponse.Content.ReadAsAsync<SaveEntryResponse>().Result.EntryId;

        //retrieve saved entry
        var retrievedEntry = _diaryApplicationClientForUser1.GetSpecificEntry(savedEntryId).Content.ReadAsAsync<EntryData>().Result;

        //check that the retrieved entry contains all information
        var precision = TimeSpan.FromSeconds(1);
        Assert.InRange(retrievedEntry.Created, 
            creationTime - precision, 
            creationTime + precision);        
        Assert.Equal(title, retrievedEntry.Title);
        Assert.Equal(text, retrievedEntry.Text);
        Assert.Equal(savedEntryId, retrievedEntry.Id);
    }
}