// Linear search for comparison
    private static int LinearSearch(int[] arr, int target)
    {
        int steps = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            steps++;
            if (arr[i] == target)
            {
                Console.WriteLine($"Found in {steps} steps");
                return i;
            }
        }
        Console.WriteLine($"Not found after {steps} steps");
        return -1;
    }