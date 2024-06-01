using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> _executionQueue = new Queue<Action>();

    private static UnityMainThreadDispatcher _instance = null;
    private static bool _initialized = false;

    public static void Initialize()
    {
        if (_initialized) return;

        if (_instance == null)
        {
            GameObject obj = new GameObject("UnityMainThreadDispatcher");
            _instance = obj.AddComponent<UnityMainThreadDispatcher>();
            DontDestroyOnLoad(obj);
        }

        _initialized = true;
    }

    public static UnityMainThreadDispatcher Instance()
    {
        if (_instance == null)
        {
            throw new Exception("UnityMainThreadDispatcher is not set up in this scene. Please call UnityMainThreadDispatcher.Initialize() in the main thread.");
        }
        return _instance;
    }

    void Update()
    {
        lock (_executionQueue)
        {
            while (_executionQueue.Count > 0)
            {
                _executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (_executionQueue)
        {
            _executionQueue.Enqueue(action);
        }
    }
}