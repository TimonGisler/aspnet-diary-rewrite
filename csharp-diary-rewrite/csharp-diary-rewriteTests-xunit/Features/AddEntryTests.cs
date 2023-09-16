using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class AddEntryTests
{
    [Fact]
    public void not_logged_in_user_cannot_save_anything()
    {
        Assert.Fail("Not Implemented");
    }

    [Fact]
    public void logged_in_user_can_save_entry()
    {
        Assert.Fail("Not Implemented");
    }

    [Fact]
    public void user_with_wrong_pw_should_not_be_able_to_save_anything()
    {
        Assert.Fail("Not Implemented");
    }
}