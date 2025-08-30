using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVExporter
{
    internal static class ExporterWitReflection
    {
        public static string ExportToCsv<T>(IEnumerable<T> items, string separator = "\t")
        {
            if (items == null || !items.Any())
                return string.Empty;
            var type = typeof(T);
            var properties = type.GetProperties();
            var sb = new StringBuilder();
            // Header
            sb.AppendLine(string.Join(separator, properties.Select(p => p.Name)));
            // Rows
            foreach (var item in items)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty);
                sb.AppendLine(string.Join(separator, values));
            }
            return sb.ToString();
        }
    }
}
