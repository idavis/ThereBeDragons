using System;
using System.Reflection;
using Xunit;

namespace MethodEmitter.Tests
{
    public class DelegateTestsStaticMethodTests : StaticMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return DelegateCompiler.Compile<T>(method);
        }
    }

    public class EmitTestsStaticMethodTests : StaticMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return CilMethodGenerator.Compile<T>(method);
        }
    }

    public class ExpressionTestsStaticeMethodTests : StaticMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return MethodExpressionBuilder.Compile<T>(method);
        }
    }

    public abstract class StaticMethodTests
    {
        protected abstract T Compile<T>(MethodInfo method);

        [Fact]
        public void Can_call_a_func_with_zero_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("GetAnswerToLife");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int>>(methodInfo);
            Assert.NotNull(func);

            var result = func();
            Assert.Equal(42, result);
        }

        [Fact]
        public void Can_call_an_action_with_zero_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetAnswerToLife");
            Assert.NotNull(methodInfo);

            var func = Compile<Action>(methodInfo);
            Assert.NotNull(func);

            func();

            Assert.Equal(42, StaticMethodHolder.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_one_param()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("ReturnParamUnchanged");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int, int>>(methodInfo);
            Assert.NotNull(func);

            var result = func(10);
            Assert.Equal(10, result);
        }

        [Fact]
        public void Can_call_an_action_with_one_param()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetParamUnchanged");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<int>>(methodInfo);
            Assert.NotNull(func);

            func(10);

            Assert.Equal(10, StaticMethodHolder.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_two_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("Add");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int, int, int>>(methodInfo);
            Assert.NotNull(func);

            var result = func(10, 15);
            Assert.Equal(25, result);
        }

        [Fact]
        public void Can_call_an_action_with_two_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetAdd");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<int, int>>(methodInfo);
            Assert.NotNull(func);

            func(10, 15);

            Assert.Equal(25, StaticMethodHolder.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_thee_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("AddThree");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            var result = func(10, 15, 20);
            Assert.Equal(45, result);
        }


        [Fact]
        public void Can_call_an_action_with_three_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetAddThree");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<int, int, int>>(methodInfo);
            Assert.NotNull(func);

            func(10, 15, 20);

            Assert.Equal(45, StaticMethodHolder.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_four_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("AddFour");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            var result = func(5, 10, 15, 20);
            Assert.Equal(50, result);
        }

        [Fact]
        public void Can_call_an_action_with_four_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetAddFour");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            func(5,10, 15, 20);

            Assert.Equal(50, StaticMethodHolder.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_five_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("AddFive");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<int, int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            var result = func(5, 10, 15, 20, 25);
            Assert.Equal(75, result);
        }

        [Fact]
        public void Can_call_an_action_with_five_params()
        {
            var methodInfo = typeof(StaticMethodHolder).GetMethod("SetAddFive");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            func(5, 10, 15, 20, 25);

            Assert.Equal(75, StaticMethodHolder.CurrentAnswer);
        }
    }

    public class StaticMethodHolder
    {
        public static int CurrentAnswer { get; private set; }

        public static int GetAnswerToLife()
        {
            return 42;
        }

        public static int ReturnParamUnchanged(int x)
        {
            return x;
        }

        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int AddThree(int x, int y, int z)
        {
            return x + y + z;
        }

        public static int AddFour(int x, int y, int z, int a)
        {
            return x + y + z + a;
        }

        public static int AddFive(int x, int y, int z, int a, int b)
        {
            return x + y + z + a + b;
        }



        public static void SetAnswerToLife()
        {
            CurrentAnswer = 42;
        }

        public static void SetParamUnchanged(int x)
        {
            CurrentAnswer = x;
        }

        public static void SetAdd(int x, int y)
        {
            CurrentAnswer = x + y;
        }

        public static void SetAddThree(int x, int y, int z)
        {
            CurrentAnswer = x + y + z;
        }

        public static void SetAddFour(int x, int y, int z, int a)
        {
            CurrentAnswer = x + y + z + a;
        }

        public static void SetAddFive(int x, int y, int z, int a, int b)
        {
            CurrentAnswer = x + y + z + a + b;
        }
    }
}
