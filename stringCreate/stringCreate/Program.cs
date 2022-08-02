namespace stringCreate
{
    internal class Program
    {
        static string GetJoinedStringValueFromArray(string[] values)
        {
            // Calculate final length
            int length = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string value = values[i];
                // Skip null and empty values
                if (value != null && value.Length > 0)
                {
                    if (length > 0)
                    {
                        // Add seperator
                        length++;
                    }

                    length += value.Length;
                }
            }
#if NETCOREAPP
            // Create the new string
            return string.Create(length, values, (span, strings) => {
                int offset = 0;
                // Skip null and empty values
                for (int i = 0; i < strings.Length; i++)
                {
                    string value = strings[i];
                    if (value != null && value.Length > 0)
                    {
                        if (offset > 0)
                        {
                            // Add seperator
                            span[offset] = ',';
                            offset++;
                        }

                        value.AsSpan().CopyTo(span.Slice(offset));
                        offset += value.Length;
                    }
                }
            });
#else
                var sb = new ValueStringBuilder(length);
                bool hasAdded = false;
                // Skip null and empty values
                for (int i = 0; i < values.Length; i++)
                {
                    string value = values[i];
                    if (value != null && value.Length > 0)
                    {
                        if (hasAdded)
                        {
                            // Add seperator
                            sb.Append(',');
                        }

                        sb.Append(value);
                        hasAdded = true;
                    }
                }

                return sb.ToString();
#endif
        }
    
        static void Main(string[] args)
        {
            string[] fruts = { "Apple", "Banana", "Cinnamon", "Dates", "Emblica" };
            Console.WriteLine(GetJoinedStringValueFromArray(fruts));
        }
    }
}