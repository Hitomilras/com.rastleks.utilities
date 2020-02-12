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
            return pool.Count;
        }
    }

    private Func<T> createObjectFunction;

    private Action<T> resetObjectAction;

    private Action<T> popObjectAction;

    private Queue<T> pool = new Queue<T>();

    public PoolBase(int initSize, Func<T> createObjectDelegate, Action<T> resetObjectFunction, Action<T> popObjectAction = null)
    {
        this.createObjectFunction = createObjectDelegate;
        this.resetObjectAction = resetObjectFunction;
        this.popObjectAction = popObjectAction;

        for (int i = 0; i < initSize; i++)
            IncreasePool();
    }

    public T Pop()
    {
        if (pool.Count == 0)
            IncreasePool();

        T result = pool.Dequeue();

        popObjectAction?.Invoke(result);

        return result;
    }

    public void Push(T obj)
    {
        if (obj == null)
        {
            Debug.LogError("Can't push null object to Pool.");
            throw new System.ArgumentNullException();
        }

        resetObjectAction?.Invoke(obj);

        pool.Enqueue(obj);
    }

    protected virtual T IncreasePool()
    {
        var result = createObjectFunction();
        pool.Enqueue(result);

        return result;
    }

}
