//Implemention of a generic object pool

public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> _objects;
        private readonly Func<T> _objectGenerator;
        private readonly int _maxSize;

        public ObjectPool(Func<T> objectGenerator, int maxSize)
        {
            _objects = new ConcurrentBag<T>();
            _objectGenerator = objectGenerator;
            _maxSize = maxSize;
        }

        public T Get()
        {
            if (_objects.TryTake(out T item))
                return item;

            return _objectGenerator();
        }

        public void Return(T item)
        {
            if (_objects.Count < _maxSize)
                _objects.Add(item);
        }
    }
