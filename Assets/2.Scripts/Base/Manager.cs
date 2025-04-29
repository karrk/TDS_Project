using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;

    public static InitManager Init { get; } = new InitManager();
    public static DataManager Data { get; } = new DataManager();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoadInit()
    {
        if (_instance == null)
        {
            _instance = FindAnyObjectByType<Manager>();

            _instance = Instantiate(Resources.Load<Manager>("Manager"));

            DontDestroyOnLoad(_instance);

            _instance.StartCoroutine(_instance.ProgramInit());
        }
    }

    /// <summary>
    /// 메인 핵심 싱글톤 코어의 초기화를 진행합니다.
    /// </summary>
    private IEnumerator ProgramInit()
    {
        yield return null; // 한 프레임 스킵 (초기화 컴포넌트 등록)
        Init.Init();
    }
}

