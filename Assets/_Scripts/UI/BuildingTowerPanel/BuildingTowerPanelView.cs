using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemLinks;
using UnityEngine.UI;
using Helpers;
using Helpers.Events;

namespace MVP
{
    public interface IBuildingTowerPanelView : IView<IBuildingTowerPanelPresenter>
    {

    }
    public class BuildingTowerPanelView : BaseView, IBuildingTowerPanelView
    {
        [SerializeField] private TowerItemScriptable _towerItemScriptable;
        [SerializeField] private Transform _towerItemContainer;
        public IBuildingTowerPanelPresenter Presenter { get; private set; }

        public void InitPresenter(IBuildingTowerPanelPresenter presenter)
        {
            Presenter = presenter;
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            PrepareTowerItems();

            EventAggregator.Subscribe<SelectedObjectEvent>(SelectedObjecttHandler);
            EventAggregator.Subscribe<DeselectedAllEvent>(delegate { Hide(); });
        }

        private void PrepareTowerItems()
        {
            for (int i = 0; i < _towerItemScriptable.TowerItems.Length; i++)
            {
                int itemIndex = i;

                GameObject item = Instantiate(_towerItemScriptable.PrefabItem, _towerItemContainer);
                item.GetComponent<TowerItem>().Icon.sprite = _towerItemScriptable.TowerItems[i].Icon;
                item.GetComponent<TowerItem>().PriceText.text = _towerItemScriptable.TowerItems[i].Price.ToString();
                item.GetComponent<Button>().onClick.AddListener(delegate { CreateTowerButtonClick(_towerItemScriptable.TowerItems[itemIndex].Prefab); });
            }
        }
        private void SelectedObjecttHandler(object sender, SelectedObjectEvent eventData)
        {
            if (eventData.TypeSelectedObject == TypeSelectedObject.BuildTowerPoint)
                Show();
            else
                Hide();
        }
        private void CreateTowerButtonClick(GameObject prefab) => Presenter.CreatedTowerClick(prefab);
        private void OnDestroy()
        {
            EventAggregator.Unsubscribe<SelectedObjectEvent>(SelectedObjecttHandler);
            EventAggregator.Unsubscribe<DeselectedAllEvent>(delegate { Hide(); });
        }
    }
}
