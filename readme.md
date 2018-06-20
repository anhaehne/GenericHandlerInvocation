# Results

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17692
Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.300
  [Host]     : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT


```
|                   Method |         Mean |      Error |     StdDev |          Max |   Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|------------------------- |-------------:|-----------:|-----------:|-------------:|---------:|---------:|-------:|-------:|----------:|
|           SwitchBaseline |     1.243 us |  0.0096 us |  0.0085 us |     1.261 us |     1.00 |     0.00 | 0.0362 |      - |     160 B |
| CachedCompiledExpression |     1.467 us |  0.0293 us |  0.0572 us |     1.621 us |     1.18 |     0.05 | 0.0362 |      - |     160 B |
|         CachedReflection |     7.434 us |  0.1067 us |  0.0946 us |     7.619 us |     5.98 |     0.08 | 0.1450 |      - |     640 B |
|          NaiveReflection |    11.806 us |  0.1238 us |  0.1097 us |    11.986 us |     9.50 |     0.11 | 0.2594 |      - |    1121 B |
|       CompiledExpression | 1,719.490 us | 22.2936 us | 17.4054 us | 1,747.325 us | 1,383.94 |    16.19 | 7.8125 | 3.9063 |   34067 B |
