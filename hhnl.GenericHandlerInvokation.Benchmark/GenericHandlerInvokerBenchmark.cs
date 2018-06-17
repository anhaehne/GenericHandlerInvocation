using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Engines;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;
using hhnl.GenericHandlerInvocation.Benchmark.TestData;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark
{
    [MemoryDiagnoser]
    [RankColumn]
    public class GenericHandlerInvokerBenchmark
    {
        private readonly object[] _typesToLookup = {"", 1, 1d, 1f, 1L};
        private IGenericHandlerInvoker _cachedReflect;
        private IGenericHandlerInvoker _compileExpression;
        private IGenericHandlerInvoker _naiveReflect;
        private readonly IServiceProvider _serviceProvider;

        public GenericHandlerInvokerBenchmark()
        {
            var services = new ServiceCollection();
            services.AddScoped<ITestHandler<string>, TestHandler1>();
            services.AddScoped<ITestHandler<int>, TestHandler2>();
            services.AddScoped<ITestHandler<double>, TestHandler3>();
            services.AddScoped<ITestHandler<float>, TestHandler4>();
            services.AddScoped<ITestHandler<long>, TestHandler5>();
            _serviceProvider = services.BuildServiceProvider(false);
        }

        [Params(1, 100)] public int Iterations { get; set; }

        [IterationSetup]
        public void IterationSetup()
        {
            _cachedReflect = new CachedReflectionInvoker();
            _compileExpression = new CompiledExpressionInvoker();
            _naiveReflect = new NaiveReflectionInvoker();
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

        private void TestTypes(IGenericHandlerInvoker invoker)
        {
            for (var i = 0; i < Iterations; i++)
                foreach (var type in _typesToLookup)
                    invoker.InvokeHandler<Task>(_serviceProvider, typeof(ITestHandler<>), type.GetType(), "HandleAsync",
                        new[] {type});
        }
    }
}