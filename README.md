# Trigger-and-MultiQ-Game-
Uses a HAS-A relationship by reusing closePrime class.

Design and implement two classes, each of which encapsulate distinct closePrime objects. 

 

The first type, trigger, uses 2 closePrime objects to identify all pinged values that 
yield “stepped prime”s with the same last digit.  For example, if a trigger object 
encapsulated closePrime objects with hidden digits 2 and 5 then a pinged value of 81 
would satisfy this criterion (2 steps away: 83, 89 => 97; 5 steps away: 83, 89, 97, 
101, 103 => 107) while a pinged value of 78 would not. Cumulative statistics may also
be released.

The second type, multiQ, tracks some number of closePrime objects at a given time and

returns the maximum and minimum ‘stepped prime’ resulting from a query across all 
encapsulated closePrimes. Supports cumulative statistics. Adds/Removes closePrime objects.
