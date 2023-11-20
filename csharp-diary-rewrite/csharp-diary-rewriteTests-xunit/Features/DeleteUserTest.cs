﻿using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class DeleteUserTest
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    private readonly DiaryDbContext _diaryDbContext;

    
    public DeleteUserTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
        _diaryDbContext = testApplicationFactory.DiaryDbContext;
    }
    
    [Fact]
    public void user_deletion_deletes_user_and_all_entries_associated()
    {
        //save entry which should also be deleted
        _diaryApplicationClientForUser1.SaveEntry(new SaveEntryCommand("user_deletion_deletes_user_and_all_entries_associated", "text"));
        var response = _diaryApplicationClientForUser1.DeleteUser();

        response.EnsureSuccessStatusCode();
        //make sure the user was deleted
        var user = _diaryDbContext.Users.FirstOrDefault(u => u.Email == _diaryApplicationClientForUser1.UserOfThisClient!.Email);
        //make sure the entries were deleted as well
        var entryFromDb = _diaryDbContext.Entries.FirstOrDefault(e => e.Creator.Email == _diaryApplicationClientForUser1.UserOfThisClient!.Email);
        Assert.Null(user);
        Assert.Null(entryFromDb);
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