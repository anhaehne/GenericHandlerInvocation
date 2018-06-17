using System.Threading.Tasks;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestData
{
    public interface ITestHandler<in TToHandle>
    {
        Task HandleAsync(TToHandle args);
    }

    public class TestHandler5 : ITestHandler<long>
    {
        public Task HandleAsync(long args)
        {
            return Task.CompletedTask;
        }
    }

    public class TestHandler4 : ITestHandler<float>
    {
        public Task HandleAsync(float args)
        {
            return Task.CompletedTask;
        }
    }

    public class TestHandler3 : ITestHandler<double>
    {
        public Task HandleAsync(double args)
        {
            return Task.CompletedTask;
        }
    }

    public class TestHandler2 : ITestHandler<int>
    {
        public Task HandleAsync(int args)
        {
            return Task.CompletedTask;
        }
    }

    public class TestHandler1 : ITestHandler<string>
    {
        public Task HandleAsync(string args)
        {
            return Task.CompletedTask;
        }
    }
}