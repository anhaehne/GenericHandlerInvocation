using System;
using System.Collections.Concurrent;
using System.Reflection;
using hhnl.GenericHandlerInvocation.Benchmark.TestCandidates;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class CachedReflectionInvoker : IGenericHandlerInvoker
    {
        private readonly Type _genericHandlerType;
        private readonly string _methodToInvoke;

        public CachedReflectionInvoker(Type genericHandlerType, string methodToInvoke)
        {
            _genericHandlerType = genericHandlerType;
            _methodToInvoke = methodToInvoke;
        }

        private readonly ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleMethod)> _handlerLookup =
            new ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleMethod)>();

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider,
            Type genericParameterType, object[] args)
        {
            var handlerLookup = _handlerLookup.GetOrAdd(genericParameterType, type =>
            {
                var handlerType = _genericHandlerType.MakeGenericType(type);
                return (handlerType, handlerType.GetMethod(_methodToInvoke));
            });

            var service = serviceProvider.GetService(handlerLookup.HandlerType);
            return (TReturnType) handlerLookup.HandleMethod.Invoke(service, args);
        }
    }
}