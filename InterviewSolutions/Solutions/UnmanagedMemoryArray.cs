unsafe
{
    // Stack allocated array
    int* numbers = stackalloc int[100];
    
    // Use like an array
    for (int i = 0; i < 100; i++)
    {
        numbers[i] = i;
    }
}

// int* is a pointer type declaration used in unsafe contexts. 
// The asterisk (*) indicates that this is a pointer to an integer value 
// rather than the integer value itself.