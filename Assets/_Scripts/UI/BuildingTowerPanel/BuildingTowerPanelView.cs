using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemLinks;
using UnityEngine.UI;

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

        public void CreateTowerButtonClick(GameObject prefab) => Presenter.CreatedTowerClick(prefab);
    }
}
