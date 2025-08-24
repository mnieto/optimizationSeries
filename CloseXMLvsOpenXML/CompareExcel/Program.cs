using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CompareExcel
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args);
        }
    }
}
