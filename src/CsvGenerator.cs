using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CodeGeneration;

[Generator]
public class CsvGenerator : ISourceGenerator
{
    private record CsvFile(AdditionalText AdditionalText)
    {
        public AdditionalText AdditionalText { get; } = AdditionalText;
    }

    public void Initialize(GeneratorInitializationContext context) { }

    public void Execute(GeneratorExecutionContext context)
    {
        var csvFiles = GetCsvFiles(context);

        foreach (var csvFile in csvFiles)
        {
            var (className, code) = ParseCsvFileToCode(csvFile);

            var sourceText = SourceText.From(code, Encoding.UTF8);
            context.AddSource($"Csv_{className}.g.cs", sourceText);
        }
    }

    private static IEnumerable<CsvFile> GetCsvFiles(GeneratorExecutionContext context)
    {
        foreach (var file in context.AdditionalFiles)
        {
            if (!Path.GetExtension(file.Path).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }
            
            yield return new CsvFile(file);
        }
    }

    private static (string ClassName, string SourceCode) ParseCsvFileToCode(CsvFile csvFile)
    {
        var csvText = csvFile.AdditionalText.GetText()?.ToString();

        if (string.IsNullOrWhiteSpace(csvText))
        {
            throw new ArgumentException("File did not contain any text", nameof(csvFile));
        }
        
        var path = csvFile.AdditionalText.Path.Substring(0, csvFile.AdditionalText.Path.Length - 1);
        
        var className = Path.GetFileNameWithoutExtension(path);
        var fileName = Path.GetFileName(path);
        
        var sourceCode = GenerateSourceCode(className, fileName, csvText);
        
        return (className, sourceCode);
    }

    private static string GenerateSourceCode(string staticClassName, string fileLocation, string csvText)
    {
        var reader = new CsvReader();
        var csvData = reader.ReadCsv(csvText);
        
        var parser = new CsvParser();
        var csvDocument = parser.ParseCsv(csvData);
        
        var csvTemplate = new CsvTemplate
        {
            StaticClassName = staticClassName,
            CsvDocument = csvDocument,
            FileLocation = fileLocation
        };
        
        return csvTemplate.TransformText();
    }
}
