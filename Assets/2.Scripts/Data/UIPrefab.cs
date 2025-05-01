using UnityEngine;

[CreateAssetMenu(fileName = "UIPrefab", menuName = "GameData/UI Prefab")]
public class UIPrefab : PrefabData
{
    [SerializeField] private E_Text _type;
    public E_Text Type => _type;
}