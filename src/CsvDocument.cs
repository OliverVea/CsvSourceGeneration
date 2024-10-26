using System.Diagnostics;

namespace CodeGeneration;

[DebuggerDisplay("{Properties.Length} properties, {Rows.Length} rows")]
public class CsvDocument
{
    public string EntityName { get; set; } = string.Empty;
    public CsvProperty[] Properties { get; set; } = [];
    public CsvRow[] Rows { get; set; } = [];
}