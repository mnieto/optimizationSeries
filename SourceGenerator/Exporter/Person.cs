using ExporterSourceGenerator.Shared;

namespace CSVExporter;


[Exporter]
public class Person
{
    public string Name {get; set; } = "";
    public int Age {get; set; }
    public string? Email {get; set; }
}