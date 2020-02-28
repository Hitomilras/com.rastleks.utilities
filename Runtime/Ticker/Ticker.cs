using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{

    public event System.Action OnUpdate;

    public event System.Action OnLateUpdate;

    public event System.Action OnFixedUpdate;

    public static Ticker Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public  static void OnGameLoad()
    {
        GameObject tickerObject = new GameObject("Ticker");
        DontDestroyOnLoad(tickerObject);

        Instance = tickerObject.AddComponent<Ticker>();
    }

    public void Update()
    {
        OnUpdate?.Invoke();
    }

    public void LateUpdate()
    {
        OnLateUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
