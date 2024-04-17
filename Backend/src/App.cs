var array1 = Arr("one", "two", "three");
Console.WriteLine("array1: " + array1);
// Expected output: array1: ["one", "two", "three"]

var reversed = array1.Reverse();
Console.WriteLine("reversed: " + array1);
// Expected output: reversed: ["three", "two", "one"]

// Careful: reverse is destructive - it changes the original array.
// (reversed and array1 are just two pointers to the same object reference)

Console.WriteLine("array1: " + array1);
// Expected output: array1: ["three", "two", "one"]
