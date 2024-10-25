public class InterviewSolutions
{
    // Implemention of a thread-safe singleton with double-check locking
    public sealed class ThreadSafeSingleton
    {
        private static volatile ThreadSafeSingleton _instance;
        private static readonly object _lock = new object();

        private ThreadSafeSingleton() { }

        public static ThreadSafeSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ThreadSafeSingleton();
                        }
                    }
                }
                return _instance;
            }
        }
    }