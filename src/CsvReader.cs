namespace CsvSourceGeneration;

public class CsvReader
{
    public IEnumerable<string[]> ReadCsv(string csvText, char newLine = '\n', char delimiter = ',')
    {
        var filteredCsvText = csvText.Replace("\r", "");
        
        return filteredCsvText.Split(newLine).Select(l => l.Split(delimiter));
    }
}