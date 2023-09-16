using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace csharp_diary_rewrite.Features.Tests
{
    [TestClass()]
    public class DeleteEntryTest
    {
        [TestMethod()]
        public void not_logged_in_user_should_not_be_able_to_deleta_any_entries()
        {
            Assert.Fail("Not Implemented");
        }
        
        [TestMethod()]
        public void user_is_unable_to_delete_entry_from_someone_else()
        {
            Assert.Fail("Not Implemented");
        }
        
        [TestMethod()]
        public void user_can_delete_his_own_entry()
        {
            Assert.Fail("Not Implemented");
        }
    }
}