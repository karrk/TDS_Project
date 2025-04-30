using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnCoolTime;
    private WaitForSeconds _spawnCoolWait;

    private void Start()
    {
        _spawnCoolWait = new WaitForSeconds(_spawnCoolTime);
        StartCoroutine(StartSpawn());
    }

    /// <summary>
    /// 구체적인 몬스터 스폰 로직
    /// </summary>
    private void Spawn()
    {
        E_Way way = GetRandomWay();
        float wayPosY = Manager.Data.TopEdgeY(way);

        Monster newMonster = Manager.Pool.Get<Monster>(E_MonsterType.Melee);
        newMonster.SetLayer(way);

        newMonster.transform.position = new Vector2(transform.position.x, wayPosY + newMonster.PivotToBot);
        newMonster.LogicStart();
    }

    /// <summary>
    /// 3 갈래 길중 무작위 경로를 선정합니다.
    /// </summary>
    private E_Way GetRandomWay()
    {
        return (E_Way)Random.Range((int)E_Way.Top, (int)E_Way.Size);
    }

    /// <summary>
    /// 설정된 쿨타임 주기로 몬스터를 스폰합니다.
    /// </summary>
    public IEnumerator StartSpawn()
    {
        while (true)
        {
            yield return _spawnCoolWait;

            Spawn();
        }
    }
}
