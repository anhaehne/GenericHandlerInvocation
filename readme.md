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
