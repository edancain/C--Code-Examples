struct InvalidUnmanaged
{
    public string Text;      // Invalid: strings are reference types
    public object Data;      // Invalid: object is a reference type
    public int[] Numbers;    // Invalid: arrays are reference types
    public MyClass Instance; // Invalid: classes are reference types
}

class MyClass { }


struct ValidUnmanaged
{
    public int Number;           // Valid: primitive type
    public float Value;         // Valid: primitive type
    public DateTime Time;       // Valid: value type containing only unmanaged types
    public MyEnum Status;       // Valid: enums are value types
    public OtherValidStruct Nested; // Valid: another unmanaged struct
}

struct OtherValidStruct
{
    public int X;
    public int Y;
}

enum MyEnum
{
    One,
    Two
}