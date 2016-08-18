using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Contextable.Attributes;

namespace Contextable
{
    public class Context<T> : IDisposable where T : class, IDisposable, ICloneable, new()
    {
        [ThreadStatic] private static ConcurrentStack<T> _scopeStack;

        //public static Guid TopUid
        //{
        //    get
        //    {
        //        T scope = ScopeStack.FirstOrDefault();

        //        return scope != null ? scope.CurrentUid : Guid.Empty;
        //    }
        //}

        [Description("Method #1")]
        public Context(T input = default(T))
        {
            ScopeStack.Push(input ?? CreateInstance<T>());
        }

        [Description("Method #2")]
        public Context(params Action<T>[] ctors)
        {
            var defaultValue = CreateInstance<T>();

            foreach (var ctor in ctors)
            {
                ctor.Invoke(defaultValue);
            }

            ScopeStack.Push(defaultValue);
        }

        private static ConcurrentStack<T> ScopeStack => _scopeStack ?? (_scopeStack = new ConcurrentStack<T>());

        public static int Count => ScopeStack.Count;

        public static T Parent
        {
            get
            {
                T output = null;

                if (ScopeStack.Any())
                {
                    output = ScopeStack.ElementAtOrDefault(Depth + 1);
                }

                return output;
            }
        }

        public static T Current
        {
            get
            {
                //if (!ScopeStack.Any())
                //{
                //    var defaultValue = CreateInstance<T>();
                //    ScopeStack.Push(defaultValue);
                //}

                T result;
                ScopeStack.TryPeek(out result);
                return result;
            }
        }

        public static int Depth
        {
            get
            {
                //var current = Current;

                var stack = ScopeStack.ToList();
                var index = stack.FindIndex(c => c == Current);

                return index;
            }
        }

        public void Dispose()
        {
            T obj;

            if (ScopeStack.TryPop(out obj))
            {
                obj?.Dispose();
            }
        }

        public static void Clear()
        {
            ScopeStack.Clear();
        }

        private static TU CreateInstance<TU>() where TU : new()
        {
            MemberInfo info = typeof (TU);

            TU defaultValue;

            if (Current != null &&
                info.GetCustomAttributes(true).Any(t => t.GetType() == typeof (SetFromParentAttribute)))
            {
                defaultValue = (TU) Current.Clone();
            }
            else
            {
                defaultValue = new TU();
            }

            return defaultValue;
        }
    }
}