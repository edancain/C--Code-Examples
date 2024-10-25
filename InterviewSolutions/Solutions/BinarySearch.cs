/// Binary Search Algorithm
    public int BinarySearch(int[] array, int target)
    {
        int left = 0;
        int right = array.length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (array[mid] == target)
                return mid;

            if (array[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }



/*
break down the pros and cons of Binary Search:
Pros:

Efficiency:

Time complexity of O(log n) compared to O(n) for linear search
Extremely efficient for large datasets
Number of steps grows very slowly as data size increases


Predictable Performance:

Consistent performance characteristics
Worst case is still O(log n)
Works well with CPU caching due to predictable memory access patterns


Memory Efficient:

In-place algorithm
No additional space required except for a few variables
Constant space complexity O(1)


Well-suited for:

Large sorted datasets
Systems with limited memory
Frequently accessed data structures
Database indexing implementations



Cons:

Requires Sorted Data:

Array must be sorted before binary search can be used
Sorting overhead can be significant
Not suitable for frequently updated collections that need resorting


Array Requirements:

Only works with arrays or similar data structures that allow random access
Not suitable for linked lists or stream data
Requires contiguous memory allocation


Limited Use Cases:

Only finds exact matches (without modification)
Not suitable for fuzzy searches
Cannot find all occurrences of an element efficiently


Potential Issues:

Integer overflow in mid-point calculation (addressed in the code)
Not cache-friendly for very large datasets compared to linear search on small datasets
Can be slower than linear search for small datasets due to more complex calculations


Implementation Complexity:

More complex to implement correctly than linear search
Edge cases need careful handling
Recursive implementations can cause stack overflow for very large datasets