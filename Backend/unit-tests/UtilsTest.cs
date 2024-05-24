namespace WebApp;

public class UtilsTest(Xlog Console)
{
    // Read all mock users from file
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );

    [Theory]
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnough(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnough(password));
    }

    [Theory]
    [InlineData("abC9#fgh", true)]  // ok
    [InlineData("stU5/xyz", true)]  // ok too
    [InlineData("abC9#fg", false)]  // too short
    [InlineData("abCd#fgh", false)] // no digit
    [InlineData("abc9#fgh", false)] // no capital letter
    [InlineData("abC9efgh", false)] // no special character
    public void TestIsPasswordGoodEnoughRegexVersion(string password, bool expected)
    {
        Assert.Equal(expected, Utils.IsPasswordGoodEnoughRegexVersion(password));
    }

    [Theory]
    [InlineData(
        "---",
        "Hello, I am going through hell. Hell is a real fucking place " +
            "outside your goddamn comfy tortoiseshell!",
        "Hello, I am going through ---. --- is a real --- place " +
            "outside your --- comfy tortoiseshell!"
    )]
    [InlineData(
        "---",
        "Rhinos have a horny knob? (or what should I call it) on " +
            "their heads. And doorknobs are damn round.",
        "Rhinos have a --- ---? (or what should I call it) on " +
            "their heads. And doorknobs are --- round."
    )]
    public void TestDeleteBadWords(string replaceWith, string original, string expected)
    {
        Assert.Equal(expected, Utils.DeleteBadWords(original, replaceWith));
    }

    [Fact]
    public void TestCreateMockUsers()
    {
        // Get all users from the database
        Arr usersInDb = SQLQuery("SELECT email FROM users");
        Arr emailsInDb = usersInDb.Map(user => user.email);
        // Only keep the mock users not already in db
        Arr mockUsersNotInDb = mockUsers.Filter(
            mockUser => !emailsInDb.Contains(mockUser.email)
        );
        // Get the result of running the method in our code
        var result = Utils.CreateMockUsers();
        // Assert that the CreateMockUsers only return
        // newly created users in the db
        Console.WriteLine($"The test expected that {mockUsersNotInDb.Length} users should be added.");
        Console.WriteLine($"And {result.Length} users were added.");
        Console.WriteLine("The test also asserts that the users added " +
            "are equivalent (the same) as the expected users!");
        Assert.Equivalent(mockUsersNotInDb, result);
        Console.WriteLine("Test has passed correctly");
    }

    [Fact]
    public void MockUsersRemoved()
    {
    string jsonData = File.ReadAllText(FilePath("json", "mock-users.json"));
    Arr mockData = JSON.Parse(jsonData);
    Arr usersRemoved = Utils.RemoveMockUsers();
    Arr removedUsersEmail = usersRemoved.Map(user => user.email);
    Arr UsersInDatabase = SQLQuery("select email from users");

    foreach (var removedUser in usersRemoved)
    {
        Assert.DoesNotContain(removedUser.email, UsersInDatabase.Map(dbUser => dbUser.email));
    }
    Assert.Equivalent(mockData, usersRemoved);
    Console.WriteLine($"{usersRemoved.Length} users were successfully removed from the database");
    }


    [Fact]
    public void TestCountDomainsFromUserEmails()
    {
        Obj domainCounts = Utils.CountDomainsFromUserEmails();
        Arr users = SQLQuery("SELECT email FROM users");

        Dictionary<string, int> expectedCounts = new Dictionary<string, int>();

        foreach (var user in users)
        {
        string email = user.email;
        string[] parts = email.Split('@');

            if (parts.Length == 2)
            {
                string domain = parts[1];
                if (!expectedCounts.ContainsKey(domain))
                expectedCounts[domain] = 1;
                else
                expectedCounts[domain]++;
            }
        }
        
        foreach (var entry in expectedCounts)
        {
            Assert.True(domainCounts.HasKey(entry.Key));
            Assert.Equal(entry.Value, (int)domainCounts[entry.Key]);
        }
        Console.WriteLine("All tests for counting domainshave passed successfully!");
    }
}
