using UnityEngine;

public class PrefabData : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;
}
