using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "GameData/Bullet Prefab")]
public class BulletPrefab : PrefabData
{
    [SerializeField] private E_Bullet _type;
    public E_Bullet Type => _type;
}