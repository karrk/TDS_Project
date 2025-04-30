using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Transform Dir => _instance.transform;

    public static InitManager Init { get; } = new InitManager();
    public static DataManager Data { get; private set; }
    [SerializeField] private DataManager _data;

    public static PoolManager Pool { get; private set; }
    [SerializeField] private PoolManager _pool;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoadInit()
    {
        if (_instance == null)
        {
            _instance = FindAnyObjectByType<Manager>();

            _instance = Instantiate(Resources.Load<Manager>("Manager"));

            DontDestroyOnLoad(_instance);

            _instance.StartCoroutine(_instance.StepInit());
        }
    }

    private void ManagerInit()
    {
        Pool = _pool;
        _pool.Init();

        Data = _data;
    }

    /// <summary>
    /// 메인 핵심 싱글톤 코어의 초기화를 진행합니다.
    /// </summary>
    private IEnumerator StepInit()
    {
        yield return null; // 한 프레임 스킵 (초기화 컴포넌트 등록)
        ManagerInit();
        Init.Init();
    }
}

