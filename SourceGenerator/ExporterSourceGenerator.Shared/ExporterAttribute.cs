using System;
using System.Collections.Generic;
using System.Text;

namespace ExporterSourceGenerator.Shared;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ExporterAttribute : Attribute
{
    public string Separator { get; set; } = "\t";
}
