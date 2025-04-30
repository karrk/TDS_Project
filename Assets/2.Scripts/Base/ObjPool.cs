using System.Collections.Generic;
using UnityEngine;

public class ObjPool
{
    private GameObject _prefab;
    private List<GameObject> _pool;
    private int _initCount = 5;
    private Transform _dir;

    public ObjPool(GameObject m_prefab, Transform m_dir)
    {
        this._dir = m_dir;
        this._prefab = m_prefab;
        Init();
    }

    private void Init()
    {
        _pool = new List<GameObject>(_initCount);
        CreateObj(_pool.Capacity);
    }

    /// <summary>
    /// 내부 관리될 풀 안에 비활성화 오브젝트를 저장합니다.
    /// </summary>
    /// <param name="m_count">생성할 오브젝트 수</param>
    private void CreateObj(int m_count)
    {
        for (int i = 0; i < m_count; i++)
        {
            GameObject newObj = GameObject.Instantiate(_prefab);
            newObj.transform.SetParent(_dir);
            newObj.SetActive(false);
        }
    }

    /// <summary>
    /// 내부 풀(리스트)에 저장된 마지막 오브젝트를 전달합니다.
    /// </summary>
    public GameObject GetObj()
    {
        if (_pool.Count < 1)
        {
            CreateObj(_pool.Capacity * 2);
        }

        GameObject obj = _pool[_pool.Count - 1];
        _pool.RemoveAt(_pool.Count - 1);
        return obj;
    }

    /// <summary>
    /// 반환요청 된 오브젝트를 내부 풀로 저장합니다.
    /// </summary>
    public void Return(IPooling m_obj)
    {
        _pool.Add(m_obj.Obj);
    }
}