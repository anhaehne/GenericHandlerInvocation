using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class CompiledExpressionInvoker : IGenericHandlerInvoker
    {
        private readonly Type _genericHandlerType;
        private readonly string _methodToInvoke;

        public CompiledExpressionInvoker(Type genericHandlerType, string methodToInvoke)
        {
            _genericHandlerType = genericHandlerType;
            _methodToInvoke = methodToInvoke;
        }

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider,
            Type genericParameterType, object[] args)
        {
            var explicitHandlerType = _genericHandlerType.MakeGenericType(genericParameterType);
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
                explicitHandlerType.GetMethod(_methodToInvoke),
                argumentExpressions);

            var compiled = Expression.Lambda<Func<IServiceProvider, object[], object>>(serviceCallExpression,
                serviceProviderParameter,
                argumentsParameter).Compile();

            return (TReturnType) compiled(serviceProvider, args);
        }
    }
}