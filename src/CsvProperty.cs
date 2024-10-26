using System.Diagnostics;

namespace CodeGeneration;

[DebuggerDisplay("{Name}: {Type}")]
public record CsvProperty(CsvType Type, string Name)
{
    public CsvType Type { get; set; } = Type;
    public string Name { get; set; } = Name;

    public string GetTypeString()
    {
        return Type switch
        {
            CsvType.Id => "IdString",
            CsvType.String => "string",
            CsvType.TimeSpan => "TimeSpan",
            CsvType.Double => "double",
            CsvType.Float => "float",
            CsvType.Int => "int",
            CsvType.Bool => "bool",
            _ => throw new NotSupportedException($"Cannot get type string for unsupported CsvType {Type}")
        };
    }
    public string GetInitializerString()
    {
        return Type switch
        {
            CsvType.Id => " = new IdString(string.Empty);",
            CsvType.String => " = string.Empty;",
            CsvType.TimeSpan => " = TimeSpan.Zero;",
            CsvType.Double => "",
            CsvType.Float => "",
            CsvType.Int => "",
            CsvType.Bool => "",
            _ => throw new NotSupportedException($"Cannot get initializer string for unsupported CsvType {Type}")
        };
    }
}