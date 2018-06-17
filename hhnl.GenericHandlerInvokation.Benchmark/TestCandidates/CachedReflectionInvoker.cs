using System;
using System.Collections.Concurrent;
using System.Reflection;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class CachedReflectionInvoker : IGenericHandlerInvoker
    {
        private readonly ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleMethod)> _handlerLookup =
            new ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleMethod)>();

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericHandlerType,
            Type genericParameterType, string methodToInvoke, object[] args)
        {
            var handlerLookup = _handlerLookup.GetOrAdd(genericParameterType, type =>
            {
                var handlerType = genericHandlerType.MakeGenericType(type);
                return (handlerType, handlerType.GetMethod(methodToInvoke));
            });

            var service = serviceProvider.GetService(handlerLookup.HandlerType);
            return (TReturnType) handlerLookup.HandleMethod.Invoke(service, args);
        }
    }
}