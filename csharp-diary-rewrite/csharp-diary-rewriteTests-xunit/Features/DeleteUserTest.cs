using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class DeleteUserTest : IClassFixture<TestApplicationFactory>
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    public DeleteUserTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;

    }
    
    [Fact]
    public void user_deletion_deletes_user_and_all_entries_associated()
    {
        Assert.Fail("Not Implemented");
    }
    
    [Fact]
    public void user_cannot_delete_other_users_account()
    {
        Assert.Fail("Not Implemented");
    }
    
    [Fact]
    public void user_cannot_access_apis_after_deletion()
    {
        //the jwt should not work anymore after the accounts was deleted
        //(add some validation to check if the user for which this jwt was created still exists)
        //https://stackoverflow.com/questions/61371857/check-if-user-exists-in-asp-net-core-webapi-jwt-authentication
        Assert.Fail("Not Implemented");
    }
}