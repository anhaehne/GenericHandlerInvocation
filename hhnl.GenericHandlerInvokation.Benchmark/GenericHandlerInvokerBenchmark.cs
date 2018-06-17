using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;
using hhnl.GenericHandlerInvocation.Benchmark.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark
{
    [MemoryDiagnoser]
    [Config(typeof(Config))]
    public class GenericHandlerInvokerBenchmark
    {
        private readonly IGenericHandlerInvoker _cachedCompileExpression;
        private readonly IGenericHandlerInvoker _cachedReflect;
        private readonly IGenericHandlerInvoker _compileExpression;
        private readonly IGenericHandlerInvoker _naiveReflect;

        private readonly IServiceProvider _serviceProvider;
        private readonly object[] _typesToLookup = {"", 1, 1d, 1f, 1L};

        public GenericHandlerInvokerBenchmark()
        {
            var services = new ServiceCollection();
            services.AddScoped<ITestHandler<string>, TestHandler1>();
            services.AddScoped<ITestHandler<int>, TestHandler2>();
            services.AddScoped<ITestHandler<double>, TestHandler3>();
            services.AddScoped<ITestHandler<float>, TestHandler4>();
            services.AddScoped<ITestHandler<long>, TestHandler5>();
            _serviceProvider = services.BuildServiceProvider(false);

            _cachedReflect = new CachedReflectionInvoker();
            _compileExpression = new CompiledExpressionInvoker();
            _naiveReflect = new ReflectionInvoker();
            _cachedCompileExpression = new CachedCompiledExpressionInvoker();
        }

        [Benchmark(Baseline = true)]
        public void NaiveReflection()
        {
            TestTypes(_naiveReflect);
        }

        [Benchmark]
        public void CachedReflection()
        {
            TestTypes(_cachedReflect);
        }

        [Benchmark]
        public void CompiledExpression()
        {
            TestTypes(_compileExpression);
        }

        [Benchmark]
        public void CachedCompiledExpression()
        {
            TestTypes(_cachedCompileExpression);
        }

        private void TestTypes(IGenericHandlerInvoker invoker)
        {
            foreach (var type in _typesToLookup)
                invoker.InvokeHandler<Task>(_serviceProvider, typeof(ITestHandler<>), type.GetType(), "HandleAsync",
                    new[] {type});
        }

        private class Config : ManualConfig
        {
            public Config()
            {
                Set(new FastestToSlowestOrderProvider());
            }

            private class FastestToSlowestOrderProvider : DefaultOrderProvider
            {
                public override IEnumerable<BenchmarkDotNet.Running.Benchmark> GetSummaryOrder(BenchmarkDotNet.Running.Benchmark[] benchmarks, Summary summary)
                {
                    return benchmarks.OrderBy(x => summary[x].ResultStatistics.Median);
                }
            }
        }
    }
}