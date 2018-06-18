using System;
using System.Collections.Generic;
using hhnl.GenericHandlerInvocation.Benchmark.TestData;

namespace hhnl.GenericHandlerInvocation.Benchmark.TestCandidates
{
    public class SwitchBaselineInvoker : IGenericHandlerInvoker
    {
        private static readonly Dictionary<Type, Func<object[], IServiceProvider, object>> _provider =
            new Dictionary<Type, Func<object[], IServiceProvider, object>>
            {
                {
                    typeof(string),
                    (args, provider) =>
                        ((ITestHandler<string>) provider.GetService(typeof(ITestHandler<string>))).HandleAsync(
                            (string) args[0])
                },
                {
                    typeof(long),
                    (args, provider) =>
                        ((ITestHandler<long>) provider.GetService(typeof(ITestHandler<long>))).HandleAsync(
                            (long) args[0])
                },
                {
                    typeof(int),
                    (args, provider) =>
                        ((ITestHandler<int>) provider.GetService(typeof(ITestHandler<int>))).HandleAsync(
                            (int) args[0])
                },
                {
                    typeof(double),
                    (args, provider) =>
                        ((ITestHandler<double>) provider.GetService(typeof(ITestHandler<double>))).HandleAsync(
                            (double) args[0])
                },
                {
                    typeof(float),
                    (args, provider) =>
                        ((ITestHandler<float>) provider.GetService(typeof(ITestHandler<float>))).HandleAsync(
                            (float) args[0])
                }
            };

        public TReturnType InvokeHandler<TReturnType>(IServiceProvider serviceProvider, Type genericParameterType,
            object[] args)
        {
            return (TReturnType) _provider[genericParameterType](args, serviceProvider);
        }
    }
}