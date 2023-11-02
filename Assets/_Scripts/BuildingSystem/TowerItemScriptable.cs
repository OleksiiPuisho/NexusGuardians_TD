using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerItemScriptable", menuName = "Create/TowerItem")]
public class TowerItemScriptable : ScriptableObject
{
    public GameObject PrefabItem;
    public TowerItemData[] TowerItems;
}

[System.Serializable]
public class TowerItemData
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _prefab;

    public Sprite Icon => _icon;
    public int Price => _price;
    public GameObject Prefab => _prefab;

}
