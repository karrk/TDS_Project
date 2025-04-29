using UnityEngine;

/// <summary>
/// 초기화 작업의 순서가 중요한 경우 해당 클래스를 활용해 초기화 작업을 진행
/// </summary>
public abstract class InitBehaviour : MonoBehaviour, IInitable
{
    public abstract int InitOrder { get; }

    public abstract void Init();

    /// <summary>
    /// 초기화 함수 호출 시 자신을 InitManager에 등록해 초기화 순서를 결정합니다.
    /// </summary>
    protected virtual void Awake()
    {
        Manager.Init.Add(this);
    }
}
