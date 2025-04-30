using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "GameData/Monster Prefab")]
public class MonsterPrefab : PrefabData
{
    [SerializeField] private E_MonsterType _type;
    public E_MonsterType Type => _type;
}