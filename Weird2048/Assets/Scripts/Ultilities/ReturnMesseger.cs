using System;

namespace Utilities
{
    public class ReturnMessenger<T> : ScriptCommond
    {
        private Func<T> func;
        public ReturnMessenger(Func<T> func, Action<Exception> onException = null)
            : base(onException)
        {
            this.func = func;
        }

        protected override object Call(object[] args)
        {
            return func();
        }
    }

    public class ReturnMessenger<T1, T> : ScriptCommond
    {
        private Func<T1, T> func;
        public ReturnMessenger(Func<T1, T> func, Action<Exception> onException = null)
            : base(onException)
        {
            this.func = func;
        }

        protected override object Call(object[] args)
        {
            return func((T1)args[0]);
        }
    }

    public class ReturnMessenger<T1, T2, T> : ScriptCommond
    {
        private Func<T1, T2, T> func;
        public ReturnMessenger(Func<T1, T2, T> func, Action<Exception> onException = null)
            : base(onException)
        {
            this.func = func;
        }

        protected override object Call(object[] args)
        {
            return func((T1)args[0], (T2)args[1]);
        }
    }

    public class ReturnMessenger<T1, T2, T3, T> : ScriptCommond
    {
        private Func<T1, T2, T3, T> func;
        public ReturnMessenger(Func<T1, T2, T3, T> func, Action<Exception> onException = null)
            : base(onException)
        {
            this.func = func;
        }

        protected override object Call(object[] args)
        {
            return func((T1)args[0], (T2)args[1], (T3)args[2]);
        }
    }

    public class ReturnMessenger<T1, T2, T3, T4, T> : ScriptCommond
    {
        private Func<T1, T2, T3, T4, T> func;
        public ReturnMessenger(Func<T1, T2, T3, T4, T> func, Action<Exception> onException = null)
            : base(onException)
        {
            this.func = func;
        }

        protected override object Call(object[] args)
        {
            return func((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
        }
    }
}

