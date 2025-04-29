using System;
using System.Collections.Generic;

public class InitManager
{
    private Dictionary<int, List<IInitable>> _initTable = null;
    private int _maxOrder = 0;

    public InitManager()
    {
        _initTable = new Dictionary<int, List<IInitable>>();
    }

    /// <summary>
    /// 초기화 순서 관리가 필요한 오브젝트를 등록합니다.
    /// 초기화 우선순위를 확인 한 후, 가장 높은 우선순위를 저장합니다.
    /// </summary>
    public void Add(IInitable m_initComponent)
    {
        _maxOrder = (int)MathF.Max(m_initComponent.InitOrder, _maxOrder);

        if (_initTable.ContainsKey(m_initComponent.InitOrder) == false)
            _initTable.Add(m_initComponent.InitOrder, new List<IInitable>());

        _initTable[m_initComponent.InitOrder].Add(m_initComponent);
    }

    /// <summary>
    /// 저장된 초기화 컴포넌트들의 우선순위 별 초기화를 진행합니다.
    /// </summary>
    public void Init()
    {
        for (int i = _maxOrder; i >= 0; i--)
        {
            if (_initTable.ContainsKey(_maxOrder) == false)
                continue;

            foreach (var initComponent in _initTable[i])
            {
                initComponent.Init();
            }
        }
    }
}