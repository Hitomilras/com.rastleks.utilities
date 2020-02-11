using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class PoolBase<T>
{

    public int Length
    {
        get
        {
            return m_Pool.Count;
        }
    }

    private Func<T> m_CreateObjectFunction;

    private Action<T> m_ResetObjectFunction;

    private Queue<T> m_Pool = new Queue<T>();

    private T m_TempPoolObject;

    public PoolBase(int initSize, Func<T> createObjectDelegate, Action<T> resetObjectFunction)
    {
        m_CreateObjectFunction = createObjectDelegate;
        m_ResetObjectFunction = resetObjectFunction;

        for (int i = 0; i < initSize; i++)
            IncreasePool();
    }

    public T Pop()
    {
        if (m_Pool.Count == 0)
            IncreasePool();

        return m_Pool.Dequeue();
    }

    public void Push(T obj)
    {
        if (obj == null)
        {
            Debug.LogError("Can't push null object to Pool.");
            throw new System.ArgumentNullException();
        }

        if (m_ResetObjectFunction != null)
            m_ResetObjectFunction(obj);

        m_Pool.Enqueue(obj);
    }

    protected virtual T IncreasePool()
    {
        var result = m_CreateObjectFunction();
        m_Pool.Enqueue(result);

        return result;
    }

}
