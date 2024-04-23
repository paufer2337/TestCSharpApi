### Arr.Length
The length data property of an Arr instance represents the number of elements in that array. The value is an integer that is always numerically greater than the highest index in the array.

#### Syntax - setting Length
* Setting the length to a value smaller than the current length truncates the array — elements beyond the new length are deleted.
* Setting any array index beyond the current length extends the array — the length property is increased to reflect the new highest index. New elements with null values will be inserted.

#### Example
```cs
var arr = Arr(1, 2, 3);

Log(arr);
// [1, 2, 3]
Log(arr.Length);
// 3

arr.Length = 2;
Log(arr);
// [1, 2]

arr.Length = 5;
Log(arr);
// [1, 2, null, null, null]
Log(arr.Length);
// 5
```

### Arr.Pop()
The Pop() method of Arr instances removes the last element from an array and returns that element. This method changes the length of the array.

#### Example
```cs
var plants = Arr("broccoli", "cauliflower", "cabbage", "kale", "tomato");

Log(plants.Pop());
// Expected output: "tomato"

Log(plants);
// Expected output: ["broccoli", "cauliflower", "cabbage", "kale"]

plants.Pop();

Log(plants);
// Expected output: ["broccoli", "cauliflower", "cabbage"]
```

### Arr.Push()
The Push() method of Arr instances adds the specified elements to the end of an array and returns the new length of the array.

#### Example
```cs
var animals = Arr("pigs", "goats", "sheep");

var count = animals.Push("cows");

Log(count);
// Expected output: 4

Log(animals);
// Expected output: ["pigs", "goats", "sheep", "cows"]

animals.Push("chickens", "cats", "dogs");
Log(animals);
// Expected output: ["pigs", "goats", "sheep", "cows", "chickens", "cats", "dogs"]
```

### Arr.Shift()
The Shift() method of Arr instances removes the first element from an array and returns that removed element. This method changes the length of the array.

#### Example
```cs
var array1 = Arr(1, 2, 3);

var firstElement = array1.Shift();

Log(array1);
// Expected output: [2, 3]

Log(firstElement);
// Expected output: 1
```

### Arr.Unshift()
The Unshift() method of Arr instances adds the specified elements to the beginning of an array and returns the new length of the array.

#### Example
```cs
var array1 = Arr(1, 2, 3);

Log(array1.Unshift(4, 5));
// Expected output: 5

Log(array1);
// Expected output: [4, 5, 1, 2, 3]
```

### Arr.Slice()
The Slice() method of Arr instances returns a new Arr which is a shallow copy of a portion of the original, selected from start to end (end not included). Start and end represent the index of items. The original array will not be modified.

#### Syntax
```cs
Slice();
Slice(start);
Slice(start, end);
```
**Note:** Negative index counts back from the end of the array.

#### Example
```cs
var animals = Arr("ant", "bison", "camel", "duck", "elephant");

Log(animals.Slice(2));
// Expected output: ["camel", "duck", "elephant"]

Log(animals.Slice(2, 4));
// Expected output: ["camel", "duck"]

Log(animals.Slice(1, 5));
// Expected output: ["bison", "camel", "duck", "elephant"]

Log(animals.Slice(-2));
// Expected output: ["duck", "elephant"]

Log(animals.Slice(2, -1));
// Expected output: ["camel", "duck"]

Log(animals.Slice());
// Expected output: ["ant", "bison", "camel", "duck", "elephant"]
```

### Arr.Splice()
The Splice() method of Arr instances changes the contents of an array by removing or replacing existing elements and/or adding new elements in place. It returns a new Arr of the removed elements.

To create a new Arr with a segment removed and/or replaced without mutating the original array, use ToSpliced(). To access part of an array without modifying it, see Slice().

#### Syntax
```cs
Splice(start)
Splice(start, deleteCount)
Splice(start, deleteCount, item1)
Splice(start, deleteCount, item1, item2)
Splice(start, deleteCount, item1, item2, /* …, */ itemN)
```

**Note:** Negative index counts back from the end of the array.

#### Example
```cs
var months = Arr("Jan", "March", "April", "June");

// Inserts at index 1
months.Splice(1, 0, "Feb");

Log(months);
// Expected output: ["Jan", "Feb", "March", "April", "June"]

// Replaces 1 element at index 4
var removedItems = months.Splice(4, 1, "May");

Log(removedItems);
// Expected output: ["June"]
Log(months);
// Expected output: ["Jan", "Feb", "March", "April", "May"]
```

### Arr.ToSpliced()
The ToSpliced() method of Arr instances is the copying version of the splice() method. It returns a new Arr with some elements removed and/or replaced at a given index.

#### Syntax
```cs
ToSpliced(start)
ToSpliced(start, deleteCount)
ToSpliced(start, deleteCount, item1)
ToSpliced(start, deleteCount, item1, item2)
ToSpliced(start, deleteCount, item1, item2, /* …, */ itemN)
```

**Note:** Negative index counts back from the end of the array.

#### Example
```cs
var months = Arr("Jan", "Mar", "Apr", "May");

// Inserting an element at index 1
var months2 = months.ToSpliced(1, 0, "Feb");
Log(months2); // ["Jan", "Feb", "Mar", "Apr", "May"]

// Deleting two elements starting from index 2
var months3 = months2.ToSpliced(2, 2);
Log(months3); // ["Jan", "Feb", "May"]

// Replacing one element at index 1 with two new elements
var months4 = months3.ToSpliced(1, 1, "Feb", "Mar");
Log(months4); // ["Jan", "Feb", "Mar", "May"]

// Original array is not modified
Log(months); // ["Jan", "Mar", "Apr", "May"]
```

### Arr.Reverse()
The Reverse() method of Arr instances reverses an array in place and returns the reference to the same array, the first array element now becoming the last, and the last array element becoming the first. 

To create a new Arr with the reversed element order, without mutating the original array, use ToReversed().

#### Example
```cs
var array1 = Arr("one", "two", "three");
Log("array1: " + array1);
// Expected output: array1: ["one", "two", "three"]

var reversed = array1.Reverse();
Log("reversed: " + array1);
// Expected output: reversed: ["three", "two", "one"]

// Careful: Reverse is destructive - it changes the original array.
// (And reversed and array1 both point to the same object.)

Log("array1: " + array1);
// Expected output: array1: ["three", "two", "one"]
```

### Arr.ToReversed()
The ToReversed() method of Arr instances is the copying counterpart of the reverse() method. It returns a new array with the elements in reversed order.

#### Example
```cs
var items = Arr(1, 2, 3);
Log(items); // [1, 2, 3]

var reversedItems = items.ToReversed();
Log(reversedItems); // [3, 2, 1]
Log(items); // [1, 2, 3]
```