var beasts = Arr("ant", "bison", "camel", "duck", "bison");

Log(beasts.IndexOf("bison"));     // Expected output: 1

// Start from index 2
Log(beasts.IndexOf("bison", 2));  // Expected output: 4

Log(beasts.IndexOf("giraffe"));   // Expected output: -1
