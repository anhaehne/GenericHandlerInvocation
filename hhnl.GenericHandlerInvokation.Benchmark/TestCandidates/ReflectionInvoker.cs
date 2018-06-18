using System;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class ReflectionInvoker : IGenericHandlerInvoker
    {
        private readonly Type _genericHandlerType;
        private readonly string _methodToInvoke;

        public ReflectionInvoker(Type genericHandlerType, string methodToInvoke)
        {
            _genericHandlerType = genericHandlerType;
            _methodToInvoke = methodToInvoke;
        }

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider,
            Type genericParameterType, object[] args)
        {
            var genericInterface = _genericHandlerType.MakeGenericType(genericParameterType);
            var service = serviceProvider.GetService(genericInterface);
            return (TReturnType) genericInterface.GetMethod(_methodToInvoke).Invoke(service, args);
        }
    }
}