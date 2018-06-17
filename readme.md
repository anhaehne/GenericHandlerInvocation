# Results

Method | Mean | Error | StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated
--------------------------|--------------|------------|------------|--------|----------|--------|--------|-----------
CachedCompiledExpression |     1.435 us |  0.0285 us |  0.0667 us |   0.13 |     0.01 | 0.0362 |      0  |     160 B 
CachedReflection |     4.595 us |  0.1178 us |  0.1157 us |   0.41 |     0.01 | 0.1831 |      0  |     800 B 
NaiveReflection |    11.272 us |  0.2570 us |  0.2856 us |   1.00 |     0.00 | 0.2594 |      0 |    1121 B 
CompiledExpression | 1,645.038 us | 13.1493 us | 10.9803 us | 146.02 |     3.57 | 7.8125 | 3.9063 |   33826 B 
