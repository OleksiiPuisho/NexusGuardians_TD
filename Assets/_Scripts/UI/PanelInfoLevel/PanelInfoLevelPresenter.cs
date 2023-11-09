using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers;
using Helpers.Events;
using TowerSystems;

namespace MVP
{
    public interface IPanelInfoLevelPresenter : IPresenter<IPanelInfoLevelView, IPanelInfoLevelModel>
    {
        void ClosePanelHandler();
        void DestroyTowerHandler();
        void UpdateTowerInfoData(TowerData towerData);
    }
    public class PanelInfoLevelPresenter : IPanelInfoLevelPresenter
    {
        private TowerInfoCommand _towerInfoCommand;
        private DestroyTowerCommand _destroyTowerCommand;
        public PanelInfoLevelPresenter(IPanelInfoLevelView view, IPanelInfoLevelModel model, TowerInfoCommand towerInfoCommand, DestroyTowerCommand destroyTowerCommand)
        {
            View = view;
            Model = model;
            _towerInfoCommand = towerInfoCommand;
            _destroyTowerCommand = destroyTowerCommand;
        }

        public IPanelInfoLevelView View { get; private set; }

        public IPanelInfoLevelModel Model { get; private set; }


        public void Initialize()
        {
            View.InitPresenter(this);

            Model.TowerDataChanged += SetTowerData;
        }
        public void ClosePanelHandler()
        {
            EventAggregator.Post(this, new DeselectedAllEvent());
            View.Hide();
        }

        public void UpdateTowerInfoData(TowerData towerData)
        {
            Model.SetTowerData(towerData);
        }


        private void SetTowerData(TowerData towerData)
        {
            _towerInfoCommand.Execute(towerData);
        }

        public void DestroyTowerHandler()
        {
            _destroyTowerCommand.Execute();
        }
    }

    public class TowerInfo
    {
        public string Name;
        public string AttackType;
        public BulletType BulletType;
        public SearchingType SerchingType;
        public string Radius;
        public string SpeedRotate;
        public string Damage;
        public string ReloadingSpeed;
        public string FiringSpread;
    }
}
