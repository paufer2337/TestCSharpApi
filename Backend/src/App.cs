using System.Text.RegularExpressions;

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

//Log(x.hobbies[0].advantages.GetType());
//Log(b.hobbies[0].advantages.GetType());

var c = ((Arr)b.hobbies).Map(x => Obj(new { ___ = x, name = x.name + " Yo" }));
//Log(c);

// var ha = Regex.Replace(JSON.Stringify(c, true), "\"([^\"]*)\":", "\u001b[37m__quote__$1__quote__:\u001b[0m");
// ha = Regex.Replace(ha, "\"([^\"]*)\"", "\u001b[33m\"$1\"\u001b[0m");
// ha = ha.Replace("__quote__", "\"");
// Log(ha);

//Log("\u001b[38;5;" + 159 + "m" + "hello stack" + "\u001b[0m" + "\u001b[" + 37 + "m" + "I am fine");

//var d = Arr(1, 2, 3, Arr(4, 5, Arr(9, 10, Arr(98, 14))), 6, 7, 8);
//Log(d.Flat(3));
