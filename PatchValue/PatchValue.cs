public class PatchValue<T> where T : struct
{
    public T? Value { get; private set; }

    private PatchValue() : this(null)
    {
    }

    public PatchValue(T? value)
    {
        Value = value;
    }

    public static implicit operator T?(PatchValue<T> v) => v.Value;
}
