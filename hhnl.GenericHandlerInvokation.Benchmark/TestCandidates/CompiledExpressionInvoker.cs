using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class CompiledExpressionInvoker : IGenericHandlerInvoker
    {
        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericHandlerType,
            Type genericParameterType, string methodToInvoke, object[] args)
        {
            var explicitHandlerType = genericHandlerType.MakeGenericType(genericParameterType);
            var serviceProviderParameter = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
            var argumentsParameter = Expression.Parameter(typeof(object[]), "args");

            var getHandlerCall = Expression.Call(null,
                typeof(ServiceProviderServiceExtensions).GetMethod("GetService")
                    .MakeGenericMethod(explicitHandlerType),
                serviceProviderParameter);

            var argumentExpressions = new Expression[args.Length];
            for (var i = 0; i < args.Length; i++)
                argumentExpressions[i] =
                    Expression.Convert(Expression.ArrayAccess(argumentsParameter, Expression.Constant(i)),
                        args[i].GetType());

            Expression serviceCallExpression = Expression.Call(getHandlerCall,
                explicitHandlerType.GetMethod(methodToInvoke),
                argumentExpressions);

            var compiled = Expression.Lambda<Func<IServiceProvider, object[], object>>(serviceCallExpression,
                serviceProviderParameter,
                argumentsParameter).Compile();

            return (TReturnType) compiled(serviceProvider, args);
        }
    }
}