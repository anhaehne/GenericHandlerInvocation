# Results - SDK 7.0.102

``` ini
BenchmarkDotNet=v0.13.4, OS=Windows 11 (10.0.22621.1105)
Intel Core i5-8500 CPU 3.00GHz (Coffee Lake), 1 CPU, 6 logical and 6 physical cores
.NET SDK=7.0.102
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
```
|                            Method |         Mean |       Error |      StdDev |          Max |    Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|---------------------------------- |-------------:|------------:|------------:|-------------:|---------:|--------:|-------:|-------:|----------:|------------:|
|                    SwitchBaseline |     394.1 ns |     2.83 ns |     2.64 ns |     399.7 ns |     1.00 |    0.00 | 0.0339 |      - |     160 B |        1.00 |
| ProdReadyCachedCompiledExpression |     395.6 ns |     2.69 ns |     2.51 ns |     400.9 ns |     1.00 |    0.01 | 0.0339 |      - |     160 B |        1.00 |
|          CachedCompiledExpression |     457.0 ns |     6.74 ns |     6.31 ns |     466.4 ns |     1.16 |    0.02 | 0.0339 |      - |     160 B |        1.00 |
|                  CachedReflection |     615.4 ns |     5.91 ns |     5.24 ns |     624.0 ns |     1.56 |    0.02 | 0.1011 |      - |     480 B |        3.00 |
|                   NaiveReflection |   1,622.2 ns |    11.78 ns |    11.02 ns |   1,644.2 ns |     4.12 |    0.05 | 0.1011 |      - |     480 B |        3.00 |
|                CompiledExpression | 908,854.8 ns | 9,254.97 ns | 8,657.10 ns | 918,399.4 ns | 2,306.25 |   25.39 | 5.8594 | 4.8828 |   29358 B |      183.49 |

# Results - SDK 2.1.401

``` ini
BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i5-7440HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
Frequency=2742188 Hz, Resolution=364.6723 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT

```
|                            Method |         Mean |      Error |     StdDev |          Max |   Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|---------------------------------- |-------------:|-----------:|-----------:|-------------:|---------:|---------:|-------:|-------:|----------:|
|                    SwitchBaseline |     1.068 us |  0.0049 us |  0.0046 us |     1.075 us |     1.00 |     0.00 | 0.0496 |      - |     160 B |
| ProdReadyCachedCompiledExpression |     1.079 us |  0.0064 us |  0.0060 us |     1.086 us |     1.01 |     0.01 | 0.0496 |      - |     160 B |
|          CachedCompiledExpression |     1.256 us |  0.0068 us |  0.0064 us |     1.267 us |     1.18 |     0.01 | 0.0496 |      - |     160 B |
|                  CachedReflection |     4.324 us |  0.0254 us |  0.0237 us |     4.370 us |     4.05 |     0.03 | 0.1984 |      - |     640 B |
|                   NaiveReflection |     6.038 us |  0.0494 us |  0.0438 us |     6.103 us |     5.65 |     0.05 | 0.3510 |      - |    1120 B |
|                CompiledExpression | 1,214.553 us | 10.7950 us | 10.0977 us | 1,231.315 us | 1,137.00 |    10.27 | 9.7656 | 3.9063 |   34073 B |

# Results - SDK 2.1.300

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i5-7440HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 4 logical and 4 physical cores
Frequency=2742185 Hz, Resolution=364.6727 ns, Timer=TSC
.NET Core SDK=2.1.300
  [Host]     : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT


```
|                            Method |         Mean |     Error |    StdDev |          Max |   Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|---------------------------------- |-------------:|----------:|----------:|-------------:|---------:|---------:|-------:|-------:|----------:|
|                    SwitchBaseline |     1.096 us | 0.0099 us | 0.0093 us |     1.109 us |     1.00 |     0.00 | 0.0496 |      - |     160 B |
| ProdReadyCachedCompiledExpression |     1.133 us | 0.0168 us | 0.0157 us |     1.151 us |     1.03 |     0.02 | 0.0496 |      - |     160 B |
|          CachedCompiledExpression |     1.272 us | 0.0061 us | 0.0054 us |     1.280 us |     1.16 |     0.01 | 0.0496 |      - |     160 B |
|                  CachedReflection |     4.489 us | 0.0138 us | 0.0129 us |     4.510 us |     4.09 |     0.04 | 0.1984 |      - |     640 B |
|                   NaiveReflection |     6.627 us | 0.0864 us | 0.0808 us |     6.740 us |     6.04 |     0.09 | 0.3510 |      - |    1120 B |
|                CompiledExpression | 1,301.539 us | 7.7442 us | 7.2440 us | 1,313.684 us | 1,187.23 |    11.71 | 9.7656 | 3.9063 |   34073 B |
