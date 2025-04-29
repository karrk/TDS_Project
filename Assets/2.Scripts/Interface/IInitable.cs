public interface IInitable
{
    /// <summary>
    /// 초기화 우선순위
    /// 값이 높을수록 초기화 우선순위가 높게 할당됩니다.
    /// </summary>
    public int InitOrder { get; }

    /// <summary>
    /// 대상 컴포넌트의 초기화를 진행합니다.
    /// </summary>
    public void Init();
}