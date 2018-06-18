# Results

``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17692
Intel Xeon CPU E3-1230 V2 3.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.300
  [Host]     : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.7 (CoreCLR 4.6.26328.01, CoreFX 4.6.26403.03), 64bit RyuJIT


```
|                   Method |         Mean |      Error |     StdDev |          Max | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|------------------------- |-------------:|-----------:|-----------:|-------------:|-------:|---------:|-------:|-------:|----------:|
| CachedCompiledExpression |     1.444 us |  0.0282 us |  0.0325 us |     1.500 us |   0.12 |     0.00 | 0.0362 |      - |     160 B |
|         CachedReflection |     7.231 us |  0.1311 us |  0.1162 us |     7.505 us |   0.63 |     0.01 | 0.1373 |      - |     640 B |
|          NaiveReflection |    11.561 us |  0.2191 us |  0.2049 us |    12.011 us |   1.00 |     0.00 | 0.2594 |      - |    1121 B |
|       CompiledExpression | 1,727.338 us | 16.9056 us | 14.1169 us | 1,754.927 us | 149.45 |     2.80 | 7.8125 | 3.9063 |   33826 B |
