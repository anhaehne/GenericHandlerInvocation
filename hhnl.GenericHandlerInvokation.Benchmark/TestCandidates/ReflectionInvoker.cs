using System;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class ReflectionInvoker : IGenericHandlerInvoker
    {
        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericHandlerType,
            Type genericParameterType,
            string methodToInvoke, object[] args)
        {
            var genericInterface = genericHandlerType.MakeGenericType(genericParameterType);
            var service = serviceProvider.GetService(genericInterface);
            return (TReturnType) genericInterface.GetMethod(methodToInvoke).Invoke(service, args);
        }
    }
}