var plants = Arr("broccoli", "cauliflower", "cabbage", "kale", "tomato");

Log(plants.Pop());
// Expected output: "tomato"

Log(plants);
// Expected output: ["broccoli", "cauliflower", "cabbage", "kale"]

plants.Pop();

Log(plants);
// Expected output: ["broccoli", "cauliflower", "cabbage"]

Log("Dumheter", 1, 2, 3, Arr(5, 6), true);

Log("nice\" ho", new { firstName = "Kalle", lastName = "Carlsson" });

Log("Fin logg");