using System.Net;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class GetEntriesOverviewTest
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    public GetEntriesOverviewTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;

    }
    
    [Fact]
    public void not_logged_in_user_should_not_be_able_to_fetch_any_entries()
    {
        var response = _unauthenticatedDiaryApplicationClient.GetEntriesOverview();
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    [Fact]
    public void overview_only_returns_entries_of_this_user()
    {
        //create entries with user 1
        var entry1Title = "entry1 for user1";
        var entry2Title = "entry2 for user1";

        var createEntryCommand = new SaveEntryCommand(entry1Title, "overview_only_returns_entries_of_this_user");
        _diaryApplicationClientForUser1.SaveEntry(createEntryCommand);
        var createEntryCommand2 = new SaveEntryCommand(entry2Title, "overview_only_returns_entries_of_this_user");
        _diaryApplicationClientForUser1.SaveEntry(createEntryCommand2);
        
        //create entry with user 2
        var createEntryCommand3 = new SaveEntryCommand("entry1 for user2", "overview_only_returns_entries_of_this_user");
        _diaryApplicationClientForUser2.SaveEntry(createEntryCommand3);
        
        //get overview with user 1
        var response = _diaryApplicationClientForUser1.GetEntriesOverview();
        
        response.EnsureSuccessStatusCode();
        var entries = response.Content.ReadAsAsync<List<EntryOverview>>().Result;
        Assert.Equal(2, entries.Count);
        Assert.Contains(entries, e => e.Title == entry1Title);
        Assert.Contains(entries, e => e.Title == entry2Title);
    }
}