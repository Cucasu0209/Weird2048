using System;

namespace Utilities
{
    public class ScriptEvent : ScriptCommond
    {
        private Action act;
        public ScriptEvent(Action act, Action<Exception> onException = null)
            : base(onException)
        {
            this.act = act;
        }

        protected override void Publish(object[] args)
        {
            act();
        }
    }

    public class ScriptEvent<T1> : ScriptCommond
    {
        private Action<T1> act;
        public ScriptEvent(Action<T1> act, Action<Exception> onException = null)
            : base(onException)
        {
            this.act = act;
        }

        protected override void Publish(object[] args)
        {
            act((T1)args[0]);
        }
    }

    public class ScriptEvent<T1, T2> : ScriptCommond
    {
        private Action<T1, T2> act;
        public ScriptEvent(Action<T1, T2> act, Action<Exception> onException = null)
            : base(onException)
        {
            this.act = act;
        }

        protected override void Publish(object[] args)
        {
            act((T1)args[0], (T2)args[1]);
        }
    }

    public class ScriptEvent<T1, T2, T3> : ScriptCommond
    {
        private Action<T1, T2, T3> act;
        public ScriptEvent(Action<T1, T2, T3> act, Action<Exception> onException = null)
            : base(onException)
        {
            this.act = act;
        }

        protected override void Publish(object[] args)
        {
            act((T1)args[0], (T2)args[1], (T3)args[2]);
        }
    }

    public class ScriptEvent<T1, T2, T3, T4> : ScriptCommond
    {
        private Action<T1, T2, T3, T4> act;
        public ScriptEvent(Action<T1, T2, T3, T4> act, Action<Exception> onException = null)
            : base(onException)
        {
            this.act = act;
        }

        protected override void Publish(object[] args)
        {
            act((T1)args[0], (T2)args[1], (T3)args[2], (T4)args[3]);
        }
    }
}

