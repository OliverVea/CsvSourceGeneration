# Static CSV Source Generator [![NuGet](https://img.shields.io/nuget/v/CsvSourceGeneration?logo=nuget)](https://www.nuget.org/packages/CsvSourceGeneration) [![GitHub](https://img.shields.io/github/license/OliverVea/CsvSourceGeneration)](LICENCE.md)

This package allows for generating static C# classes allowing for easy, type safe and performant row-wise access to the data.

## Usage

Add a package reference to your project:

```xml
    <ItemGroup>
        <PackageReference Include="CsvSourceGeneration" Version="0.0.2" />
    </ItemGroup>
```

Then include a file with the `.csv` extension:

```xml
    <ItemGroup>
        <AdditionalFiles Include="CsvFiles\MyFile.csv" />
    </ItemGroup>
```

```csv
Entry,Message
First,Hello World!
```

When the project is built next, the data contained in the csv is available through a static class with the same name as the csv file:

```csharp
Console.WriteLine($"Some data from the csv file: {MyFile.First.Value}");
// Some data from the csv file: Hello World!
```

## Data types

Several field types (`FieldType` enumerable) are supported.

The data type of a column is deduced from the first row based on the following rules:

1. If the column name is `id` (ignoring case), values are parsed into `IdString`
2. If the cell value ends with `s`, values are parsed into `TimeSpan` using `TimeSpan.FromSeconds`
3. If the cell value can be parsed with `bool.TryParse`, values are parsed into `bool`
4. If the cell value can be parsed with `int.TryParse`, values are parsed into `int`
5. If the cell value can be parsed with `float.TryParse`, values are parsed into `float`
6. If the cell value can be parsed with `double.TryParse`, values are parsed into `double`
7. Otherwise, values are assumed to be strings.

Example CSV content with each field type:

| Example | Id    | TimeSpan | Bool | Int | Float | Double | String       |
|---------|-------|----------|------|-----|-------|--------|--------------|
| First   | first | 1.0s     | true | 10  | 10.0  | 10.0d  | Hello World! | 

