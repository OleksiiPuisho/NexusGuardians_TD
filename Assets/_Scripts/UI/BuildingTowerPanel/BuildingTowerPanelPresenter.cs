using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP
{
    public interface IBuildingTowerPanelPresenter : IPresenter<IBuildingTowerPanelView>
    {
        void CreatedTowerClick(GameObject prefabTower);
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

        public void CreatedTowerClick(GameObject prefabTower)
        {
            _createTowerCommand.Execute(prefabTower);
            View.Hide();
        }

        public void Initialize()
        {
            View.InitPresenter(this);
        }
    }
}
