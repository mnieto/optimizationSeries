# Introduction

Addapted from [Dotnet Source Generators](https://posts.specterops.io/dotnet-source-generators-in-2024-part-1-getting-started-76d619b633f5)

To fully understand this project, you should have some notions about [Rosyln SDK](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/), 
specificly the different providers
- [Syntax tree](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/work-with-syntax)
- [Semantic model](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/work-with-semantics)
- Syntax provider
- CompilationProvider
- ParseOptionProvider
- AdditionalTextProvider
- AnalyzerConfigOptionsProvider
- MetadataReferencesProvider

Other interesting tutorial:
- https://kafkawannafly.medium.com/c-how-to-write-a-source-generator-part-2-5-setting-up-653bfea792ea

# Some examples of .NET platform source generators
- [Regex source generator](https://learn.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-source-generators)
- [Json source generator](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/source-generation)

# Pre-requisites
- .NET 8 SDK or later
- .NET Compiler platform SDK (Roslyn), from VS installer

# How to debug a source generator
- Follow the steps from [here](https://github.com/JoanComasFdz/dotnet-how-to-debug-source-generator-vs2022?tab=readme-ov-file)

