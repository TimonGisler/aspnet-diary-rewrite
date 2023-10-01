using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewriteTests_xunit.Helpers;


/**
 * Creates various classes which are used for testing.
 */
public class TestApplicationFactory
{
    
    public DiaryApplicationClient DiaryApplicationClientForUnauthenticatedUser { get; }
    public  DiaryApplicationClient DiaryApplicationClientForUser1 { get; }
    public DiaryApplicationClient DiaryApplicationClientForUser2 { get; }
    public DiaryDbContext DiaryDbContext { get; }
    
    
    public TestApplicationFactory()
    {
        var customWebApplicationFactory = new CustomWebApplicationFactory();

        //seed database with common test data
        var user1RegisterData  = new UserData("testUserMail1@test.com", "1testPw123");
        var user2RegisterData  = new UserData("testUserMail2@test.com", "2testPw123");
        
        //create DiaryApplicationClient instances for users
        var httpClient = customWebApplicationFactory.CreateClient();
        DiaryApplicationClientForUnauthenticatedUser = new DiaryApplicationClient(httpClient);
        DiaryApplicationClientForUser1 = new DiaryApplicationClient(httpClient, user1RegisterData);
        DiaryApplicationClientForUser2 = new DiaryApplicationClient(httpClient, user2RegisterData);
        
        //the database
        DiaryDbContext = customWebApplicationFactory.diaryDbContext;

    }
    
    
}