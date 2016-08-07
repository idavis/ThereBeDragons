using System;
using System.Reflection;
using Xunit;

namespace MethodEmitter.Tests
{
    public class DelegateTestsInstanceMethodTests : InstanceMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return DelegateCompiler.Compile<T>(method);
        }
    }

    public class EmitTestsInstanceMethodTests : InstanceMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return CilMethodGenerator.Compile<T>(method);
        }
    }
    /*
    public class ExpressionTestsInstanceMethodTests : InstanceMethodTests
    {
        protected override T Compile<T>(MethodInfo method)
        {
            return MethodExpressionBuilder.Compile<T>(method);
        }
    }*/

    public abstract class InstanceMethodTests
    {
        protected abstract T Compile<T>(MethodInfo method);

        [Fact]
        public void Can_call_a_func_with_zero_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("GetAnswerToLife");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder,int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance);
            Assert.Equal(42, result);
        }

        [Fact]
        public void Can_call_an_action_with_zero_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetAnswerToLife");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance);

            Assert.Equal(42, instance.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_one_param()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("ReturnParamUnchanged");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance, 10);

            Assert.Equal(10, result);
        }

        [Fact]
        public void Can_call_an_action_with_one_param()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetParamUnchanged");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance,10);

            Assert.Equal(10, instance.CurrentAnswer);
        }


        [Fact]
        public void Can_call_a_func_with_two_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("Add");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance, 10, 15);

            Assert.Equal(25, result);
        }

        [Fact]
        public void Can_call_an_action_with_two_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetAdd");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance, 10, 15);

            Assert.Equal(25, instance.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_thee_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("AddThree");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance, 10, 15, 20);

            Assert.Equal(45, result);
        }


        [Fact]
        public void Can_call_an_action_with_three_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetAddThree");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance, 10, 15, 20);

            Assert.Equal(45, instance.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_four_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("AddFour");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder, int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance, 5, 10, 15, 20);

            Assert.Equal(50, result);
        }

        [Fact]
        public void Can_call_an_action_with_four_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetAddFour");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance, 5, 10, 15, 20);

            Assert.Equal(50, instance.CurrentAnswer);
        }

        [Fact]
        public void Can_call_a_func_with_five_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("AddFive");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolder, int, int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance, 5, 10, 15, 20, 25);

            Assert.Equal(75, result);
        }

        [Fact]
        public void Can_call_an_action_with_five_params()
        {
            var methodInfo = typeof(InstanceMethodHolder).GetMethod("SetAddFive");
            Assert.NotNull(methodInfo);

            var func = Compile<Action<InstanceMethodHolder, int, int, int, int, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            func(instance, 5, 10, 15, 20, 25);

            Assert.Equal(75, instance.CurrentAnswer);
        }
        /*
        [Fact]
        public void Calling_a_func_whose_definition_is_based_on_a_base_class_will_call_the_base_method_rather_than_the_overriden()
        {
            var methodInfo = typeof(InstanceMethodHolderBase).GetMethod("GetRealAnswerToLife");
            Assert.NotNull(methodInfo);

            var func = Compile<Func<InstanceMethodHolderBase, int>>(methodInfo);
            Assert.NotNull(func);

            InstanceMethodHolder instance = new InstanceMethodHolder();
            var result = func(instance);

            Assert.Equal(42, result);
        }*/
    }


    public abstract class InstanceMethodHolderBase
    {
        public virtual int GetRealAnswerToLife()
        {
            return 7;
        }
    }

    public class InstanceMethodHolder : InstanceMethodHolderBase
    {
        public int CurrentAnswer { get; private set; }

        public override int GetRealAnswerToLife()
        {
            return base.GetRealAnswerToLife() + 35;
        }

        public int GetAnswerToLife()
        {
            return 42;
        }

        public int ReturnParamUnchanged(int x)
        {
            return x;
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int AddThree(int x, int y, int z)
        {
            return x + y + z;
        }

        public int AddFour(int x, int y, int z, int a)
        {
            return x + y + z + a;
        }

        public int AddFive(int x, int y, int z, int a, int b)
        {
            return x + y + z + a + b;
        }
  


        public void SetAnswerToLife()
        {
            CurrentAnswer = 42;
        }

        public void SetParamUnchanged(int x)
        {
            CurrentAnswer = x;
        }

        public void SetAdd(int x, int y)
        {
            CurrentAnswer = x + y;
        }

        public void SetAddThree(int x, int y, int z)
        {
            CurrentAnswer = x + y + z;
        }

        public void SetAddFour(int x, int y, int z, int a)
        {
            CurrentAnswer = x + y + z + a;
        }

        public void SetAddFive(int x, int y, int z, int a, int b)
        {
            CurrentAnswer = x + y + z + a + b;
        }
    }
}
