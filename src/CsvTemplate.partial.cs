namespace CodeGeneration;

public partial class CsvTemplate
{
    public string StaticClassName { get; set; } = string.Empty;
    public CsvDocument CsvDocument { get; set; } = null!;
    public string FileLocation { get; set; } = string.Empty;
    
    private string EntityClassName => CsvDocument.EntityName;
    public CsvProperty[] Properties => CsvDocument.Properties;
    public CsvRow[] Rows => CsvDocument.Rows;
}