using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP
{
    public interface IPanelInfoLevelModel : IModel
    {
        event Action<TowerData> TowerDataChanged;
        void SetTowerData(TowerData towerData);
    }
    public class PanelInfoLevelModel : IPanelInfoLevelModel
    {
        public event Action<TowerData> TowerDataChanged;

        private TowerData _towerData;

        public void SetTowerData(TowerData towerData)
        {
            _towerData = towerData;
            TowerDataChanged?.Invoke(_towerData);
        }
    }
}
