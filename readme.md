# Results

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
