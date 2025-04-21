# Big O Complexity
## EdgeExists Method
This method has a big O complexity of O(n) because it goes through each edge in the from room list to see if the there is a to room contianed in each edge.

## GetChallenge Method
This method has a big O complexity of O(log(n)) because with each check, it halves the number of possible challenges. It uses a binary search tree and uses a chalange id to balance the tree.

## getLoot Method
This method has a big O complexity of O(n) because each time it is called, it adds each item into a list. But this could be optimized by making the loot list a property in the class. This would change it to a O(1) because we would only be accessing an index in an array