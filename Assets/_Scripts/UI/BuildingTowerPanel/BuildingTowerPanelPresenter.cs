using ItemLinks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MVP
{
    public interface IBuildingTowerPanelPresenter : IPresenter<IBuildingTowerPanelView>
    {
        void CreatedTowerClick(GameObject prefabTower, int price);
        void UpdateButtonInteractableHandler(List<TowerItem> buttons);
    }
    public class BuildingTowerPanelPresenter : IBuildingTowerPanelPresenter
    {
        public IBuildingTowerPanelView View { get; private set; }

        private CreateTowerCommand _createTowerCommand;

        public BuildingTowerPanelPresenter(IBuildingTowerPanelView view, CreateTowerCommand createTowerCommand)
        {
            View = view;
            _createTowerCommand = createTowerCommand;
        }

        public void CreatedTowerClick(GameObject prefabTower, int price)
        {
            _createTowerCommand.Execute(prefabTower, price);
        }

        public void Initialize()
        {
            View.InitPresenter(this);
        }

        public void UpdateButtonInteractableHandler(List<TowerItem> buttons)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i].GetComponent<Button>();
            }
        }
    }
}
