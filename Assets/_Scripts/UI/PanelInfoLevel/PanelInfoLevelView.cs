using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;
using Helpers.Events;
using TMPro;

namespace MVP
{
    public interface IPanelInfoLevelView : IView<IPanelInfoLevelPresenter>
    {
        void UpdateTowerInfo(TowerInfo towerInfo);
    }
    public class PanelInfoLevelView : BaseView, IPanelInfoLevelView
    {
        [SerializeField] private SelectedController _selectedController;
        [SerializeField] private TowerObserver _towerObserver;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _destroyTowerButton;
        [SerializeField] private Button _upgradeTowerButton;

        [SerializeField] private TMP_Text _tittle;
        //info Tower
        [SerializeField] private TMP_Dropdown _typeBullet;
        [SerializeField] private TMP_Dropdown _typeSearching;
        [SerializeField] private TMP_Text _typeTower;
        [SerializeField] private TMP_Text _radiusTower;
        [SerializeField] private TMP_Text _speedRotateTower;
        [SerializeField] private TMP_Text _damageTower;
        [SerializeField] private TMP_Text _reloadingSpeedTower;
        [SerializeField] private TMP_Text _firingSpreadTower;

        public IPanelInfoLevelPresenter Presenter { get; private set; }

        public void InitPresenter(IPanelInfoLevelPresenter presenter)
        {
            Presenter = presenter;
        }

        public void UpdateTowerInfo(TowerInfo towerInfo)
        {
            _tittle.text = towerInfo.Name;
            _typeTower.text = towerInfo.AttackType;
            _radiusTower.text = towerInfo.Radius;
            _speedRotateTower.text = towerInfo.SpeedRotate;
            _damageTower.text = towerInfo.Damage;
            _reloadingSpeedTower.text = towerInfo.ReloadingSpeed;
            _firingSpreadTower.text = towerInfo.FiringSpread;

            _typeBullet.value = (int)towerInfo.BulletType;
            _typeSearching.value = (int)towerInfo.SerchingType;
        }


        protected override void OnAwake()
        {
            base.OnAwake();

            EventAggregator.Subscribe<SelectedObjectEvent>(SelectedObjecttHandler);
            EventAggregator.Subscribe<DeselectedAllEvent>(delegate { _upgradeTowerButton.onClick.RemoveAllListeners(); Hide(); });

            _closeButton.onClick.AddListener(delegate { Presenter.ClosePanelHandler(); });
            _destroyTowerButton.onClick.AddListener(delegate { Presenter.DestroyTowerHandler(); });            
        }

        private void SelectedObjecttHandler(object sender, SelectedObjectEvent eventData)
        {
            if (eventData.TypeSelectedObject == TypeSelectedObject.Tower)
            {
                Show();
                var tower = eventData.SelectedObject.GetComponent<Tower>();
                Presenter.UpdateTowerInfoData(tower.GetTowerData());

                
                if (tower.GetTowerData().Level >= 3)
                {
                    _upgradeTowerButton.interactable = false;
                }
                else
                {
                    _upgradeTowerButton.interactable = true;
                    _upgradeTowerButton.onClick.AddListener(delegate { Presenter.UpgradeTowerHandler(tower); });
                }
            }
            else
                Hide();
        }
        private void OnDestroy()
        {
            EventAggregator.Unsubscribe<SelectedObjectEvent>(SelectedObjecttHandler);
            EventAggregator.Unsubscribe<DeselectedAllEvent>(delegate { Hide(); });
        }
    }
}
