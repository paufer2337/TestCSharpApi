var x = Obj(new
{
    name = "John Doe",
    hobbies = new[]{
        new { name = "Fishing 2.0 true", cool=true, age=20.2, advantages = new[] { "hej", "hå" } },
        new { name = "Painting \"nice\"", cool=false,age=21.3,advantages = new[] { "hepp", "pepp" } }
    },
    good = true,
    miles = 128000
});

x.lastName = "Pelleson";

x.hobbies[0].advantages.Push(130);

var a = JSON.Stringify(x);

dynamic b = JSON.Parse(a);

//Log(x.hobbies[0].advantages.GetType());
//Log(b.hobbies[0].advantages.GetType());

var c = ((Arr)b.hobbies).Map(x => Obj(new { ___ = x, name = x.name + " Yo" }));
//Log(c);

var g = Obj(new
{
    firstName = "Anders",
    lastName = "Svensson"
});

var m = Arr(1, 2, 3, 4, 5.5);

JSON.Highlight = true;
Log(b);