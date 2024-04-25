var dogs = new Arr(
    new { name = "Fido", age = 5, breed = "labrador" },
    new { name = "Cassie", age = 8, breed = "collie" },
    new { name = "Scooby", age = 7, breed = "Great Dane" }
);

// Change the name of the second dog
dogs[1].name = "Lassie";

Log(dogs[1].name);  // Expected output: "Lassie"

foreach (var dog in dogs)
{
    Log($"{dog.name} is a {dog.age} year old {dog.breed}.");
}
// Expected output:
// "Fido is a 5 year old labrador."
// "Lassie is a 8 year old collie."
// "Scooby is a 7 year old Great Dane."