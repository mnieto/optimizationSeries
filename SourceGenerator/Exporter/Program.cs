using BenchmarkDotNet.Running;

namespace CSVExporter;

internal class Program
{
    static void Main(string[] args)
    {

        var summary = BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args);

    }
}
