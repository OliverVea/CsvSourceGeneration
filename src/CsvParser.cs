namespace CsvSourceGeneration;

public class CsvParser
{
    public CsvDocument ParseCsv(IEnumerable<string[]> csvEnumerable)
    {
        var rows = new List<CsvRow>();
        
        using var csvEnumerator = csvEnumerable.GetEnumerator();
        csvEnumerator.MoveNext();
        
        var headerFields = csvEnumerator.Current;
        csvEnumerator.MoveNext();
        
        if (headerFields == null) throw new Exception("Empty csv file!");
        
        var entityName = headerFields[0];
        headerFields = headerFields.Skip(1).ToArray();

        var fields = csvEnumerator.Current;
        csvEnumerator.MoveNext();
        
        if (fields == null) throw new Exception("No data in csv file!");
        
        var properties = MapProperties(headerFields, fields.Skip(1).ToArray()).ToArray();
        
        while (fields is { Length: > 0 })
        {
            var rowName = fields[0];
            var rowFieldValues = fields.Skip(1).ToArray();
            
            if (rowFieldValues.Length != headerFields.Length) throw new Exception("Inconsistent number of fields in csv file!");

            var rowFields = MapFields(headerFields, rowFieldValues).ToArray();

            var row = new CsvRow(rowName, rowFields);
            rows.Add(row);
            
            fields = csvEnumerator.Current;
            csvEnumerator.MoveNext();
        }
        
        return new CsvDocument
        {
            EntityName = entityName,
            Properties = properties.ToArray(), 
            Rows = rows.ToArray()
        };
    }

    private IEnumerable<CsvField> MapFields(string[] headerFields, string[] rowFieldValues)
    {
        for (var i = 0; i < headerFields.Length; i++)
        {
            var header = headerFields[i];
            var value = rowFieldValues[i];
            var fieldType = GetCsvFieldType(header, value);
            var fieldName = StringToValidPropertyName(header);
            var field = new CsvField(fieldType, fieldName, value);
            
            yield return field;
        }
    }

    private static IEnumerable<CsvProperty> MapProperties(string[] headerFields, string[] firstRowFields)
    {
        for (var i = 0; i < headerFields.Length; i++)
        {
            var header = headerFields[i];
            var exemplar = firstRowFields[i];
            var fieldType = GetCsvFieldType(header, exemplar);
            var fieldName = StringToValidPropertyName(header);
            var property = new CsvProperty(fieldType, fieldName);
            
            yield return property;
        }
    }

    private static CsvType GetCsvFieldType(string header, string exemplar) => exemplar switch
    {
        _ when header.Equals("id", StringComparison.InvariantCultureIgnoreCase) => CsvType.Id,
        _ when exemplar.EndsWith("s") && float.TryParse(exemplar.Substring(0, exemplar.Length - 1), out _) => CsvType.TimeSpan,
        _ when bool.TryParse(exemplar, out _) => CsvType.Bool,
        _ when int.TryParse(exemplar, out _) => CsvType.Int,
        _ when float.TryParse(exemplar, out _) => CsvType.Float,
        _ when double.TryParse(exemplar, out _) => CsvType.Double,
        _ => CsvType.String
    };

    private static string StringToValidPropertyName(string s)
    {
        s = s.Trim();
        s = char.IsLetter(s[0]) ? char.ToUpper(s[0]) + s.Substring(1) : s;
        s = char.IsDigit(s.Trim()[0]) ? "_" + s : s;
        s = new string(s.Select(ch => char.IsDigit(ch) || char.IsLetter(ch) ? ch : '_').ToArray());
        return s;
    }
}