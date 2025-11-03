using UnityEngine;

public class SingletonNonMono<T> where T : class, new()
{
    private static T _instance;
    private static readonly object _lock = new object();

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new T();
                }
            }
            return _instance;
        }
    }
}
