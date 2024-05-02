public static class Utils
{

    public static int Add(int x, int y)
    {
        return x + y;
    }

    public static Arr CreateMockUsers()
    {
        var read = File.ReadAllText(
            Path.Combine("json", "mock-users.json")
        );
        Arr users = JSON.Parse(read);
        users = users.Map(x => new
        {
            ___ = x,
            password = x.email.Substring(0, 1).ToUpper() + x.email.Substring(1)
        });
        users.ForEach(x =>
        {
            Log(SQLQueryOne(
                @"INSERT INTO users (firstName,lastName,email,password) 
                VALUES ($firstName,$lastName,$email,$password)", x
            ));
            Log(x);
        });
        return Arr();
    }
}
