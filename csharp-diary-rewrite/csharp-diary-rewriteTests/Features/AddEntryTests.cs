using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace csharp_diary_rewrite.Features.Tests
{
    [TestClass()]
    public class AddEntryTests
    {
        [TestMethod()]
        public void SaveEntryTest()
        {
            var webFactory = new WebApplicationFactory<Program>();
            var httpClient = webFactory.CreateClient();
            
            var response = httpClient.GetAsync("/api/hello").Result;
            
            Assert.AreEqual("HELLO", response.Content.ReadAsStringAsync().Result);
        }
    }
}