namespace WebApp;
public static class Utils
{
    private static readonly Arr mockUsers = JSON.Parse(
        File.ReadAllText(FilePath("json", "mock-users.json"))
    );

    public static bool IsPasswordGoodEnough(string password)
    {
        return password.Length >= 8
            && password.Any(Char.IsDigit)
            && password.Any(Char.IsLower)
            && password.Any(Char.IsUpper)
            && password.Any(x => !Char.IsLetterOrDigit(x));
    }

    public static bool IsPasswordGoodEnoughRegexVersion(string password)
    {
        
        var pattern = @"^(?=.*[0-9])(?=.*[a-zåäö])(?=.*[A-ZÅÄÖ])(?=.*\W).{8,}$";
        return Regex.IsMatch(password, pattern);
    }

    private static readonly Arr badWords = ((Arr)JSON.Parse(
        File.ReadAllText(FilePath("json", "bad-words.json"))
    )).Sort((a, b) => ((string)b).Length - ((string)a).Length);

    public static string DeleteBadWords(string comment, string replaceWith = "---")
    {
        comment = " " + comment;
        replaceWith = " " + replaceWith + "$1";
        badWords.ForEach(bad =>

        {
            var pattern = @$" {bad}([\,\.\!\?\:\; ])";
            comment = Regex.Replace(
                comment, pattern, replaceWith, RegexOptions.IgnoreCase);
        });
        return comment[1..];
    }

    public static Arr CreateMockUsers()
    {
        Arr successFullyWrittenUsers = Arr();
        foreach (var user in mockUsers)
        {
            // user.password = "12345678";
            var result = SQLQueryOne(
                @"INSERT INTO users(firstName,lastName,email,password)
                VALUES($firstName, $lastName, $email, $password)
            ", user);

            if (!result.HasKey("error"))
            {
                user.Delete("password");
                successFullyWrittenUsers.Push(user);
            }
        }
        return successFullyWrittenUsers;
    }

    public static Arr RemoveMockUsers()
    {
    string jsonData = File.ReadAllText(FilePath("json", "mock-users.json"));
    Arr usersArray = JSON.Parse(jsonData);
    Arr RemovedUsers = Arr();
        foreach (var mockUser in usersArray)
        {
            var deletionResult = SQLQueryOne(
            @"DELETE FROM users WHERE firstName = $firstName AND lastName = $lastName",
            mockUser);
            if (!deletionResult.HasKey("error"))
            {
            mockUser.Delete("password");
            RemovedUsers.Push(mockUser);
            }
        }
            return RemovedUsers;
    }

    public static Obj CountDomainsFromUserEmails()
    {
        Arr users = SQLQuery("SELECT email FROM users");

        Dictionary<string, int> domainCounts = new Dictionary<string, int>();

        foreach (var user in users)
        {
            string email = user.email;
            string[] parts = email.Split('@');
            if (parts.Length == 2)
            {
                string domain = parts[1];
                if (!domainCounts.ContainsKey(domain))
                domainCounts[domain] = 1;
                else
                domainCounts[domain]++;
            }
        }

        Obj result = Obj();
        foreach (var entry in domainCounts)
        {
            result[entry.Key] = entry.Value;
        }
            return result;
    }
}
