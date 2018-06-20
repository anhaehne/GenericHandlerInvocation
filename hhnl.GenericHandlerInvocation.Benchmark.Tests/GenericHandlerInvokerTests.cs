using System.Threading.Tasks;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;
using hhnl.GenericHandlerInvocation.Benchmark.TestData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace hhnl.GenericHandlerInvocation.Benchmark.Tests
{
    [TestClass]
    public class GenericHandlerInvokerTests
    {
        [TestMethod]
        public void Reflection()
        {
            TestInvoker(new ReflectionInvoker(typeof(ITestHandler<>), "HandleAsync"));
        }

        [TestMethod]
        public void CachedReflection()
        {
            TestInvoker(new CachedReflectionInvoker(typeof(ITestHandler<>), "HandleAsync"));
        }

        [TestMethod]
        public void CompiledExpression()
        {
            TestInvoker(new CompiledExpressionInvoker(typeof(ITestHandler<>), "HandleAsync"));
        }

        [TestMethod]
        public void CachedCompiledExpression()
        {
            TestInvoker(new CachedCompiledExpressionInvoker(typeof(ITestHandler<>), "HandleAsync"));
        }

        [TestMethod]
        public void ProdReadyCachedCompiledExpression()
        {
            TestInvoker(new ProductionReadyCachedCompiledExpressionInvoker(typeof(ITestHandler<>), "HandleAsync"));
        }

        public void TestInvoker(IGenericHandlerInvoker invoker)
        {
            var serviceMock = new Mock<ITestHandler<bool>>();
            serviceMock.Setup(x => x.HandleAsync(true)).Returns(Task.CompletedTask);

            var services = new ServiceCollection();
            services.AddSingleton(serviceMock.Object);
            var serviceProvider = services.BuildServiceProvider(false);

            for (var i = 0; i < 1000; i++)
                invoker.InvokeHandler<Task>(serviceProvider, typeof(bool),
                    new object[] {true});


            serviceMock.Verify();
        }
    }
}