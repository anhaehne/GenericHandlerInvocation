using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public interface IGenericHandlerInvoker
    {
        TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericHandlerType,
            Type genericParameterType,
            string methodToInvoke, object[] args);
    }
}
