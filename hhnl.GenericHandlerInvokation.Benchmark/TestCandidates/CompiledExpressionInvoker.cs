using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class CompiledExpressionInvoker : IGenericHandlerInvoker
    {
        private readonly ConcurrentDictionary<Type, Func<IServiceProvider, object[], object>> _handlerLookup =
            new ConcurrentDictionary<Type, Func<IServiceProvider, object[], object>>();

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericHandlerType,
            Type genericParameterType, string methodToInvoke, object[] args)
        {
            if (!_handlerLookup.ContainsKey(genericParameterType))
            {
                var explicitHandlerType = genericHandlerType.MakeGenericType(genericParameterType);
                var serviceProviderParameter = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
                var argumentsParameter = Expression.Parameter(typeof(object[]), "args");

                var getHandlerCall = Expression.Call(null,
                    typeof(ServiceProviderServiceExtensions).GetMethod("GetService")
                        .MakeGenericMethod(explicitHandlerType),
                    serviceProviderParameter);

                var argumentExpressions = new Expression[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    argumentExpressions[i] =
                        Expression.Convert(Expression.ArrayAccess(argumentsParameter, Expression.Constant(i)),
                            args[i].GetType());
                }
                
                Expression serviceCallExpression = Expression.Call(getHandlerCall,
                    explicitHandlerType.GetMethod(methodToInvoke),
                    argumentExpressions);

                var compiled = Expression.Lambda<Func<IServiceProvider, object[], object>>(serviceCallExpression,
                    serviceProviderParameter,
                    argumentsParameter).Compile();

                _handlerLookup.TryAdd(genericParameterType, compiled);
            }

            return (TReturnType) _handlerLookup[genericParameterType](serviceProvider, args);
        }
    }
}