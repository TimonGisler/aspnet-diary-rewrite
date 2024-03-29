﻿using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class RegisterUserTest
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    private readonly DiaryDbContext _diaryDbContext;
    
    
    public RegisterUserTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;
        _diaryDbContext = testApplicationFactory.DiaryDbContext;

    }
    
    
    [Fact]
    public void no_two_users_with_same_mail_can_register()
    {
        const string mail = "testno_two_users_with_same_mail_can_register@test.com";
        const string password = "test123";
        
        //register first user, everything should be okay
        var registerUserCommand = new RegisterUserCommand(mail, password);
        var response = _unauthenticatedDiaryApplicationClient.RegisterUser(registerUserCommand);
        response.EnsureSuccessStatusCode(); //test if the status code is a success code (200-299)
        
        //register second user with same mail, should fail
        response = _unauthenticatedDiaryApplicationClient.RegisterUser(registerUserCommand);
        Assert.Equal(400, (int) response.StatusCode);

    }
    
    [Fact]
    public void register_user_saves_user_in_database()
    {
        const string mail = "testregister_user_saves_user_in_database@test.com";
        const string password = "test123";
        var registerUserCommand = new RegisterUserCommand(mail, password);
        
        var response = _unauthenticatedDiaryApplicationClient.RegisterUser(registerUserCommand);
        
        response.EnsureSuccessStatusCode(); //test if the status code is a success code (200-299)
        var user = _diaryDbContext.Users.FirstOrDefault(u => u.Email == mail);
        Assert.NotNull(user);
    }
}