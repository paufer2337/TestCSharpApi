/*var serverName = "ironboy's minimal API server";
var isSpa = true;
Debug.on = true;
CheckAcl.on = true;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Middleware: Set the server name response header 
// and touch the user session with a new timestamp
// and apply ACL rules to check if access is allowd
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Server", serverName);
    Debug.Log("route", context);
    Session.Touch(context);
    if (CheckAcl.Allow(context)) { await next(context); }
    else
    {
        // ACL says the route is not allowed
        context.Response.StatusCode = 405;
        await context.Response.WriteAsJsonAsync(
            new { error = "Not allowed." }
        );
    }
});

// Set up routes, error handling, ACL and session purging
ErrorHandler.Start(app, serverName);
FileServer.Start(app, isSpa, "..", "Frontend");
LoginRoutes.Start(app);
Rest.Start(app);
CheckAcl.Start();
Session.DeleteOldSessions(2);

// Start the server on port 3001
app.Run("http://localhost:3001");*/

/*using ExtensionMethods;

var a = new
{
    firstName = "Olle",
    lastName = "Svensson",
    cool = true,
    age = 42,
    weigth = 80.5
}.Dynamic();

Console.WriteLine(a.GetStr("firstName"));

a.Set("middleName", "Afzelius");
a.Set("middleName", "Dum");
//a.Delete("lastName");

Console.WriteLine(a.GetStr("lastName"));


Console.WriteLine(a.GetInt("age"));

Console.WriteLine(a.GetDbl("weigth"));

var arr = new[] {
    new { firstName = "Olle" },
    new { firstName = "Anna" }
}.Dynamic();

arr.Push(
    new { firstName = "Olga" },
    new { firstName = "Agneta" }
);

Console.WriteLine(arr);*/

dynamic a = new Dynamic(new { job = "CEO", salary = 50000 });

a.Merge(new { footPrint = "large", height = 1.84 });

a.firstName = "Thomas";
a.lastName = "Frank";
a["middleName"] = "Irons";
a["middleName"] = "ironboy";

a.Delete("footPrint");

Log.Out(a.middleName);
Log.Out(a["lastName"]);

Log.Out(a);

dynamic b = new Dynamic(new[] { "Hej", "Hejdå" });

b.Push("Really hejdå", "For the last time");

b.Unshift("Prehej", "Almost ready for hej");
Log.Out("-----b---", b);
Log.Out("POP", b.Pop());
Log.Out("SHIFT", b.Shift());

Log.Out("WHOLE", b);
Log.Out("SLICE", b.Slice());
Log.Out("SLICE1", b.Slice(1));
Log.Out("SLICE1,3", b.Slice(1, 3));
b.Reverse();
Log.Out("REVERSED", b);
Log.Out("SLICE(1,3)", b.Slice(1, 3));



//Console.WriteLine(b.Map(x => x));

//Log.Out("Map", Dynamic.Map((object)b, x => x + "hej"));

/*Log.Out((b as object).Map((x, i) => x + "hej" + i));
Log.Out((b as object).Filter(x => ((string)x).Contains("Almost")));*/


// var eh = Dynamic.Mappy(x => x + "hohoho")
/*Log.Out("MAP", b.Do(Dynamic.Map((x, i) => (x + "") + i + " IS DA BEST")));
Log.Out("FILTER", b.Do(Dynamic.Filter(x => (x + "").Contains("hejdå"))));


Log.Out("CHAIN",
    b
        .Do(Dynamic.Map((x, i) => (x + "") + i + " IS DA BEST"))
        .Do(Dynamic.Filter(x => (x + "").Contains("hejdå")))
);*/

//Log.Out(b.Map("(x,i)=>x+i+'hejsan'").Filter("x=>x.Contains('3')"));

var p = Dynamic.Prepare;
var searchFor = "då";

Log.Out("MAP+FILTER", b
    .Map(p((string x, int i) => x + i + "hej"))
    .Filter(p((string x) => x.Contains(searchFor), new { searchFor }))
);

dynamic f = new Dynamic(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 1 });
Log.Out(f);
Log.Out("FILTER", f.Filter(p((int x) => x == 1)));
Log.Out("FIND", f.Find(p((int x) => x == 15)));
Log.Out("FIND", f.FindLastIndex(p((int x) => x == 1)));
Log.Out("SOME", f.Some(p((int x) => x == 1)));

dynamic g = new Dynamic(new[] { "Anna", "Anna", "Eva" });

Log.Out("EVERY", g.Every(p((string x) => x == "Anna")));

f = new Dynamic(new int[] { 1, 2, 3, 4, 5 });
Log.Out("REDUCE NO SEED", f.Reduce(p((int a, int c) => a + c)));
Log.Out("REDUCE SEED", f.Reduce(p((int a, int c) => a + c), 10));
Log.Out("REDUCERIGHT NO SEED", f.ReduceRight(p((int a, int c) => a + c)));
Log.Out("REDUCERIGHT SEED", f.ReduceRight(p((int a, int c) => a + c), 10));

f = new Dynamic(new string[] { "Anna", "Beata", "Eva" });
Log.Out("REDUCE STR NO SEED", f.Aggregate(p((string a, string c) => a + c)));
Log.Out("REDUCE STR  SEED", f.Reduce(p((string a, string c) => a + " " + c), "Alla:"));
Log.Out("REDUCERIGHT STR NO SEED", f.ReduceRight(p((string a, string c) => a + c)));
Log.Out("REDUCERIGHT STR  SEED", f.ReduceRight(p((string a, string c) => a + " " + c), "Alla:"));
Log.Out("NAMES", f);

Log.Out("INCLUDES", f.Includes("Anna"));
Log.Out("CONTAINS", f.Contains("Anna"));


f = new Dynamic(new[] { 5, 3.2, 2, 1, 4 });
var h = p((decimal a, decimal b) => a - b);
Log.Out("SORT FUNC", h);
Log.Out("SORT", f.ToSorted(h));

//Log.Out("ToSpliced", f.ToSpliced(1, 1, 6, 7));
//Log.Out("AFTER", f);

dynamic j = new Dynamic(new[] { "hej", "hopp", "nu" });

Holder j2 = j._();

Log.Out(j2.Map((int x) => x + " coolt!"));