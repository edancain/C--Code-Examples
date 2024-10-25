public ref int GetReference(int[] arr, bool useFirst)
{
    // Both references must be definitely assigned before use
    ref int ref1 = ref arr[0];
    ref int ref2 = ref arr[1];
    
    // Return one of the references based on condition
    return ref (useFirst ? ref ref1 : ref ref2);
}