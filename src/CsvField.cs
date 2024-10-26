using System.Diagnostics;

namespace CsvSourceGeneration;

[DebuggerDisplay("{FieldName}: {FormattedValue}")]
public record CsvField(CsvType FieldType, string FieldName, string Value)
{
    public CsvType FieldType { get; } = FieldType;
    public string FieldName { get; } = FieldName;
    public string Value { get; } = Value;
    public string FormattedValue => GetFormattedValue();

    private string GetFormattedValue()
    {
        return FieldType switch
        {
            CsvType.Bool => Value.ToLower(),
            CsvType.Id => $"new IdString(\"{Value}\")",
            CsvType.String => $"\"{Value.Trim().Trim(['"'])}\"",
            CsvType.Float => $"{Value}f",
            CsvType.TimeSpan => $"TimeSpan.FromSeconds({Value.Substring(0, Value.Length - 1)})",
            _ => Value
        };
    }
}