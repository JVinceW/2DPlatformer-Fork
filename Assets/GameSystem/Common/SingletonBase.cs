using System;

namespace App.Scripts.Common {
    public class SingletonBase<T> : IDisposable where T : class, new() {
        private static T instance;

        protected SingletonBase() { }

        public static T Instance {
            get {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (instance == null) {
                    instance = new T();
                }

                return instance;
            }
        }

        public virtual void Dispose() { }
    }
}