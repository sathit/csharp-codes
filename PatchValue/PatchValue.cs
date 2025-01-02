namespace PatchValue
{
    public sealed class PatchValue<T> where T : struct
    {
        public T? Value { get; private set; }

        private PatchValue()
        {
        }

        public PatchValue(T? value)
        {
            Value = value;
        }

        public static implicit operator T?(PatchValue<T> v) => v.Value;
    }

    public sealed class PatchValue
    {
        public string? Value { get; private set; }

        private PatchValue()
        {
        }

        public PatchValue(string? value)
        {
            Value = value;
        }

        public static implicit operator string?(PatchValue v) => v.Value;
    }
}