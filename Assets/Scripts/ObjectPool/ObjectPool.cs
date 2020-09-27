using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool
{
    public class ObjectPool<T> : IPool<T>
    {
        private Stack<T> _cacheStack = new Stack<T>();

        private int _initCount;
        private IFactory<T> _factory;
        private Action<T> _resetMethod;

        public int Count
        {
            get { return _cacheStack.Count; }
        }

        public ObjectPool(IFactory<T> factory, Action<T> resetMethod, int initCount = 0)
        {
            _factory = factory;
            _initCount = initCount;
            _resetMethod = resetMethod;

            for (int i = 0; i < initCount; i++)
            {
                _cacheStack.Push(_factory.Create());
            }
        }


        public void Recycle(T obj)
        {
            _resetMethod?.Invoke(obj);
            _cacheStack.Push(obj);
        }

        public T Allocate()
        {
            return Count > 0 ? _cacheStack.Pop() : _factory.Create();
        }
    }
}
