dynamic x = Obj(new
{
    name = "John Doe",
    hobbies = new[]{
        new { name = "Fishing", advantages = new[] { "hej", "hå" } },
        new { name = "Painting", advantages = new[] { "hepp", "pepp" } }
    }
});

((Arr)x.hobbies[1].advantages).ForEach((x, i) => Log(i, x));

var y = Arr(
    new { name = "John" },
    new { name = "Anna" }
);

y[0].lastName = "Henrysson";
Log(y[0].lastName);