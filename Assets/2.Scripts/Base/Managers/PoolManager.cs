using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PoolManager
{
    private readonly Dictionary<Type, E_Pool> _typeToPool = new()
    {
        { typeof(E_MonsterType), E_Pool.Monster },
        { typeof(E_Bullet), E_Pool.Bullet }
    };

    [SerializeField] private MonsterPrefab[] _monsterPrefabs;
    [SerializeField] private BulletPrefab[] _bulletPrefabs;

    private Dictionary<E_MonsterType, ObjPool> _monsterPool;
    private Dictionary<E_Bullet, ObjPool> _bulletPool;

    // 오브젝트 형태만 만들고,
    // 데이터 매니저에서 SO 를 불러온다.

    public Transform MainDir { get; private set; }

    public void Init()
    {
        CreateMainDir();
        CreatePools();
    }

    /// <summary>
    /// 풀 관리 전용 디렉토리를 형성합니다.
    /// </summary>
    private void CreateMainDir()
    {
        Transform newDir = new GameObject("Pool").transform;
        newDir.SetParent(Manager.Dir);
        MainDir = newDir;
    }

    /// <summary>
    /// 초기화 시작 시 각 오브젝트 풀을 생성합니다.
    /// </summary>
    private void CreatePools()
    {
        _monsterPool = new Dictionary<E_MonsterType, ObjPool>();
        _bulletPool = new Dictionary<E_Bullet, ObjPool>();

        CreateMonsterPools();
        CreateBulletPools();
    }

    /// <summary>
    /// 몬스터 풀 생성 : 등록된 프리팹 수만큼 초기화를 진행합니다.
    /// </summary>
    private void CreateMonsterPools()
    {
        for (int i = 0; i < _monsterPrefabs.Length; i++)
        {
            Transform newDir = new GameObject($"{_monsterPrefabs[i].Type} Pool").transform;
            newDir.SetParent(MainDir);

            ObjPool pool = new ObjPool(_monsterPrefabs[i].Prefab, newDir);
            _monsterPool.Add(_monsterPrefabs[i].Type, pool);
        }
    }

    /// <summary>
    /// 총알 풀 생성 : 등록된 프리팹 수만큼 초기화를 진행합니다.
    /// </summary>
    private void CreateBulletPools()
    {
        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            Transform newDir = new GameObject($"{_bulletPrefabs[i].Type} Pool").transform;
            newDir.SetParent(MainDir);

            ObjPool pool = new ObjPool(_bulletPrefabs[i].Prefab, newDir);
            _bulletPool.Add(_bulletPrefabs[i].Type, pool);
        }
    }

    /// <summary>
    /// 필요한 오브젝트를 반환 받습니다.
    /// </summary>
    /// <param name="m_type">구체적인 오브젝트 타입을 명시
    /// ex) E_Monster.Melee , E_Bullet.Basic </param>
    /// <returns></returns>
    public GameObject Get(Enum m_type)
    {
        if (_typeToPool.TryGetValue(m_type.GetType(), out E_Pool poolType))
        {
            return FindPool(m_type, poolType).GetObj();
        }

        throw new Exception("지정된 Pool 타입이 아닙니다.");
    }

    /// <summary>
    /// 필요한 오브젝트의 컴포넌트를 바로 접근합니다.
    /// </summary>
    public T Get<T>(Enum m_type)
    {
        return Get(m_type).GetComponent<T>();
    }

    /// <summary>
    /// 대상 오브젝트 풀을 확인합니다.
    /// </summary>
    /// <param name="m_type">구체적인 Pool Type</param>
    /// <param name="m_poolType">메인 Pool</param>
    private ObjPool FindPool(Enum m_type, E_Pool m_poolType)
    {
        switch (m_poolType)
        {
            case E_Pool.Monster:
                if (_monsterPool.TryGetValue((E_MonsterType)m_type, out ObjPool monsterPool))
                { return monsterPool; }
                break;

            case E_Pool.Bullet:
                if (_bulletPool.TryGetValue((E_Bullet)m_type, out ObjPool bulletPool))
                { return bulletPool; }
                break;
        }

        throw new Exception("정의된 Pool이 없습니다.");
    }

    /// <summary>
    /// 오브젝트를 반환합니다. IPooling 인터페이스를 상속받은 오브젝트만 대상으로 함
    /// </summary>
    public void Return(IPooling m_obj)
    {
        if (_typeToPool.TryGetValue(m_obj.Type.GetType(), out E_Pool poolType))
        {
            FindPool(m_obj.Type, poolType).Return(m_obj);
            return;
        }

        throw new Exception("반환할 수 없는 타입입니다.");
    }
}