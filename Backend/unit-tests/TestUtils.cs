public class TestUtils
{
    [Fact]
    public void TestAdd()
    {
        // Assert
        Assert.Equal(12, Utils.Add(5, 7));
    }
}