usage for System.Runtime.CompilerService.Unsafe.

an example. (Microsoft.Extensions.Premitives)

public class StringValues
{
    private readonly object? _values; // <- not generic. usage wise string either string[]... 
    public int Count
    {
        get
        {
            // Take local copy of _values so type checks remain valid even if the StringValues is overwritten in memory
            object? value = _values; // this is used as just pointer.
            if (value is null)
            {
                return 0;
            }
            if (value is string)
            {
                return 1;
            }
            else
            {
                // Not string, not null, can only be string[]
                return Unsafe.As<string?[]>(value).Length; // <- instead simply static cast, call Cast function (As) -> why cannot check type?
            }
        }
    }
}
