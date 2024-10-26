using System.Diagnostics;

namespace CsvSourceGeneration;

[DebuggerDisplay("{RowName}: {Fields.Length} fields")]
public record CsvRow(string RowName, CsvField[] Fields)
{
    public string RowName { get; } = RowName;
    public CsvField[] Fields { get; } = Fields;
}