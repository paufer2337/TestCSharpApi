using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var x = Obj(new
{
    name = "John Doe",
    hobbies = new[]{
        new { name = "Fishing", age=20.1, advantages = new[] { "hej", "hå" } },
        new { name = "Painting", age=21.2,advantages = new[] { "hepp", "pepp" } }
    }
});

x.lastName = "Pelleson";

var a = JSON.Stringify(x);

dynamic b = JSON.Parse(a);

Log(x.hobbies[0].advantages.GetType());
Log(b.hobbies[0].advantages.GetType());

var c = ((Arr)(b.hobbies)).Map(x => Obj(new { SPREAD = x, name = x.name + " Yo" }));
Log(c);