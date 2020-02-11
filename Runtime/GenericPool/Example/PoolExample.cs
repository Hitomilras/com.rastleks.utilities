using UnityEngine;
using System.Diagnostics;

public class PoolExample : MonoBehaviour
{

    [SerializeField]
    private int performanceTestSize = 10000;

    private GameObject[] m_TempPoolObj;

    void Start()
    {
        m_TempPoolObj = new GameObject[performanceTestSize];

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var examplePool = new PoolBase<GameObject>(performanceTestSize, () =>
        {
            return new GameObject("PoolExample object");
        }, null);

        stopwatch.Stop();
        UnityEngine.Debug.Log("Time (ms) for creating pool with " + performanceTestSize + " empty GameObjects: " + stopwatch.ElapsedMilliseconds);

        stopwatch.Reset();
        stopwatch.Start();

        for (int i = 0; i < performanceTestSize; i++)
            m_TempPoolObj[i] = examplePool.Pop();

        for (int i = 0; i < performanceTestSize; i++)
            examplePool.Push(m_TempPoolObj[i]);

        stopwatch.Stop();
        UnityEngine.Debug.Log("Time for getting and releasing " + performanceTestSize + " empty GameObjects: " + stopwatch.ElapsedMilliseconds);
    }

}
