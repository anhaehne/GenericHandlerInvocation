using System.Threading.Tasks;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;
using hhnl.GenericHandlerInvocation.Benchmark.TestData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace hhnl.GenericHandlerInvocation.Benchmark.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NaiveReflection()
        {
            TestInvoker(new NaiveReflectionInvoker());
        }

        [TestMethod]
        public void CachedReflection()
        {
            TestInvoker(new CachedReflectionInvoker());
        }

        [TestMethod]
        public void CompiledExpression()

        {
            TestInvoker(new CompiledExpressionInvoker());
        }

        public void TestInvoker(IGenericHandlerInvoker invoker)
        {
            var serviceMock = new Mock<ITestHandler<bool>>();
            serviceMock.Setup(x => x.HandleAsync(true)).Returns(Task.CompletedTask);

            var services = new ServiceCollection();
            services.AddSingleton<ITestHandler<bool>>(serviceMock.Object);
            var serviceProvider = services.BuildServiceProvider(false);

            for (int i = 0; i < 1000; i++)
            {
                invoker.InvokeHandler<Task>(serviceProvider, typeof(ITestHandler<>), typeof(bool), "HandleAsync",
                    new object[] {true});
            }
            

            serviceMock.Verify();
        }
    }
}