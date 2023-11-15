using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;

public class TowerObserver : MonoBehaviour
{
    [SerializeField] private List<TowerLevels> _upgradeList = new();

    public void UpgradeTower(Tower tower)
    {
        var buildPointPosition = tower.transform.parent.GetComponent<SelectedBuildingPoint>().GetBuildingPosition();
        var currentTower = tower.GetTowerData();
        ApplyUpgrade(currentTower);

        var newTowerObject = GetTower(currentTower.TowerType, currentTower.Level);
        if (newTowerObject == null)
        {
            Debug.Log("Not enourght");
            return;
        }

        var newTower = Instantiate(newTowerObject, buildPointPosition.parent);

        newTower.transform.SetPositionAndRotation(buildPointPosition.position, buildPointPosition.rotation);

        currentTower.Level++;

        newTower.GetComponent<Tower>().SetTowerData(currentTower);
        EventAggregator.Post(null, new DeselectedAllEvent());
        Destroy(tower.gameObject);
    }

    private void ApplyUpgrade(TowerData data)
    {
        var upgradeStrenght = data.UpgradeStrenght;

        data.Radius += CanculateInterest(data.Radius, upgradeStrenght);
        data.SpeedRotate += CanculateInterest(data.SpeedRotate, upgradeStrenght);
        data.Damage = new Vector2(data.Damage.x + CanculateInterest(data.Damage.x, upgradeStrenght), data.Damage.y + CanculateInterest(data.Damage.y, upgradeStrenght));
        data.ReloadingSpeed -= CanculateInterest(data.ReloadingSpeed, upgradeStrenght);
        data.FiringSpread -= CanculateInterest(data.FiringSpread, upgradeStrenght);

    }

    private float CanculateInterest(float current, float ammountInterest)//!!!!! перенести в глобальний клас
    {
        return (current * ammountInterest) / 100f;
    }
    private GameObject GetTower(TowerType towerType, int level)
    {
        var result = _upgradeList.Find(tower => tower.TowerType == towerType && tower.Level == level + 1);

        if (LevelObserver.Instance.Energy >= result.Price)
        {
            EventAggregator.Post(this, new EnergyUpdateEvent() { MoneyAmmount = -result.Price });
            return result.Prefab;
        }
        else
            return null;
    }
}

[System.Serializable]
public class TowerLevels
{
    public TowerType TowerType;
    public int Level;
    public int Price;
    public GameObject Prefab;
}
