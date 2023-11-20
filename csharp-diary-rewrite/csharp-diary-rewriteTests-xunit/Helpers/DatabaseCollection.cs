using Xunit;

namespace csharp_diary_rewriteTests_xunit.Helpers;

//https://xunit.net/docs/shared-context#collection-fixture
[CollectionDefinition("Database collection")]
public class DatabaseCollection  : ICollectionFixture<TestApplicationFactory>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}