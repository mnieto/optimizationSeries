using System;
using System.Collections.Generic;
using System.Text;

namespace ExporterSourceGenerator.Shared;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class ExporterColumnAttribute : Attribute
{
    public string? Header { get; set; }
    public int Order { get; set; }
    public string? Format { get; set; }
    public ExporterColumnAttribute() { }
}
