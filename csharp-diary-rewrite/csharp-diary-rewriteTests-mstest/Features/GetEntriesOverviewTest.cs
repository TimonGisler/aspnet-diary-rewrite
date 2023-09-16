using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace csharp_diary_rewrite.Features.Tests
{
    [TestClass()]
    public class GetEntriesOverviewTest
    {
        [TestMethod()]
        public void not_logged_in_user_should_not_be_able_to_fetch_any_entries()
        {
            Assert.Fail("Not Implemented");
        }
        
        [TestMethod()]
        public void overview_only_returns_entries_of_this_user()
        {
            Assert.Fail("Not Implemented");
        }
    }
}