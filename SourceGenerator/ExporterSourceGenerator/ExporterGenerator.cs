using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Generator]
public class ExporterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var exporterClasses = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (s, _) => s is ClassDeclarationSyntax cds && cds.AttributeLists.Count > 0,
                transform: (ctx, _) => GetExporterClass(ctx))
            .Where(c => c is not null);

        context.RegisterSourceOutput(exporterClasses, (spc, classInfo) =>
        {
            try
            {
                var source = GenerateExporterMethod(classInfo!);
                spc.AddSource($"{classInfo!.ClassName}_Exporter.g.cs", SourceText.From(source, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                var descriptor = new DiagnosticDescriptor(
                    "EXPORTGEN001",
                    "Exporter Generation Error",
                    $"Unexpected error occurred: {ex.Message}",
                    nameof(ExporterGenerator),
                    DiagnosticSeverity.Error,
                    true);
                spc.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None));
            }
        });
    }

    private static ExporterClassInfo? GetExporterClass(GeneratorSyntaxContext context)
    {
        var classSyntax = (ClassDeclarationSyntax)context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(classSyntax) as INamedTypeSymbol;
        if (symbol == null) return null;

        var exporterAttribute = symbol.GetAttributes().FirstOrDefault(a => a.AttributeClass?.Name == nameof(ExporterSourceGenerator.Shared.ExporterAttribute));
        if (exporterAttribute is null) return null;
        var separator = "\t"; // default value
        var separatorArg = exporterAttribute.NamedArguments.FirstOrDefault(na => na.Key == "Separator");
        if (!separatorArg.Value.IsNull)
        {
            separator = (string)separatorArg.Value.Value!;
        }

        var properties = symbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Select(p => new ExporterPropertyInfo { Name = p.Name, Type = p.Type.ToString() })
            .ToList();

        return new ExporterClassInfo
        {
            ClassName = symbol.Name,
            Namespace = symbol.ContainingNamespace.ToDisplayString(),
            Separator = separator,
            Properties = properties
        };
    }

    private static string GenerateExporterMethod(ExporterClassInfo info)
    {
        var sb = new StringBuilder();
        string header = string.Join(info.Separator, info.Properties.Select(p => p.Name));

        sb.AppendLine($"// Auto-generated exporter for {info.ClassName}");
        sb.AppendLine($"using System.Text;");
        sb.AppendLine();
        sb.AppendLine($"namespace {info.Namespace};");
        sb.AppendLine($"public static class {info.ClassName}Exporter {{");
        sb.AppendLine($"    public static string ExportToCsv(IEnumerable<{info.ClassName}> list) {{");
        sb.AppendLine($"        var sb = new StringBuilder();");
        // Header
        sb.AppendLine($"        sb.AppendLine(\"{header}\");");
        // Rows
        sb.AppendLine($"        foreach (var item in list) {{");
        sb.AppendLine($"            sb.AppendLine($\"{string.Join(info.Separator, info.Properties.Select(p => $"{{item.{p.Name}}}"))}\");");
        sb.AppendLine($"        }}");
        sb.AppendLine($"        return sb.ToString();");
        sb.AppendLine($"    }}");
        sb.AppendLine($"}}");
        return sb.ToString();
    }

    private class ExporterClassInfo
    {
        public string ClassName { get; set; } = "";
        public string Namespace { get; set; } = "";
        public List<ExporterPropertyInfo> Properties { get; set; } = new();
        public string Separator { get; set; } = "\t";
    }

    private class ExporterPropertyInfo
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
    }
}