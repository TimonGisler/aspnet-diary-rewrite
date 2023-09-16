using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace csharp_diary_rewrite.Features.Tests
{
    [TestClass()]
    public class AddEntryTests
    {
        [TestMethod()]
        public void not_logged_in_user_cannot_save_anything()
        {
            Assert.Fail("Not Implemented");
        }
        
        [TestMethod()]
        public void logged_in_user_can_save_entry()
        {
            Assert.Fail("Not Implemented");
        }
        
        [TestMethod()]
        public void user_with_wrong_pw_should_not_be_able_to_save_anything()
        {
            Assert.Fail("Not Implemented");
        }
    }
    

}