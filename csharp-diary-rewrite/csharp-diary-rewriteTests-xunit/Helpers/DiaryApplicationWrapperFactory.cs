using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewriteTests_xunit.Helpers;


//creates DIaryApplicationWrapper instances for different users
public class DiaryApplicationWrapperFactory
{
    
    public DiaryApplicationClient DiaryApplicationClientForUnauthenticatedUser { get; }
    public  DiaryApplicationClient DiaryApplicationClientForUser1 { get; }
    public DiaryApplicationClient DiaryApplicationClientForUser2 { get; }
    
    
    public DiaryApplicationWrapperFactory()
    {
        var customWebApplicationFactory = new CustomWebApplicationFactory();

        //seed database with common test data
        var user1RegisterData  = new UserData("testUserMail1@test.com", "1testPw123");
        var user2RegisterData  = new UserData("testUserMail2@test.com", "2testPw123");
        
        //create DiaryApplicationWrapper instances for users
        DiaryApplicationClientForUnauthenticatedUser = new DiaryApplicationClient(customWebApplicationFactory);
        DiaryApplicationClientForUser1 = new DiaryApplicationClient(customWebApplicationFactory, user1RegisterData);
        DiaryApplicationClientForUser2 = new DiaryApplicationClient(customWebApplicationFactory, user2RegisterData);
    }
    
    
}