using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    /// <summary>
    /// Helper class to invoke concrete implementations of an generic handler interface using a <see cref="Type" />.
    /// </summary>
    /// <example>
    /// var invoker = new GenericHandlerInvoker(typeof(IHandler&lt;&gt;), nameof(IHandler&lt;object&gt;.Invoke));
    /// invoker.InvokeHandler(serviceProvider, typeof(string), new object[]{"test"});
    /// </example>
    public sealed class ProductionReadyCachedCompiledExpressionInvoker : IGenericHandlerInvoker
    {
        private readonly Type _genericHandlerType;

        private readonly ConcurrentDictionary<Type, Func<IServiceProvider, object[], object>> _handlerLookup =
            new ConcurrentDictionary<Type, Func<IServiceProvider, object[], object>>();

        private readonly string _methodToInvoke;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionReadyCachedCompiledExpressionInvoker" /> class.
        /// </summary>
        /// <param name="genericHandlerType">Type of the generic handler.</param>
        /// <param name="methodToInvoke">The method to invoke.</param>
        public ProductionReadyCachedCompiledExpressionInvoker(Type genericHandlerType, string methodToInvoke)
        {
            _genericHandlerType = genericHandlerType;
            _methodToInvoke = methodToInvoke;
        }

        /// <summary>
        /// Invokes the handler.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="genericParameterType">Type of the generic parameter.</param>
        /// <param name="args">The arguments.</param>
        public void InvokeHandler(
            IServiceProvider serviceProvider,
            Type genericParameterType,
            object[] args)
        {
            InvokeHandler<object>(serviceProvider, genericParameterType, args);
        }

        /// <summary>
        /// Invokes the handler.
        /// </summary>
        /// <typeparam name="TReturnType">The type of the return type.</typeparam>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="genericParameterType">Type of the generic parameter.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The <typeparamref name="TReturnType" />.</returns>
        public TReturnType InvokeHandler<TReturnType>(
            IServiceProvider serviceProvider,
            Type genericParameterType,
            object[] args)
        {
            if (!_handlerLookup.TryGetValue(genericParameterType, out var handler))
                handler = TryAddCompiledExpression(serviceProvider, genericParameterType, args);

            return (TReturnType)handler(serviceProvider, args);
        }

        private Func<IServiceProvider, object[], object> TryAddCompiledExpression(IServiceProvider serviceProvider, Type genericParameterType, object[] args)
        {
            var explicitHandlerType = _genericHandlerType.MakeGenericType(genericParameterType);

            if (serviceProvider.GetService(explicitHandlerType) == null)
            {
                throw new InvalidOperationException(
                    $"No handler of type '{explicitHandlerType}' found.");
            }

            var types = args.Select(x => x.GetType()).ToArray();
            var explicitHandlerTypeMethod = explicitHandlerType.GetMethod(_methodToInvoke, types);

            if (explicitHandlerTypeMethod == null)
            {
                throw new InvalidOperationException(
                    $"Method '{_methodToInvoke}' with parameters '{string.Join(", ", types.Select(x => x.ToString()))}' not found.");
            }

            var serviceProviderParameter = Expression.Parameter(typeof(IServiceProvider));
            var argumentsParameter = Expression.Parameter(typeof(object[]));

            var getHandlerCall = Expression.Call(
                null,
                typeof(ServiceProviderServiceExtensions)
                    .GetMethod(nameof(IServiceProvider.GetService))
                    .MakeGenericMethod(explicitHandlerType),
                serviceProviderParameter);

            var argumentExpressions = new Expression[args.Length];
            for (var i = 0; i < args.Length; i++)
            {
                argumentExpressions[i] =
                    Expression.Convert(
                        Expression.ArrayAccess(argumentsParameter, Expression.Constant(i)),
                        args[i].GetType());
            }

            Expression serviceCallExpression = Expression.Call(
                getHandlerCall,
                explicitHandlerTypeMethod,
                argumentExpressions);

            var compiled = Expression.Lambda<Func<IServiceProvider, object[], object>>(
                    serviceCallExpression,
                    serviceProviderParameter,
                    argumentsParameter)
                .Compile();

            _handlerLookup.TryAdd(genericParameterType, compiled);

            return compiled;
        }
    }
}