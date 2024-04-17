var animals = Arr("ant", "bison", "camel", "duck", "elephant");

Console.WriteLine(animals.Slice(2));
// Expected output: ["camel", "duck", "elephant"]

Console.WriteLine(animals.Slice(2, 4));
// Expected output: ["camel", "duck"]

Console.WriteLine(animals.Slice(1, 5));
// Expected output: ["bison", "camel", "duck", "elephant"]

Console.WriteLine(animals.Slice(-2));
// Expected output: ["duck", "elephant"]

Console.WriteLine(animals.Slice(2, -1));
// Expected output: ["camel", "duck"]

Console.WriteLine(animals.Slice());
// Expected output: ["ant", "bison", "camel", "duck", "elephant"]