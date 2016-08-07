using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using System;
using System.Reflection;
using Xunit;

namespace MethodEmitter.Tests
{
    public class zConfig : ManualConfig
    {
        public zConfig()
        {
            Add(Job.Default.WithLaunchCount(1));
            Add(PropertyColumn.Method);
            Add(StatisticColumn.Median, StatisticColumn.StdDev);
            Add(BaselineDiffColumn.Scaled);
            Add(new MemoryDiagnoser());
            Add(JitOptimizationsValidator.FailOnError);
            Add(CsvExporter.Default, MarkdownExporter.Default, MarkdownExporter.GitHub);
            Add(new ConsoleLogger());

        }
    }

    public class Benchmarks5Params
    {
        const string MethodName = "AddFive";
        const BindingFlags Flags = BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod;
        readonly Type StaticMethodHolderType = typeof(StaticMethodHolder);
        Func<int, int, int, int, int, int> _TargetF5;
        MethodInfo methodInfoF5;
        Delegate delegateF5;
        dynamic dynDelegateF5;
        readonly object[] paramSet = new object[] { 5, 10, 15, 20, 25 };
        private Func<int, int, int, int, int, int> _MEBF5;
        object captureData = new object();

        [Setup]
        public void SetupData()
        {
            methodInfoF5 = StaticMethodHolderType.GetMethod(MethodName);

            _MEBF5 = MethodExpressionBuilder.Compile<Func<int, int, int, int, int, int>>(methodInfoF5);

            _TargetF5 = CilMethodGenerator.Compile<Func<int, int, int, int, int, int>>(methodInfoF5);

            delegateF5 = DelegateCompiler.Compile<Func<int, int, int, int, int, int>>(methodInfoF5);

            dynDelegateF5 = delegateF5;
        }

        [Benchmark(Description = "Directly Invoke Func")]
        public void DirectF5()
        {
            StaticMethodHolder.AddFive(5, 10, 15, 20, 25);
        }

        [Benchmark(Description = "Invoke IL Emit Func", Baseline = true)]
        public void TargetF5()
        {
            _TargetF5(5, 10, 15, 20, 25);
        }

        [Benchmark(Description = "Invoke Compiled Expression Func")]
        public void MEBF5()
        {
            _MEBF5(5, 10, 15, 20, 25);
        }

        [Benchmark(Description = "Invoke using DLR Delegate Func")]
        public void InvokeF5Dynamic()
        {
            dynDelegateF5(5, 10, 15, 20, 25);
        }

        [Benchmark(Description = "MethodInfo.Invoke Func")]
        public void ReflectF5()
        {
            methodInfoF5.Invoke(null, paramSet);
        }

        [Benchmark(Description = "DynamicInvoke Delegate Func")]
        public void DynamicInvokeF5()
        {
            delegateF5.DynamicInvoke(5, 10, 15, 20, 25);
        }

        [Benchmark(Description = "InvokeMember Func")]
        public void InvokeMemberF5()
        {
            StaticMethodHolderType.InvokeMember(MethodName, Flags, null, null, paramSet);
        }
    }

    public class BenchmarkHarness
    {
        //[Fact]
        public void DoIt()
        {
            var summary = BenchmarkRunner.Run<Benchmarks5Params>(new zConfig());
            /*var x = new Benchmarks5Params();
            x.SetupData();
            x.InvokeMemberF5();
            x.InvokeF5Dynamic();
            x.DirectF5();
            x.TargetF5();
            x.MEBF5();*/
        }
    }
}
