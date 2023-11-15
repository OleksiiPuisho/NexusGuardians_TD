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

        private List<TowerItem> _towerItemButtons = new();
        public IBuildingTowerPanelPresenter Presenter { get; private set; }

        public void InitPresenter(IBuildingTowerPanelPresenter presenter)
        {
            Presenter = presenter;
        }
        protected override void OnAwake()
        {
            base.OnAwake();
            PrepareTowerItems();

            EventAggregator.Subscribe<EnergyUpdateUIEvent>(UpdateButtonInteractableHandler);
        }

        private void UpdateButtonInteractableHandler(object sender, EnergyUpdateUIEvent eventData)
        {
            for (int i = 0; i < _towerItemButtons.Count; i++)
            {
                var button = _towerItemButtons[i].GetComponent<Button>();

                if (eventData.MoneyAmmount >= _towerItemButtons[i].Price)
                {
                    button.interactable = true;
                }
                else
                    button.interactable = false;
            }
        }

        private void PrepareTowerItems()
        {
            for (int i = 0; i < _towerItemScriptable.TowerItems.Length; i++)
            {
                int itemIndex = i;

                GameObject itemObject = Instantiate(_towerItemScriptable.PrefabItem, _towerItemContainer);
                TowerItem item = itemObject.GetComponent<TowerItem>();

                item.Icon.sprite = _towerItemScriptable.TowerItems[i].Icon;
                item.Price = _towerItemScriptable.TowerItems[i].Price;
                itemObject.GetComponent<Button>().onClick.AddListener(delegate { CreateTowerButtonClick(_towerItemScriptable.TowerItems[itemIndex].Prefab, _towerItemScriptable.TowerItems[itemIndex].Price); });

                _towerItemButtons.Add(item);
            }
        }
        private void CreateTowerButtonClick(GameObject prefab, int price) => Presenter.CreatedTowerClick(prefab, price);
        private void OnDestroy()
        {
            EventAggregator.Unsubscribe<EnergyUpdateUIEvent>(UpdateButtonInteractableHandler);
        }
    }
}
