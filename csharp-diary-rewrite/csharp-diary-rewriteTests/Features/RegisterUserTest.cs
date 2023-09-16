using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace csharp_diary_rewrite.Features.Tests
{
    [TestClass()]
    public class RegisterUserTest
    {
        [TestMethod()]
        public void not_logged_in_user_cannot_save_anything()
        {
            var webFactory = new WebApplicationFactory<Program>();
            var httpClient = webFactory.CreateClient();
            
            var response = httpClient.GetAsync("/api/hello").Result;
            
            Assert.AreEqual("HELLO", response.Content.ReadAsStringAsync().Result);

        }
    }
    

}