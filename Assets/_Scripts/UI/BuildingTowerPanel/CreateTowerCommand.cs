using Command;
using UnityEngine;
using Helpers;
using Helpers.Events;

namespace MVP
{
    public class CreateTowerCommand : ICommand<GameObject, int>
    {
        private SelectedController _selectedController;

        public CreateTowerCommand(SelectedController selectedController)
        {
            _selectedController = selectedController;
        }

        public void Execute(GameObject data, int price)
        {
            var buildPoint = _selectedController.GetActiveSelected();

            if (buildPoint == null)
                return;

            if (LevelObserver.Instance.Energy >= price)
            {
                var tower = Object.Instantiate(data, buildPoint.transform);
                tower.transform.SetPositionAndRotation(_selectedController.GetActiveSelected().GetComponent<SelectedBuildingPoint>().GetBuildingPosition().position,
                    buildPoint.GetComponent<SelectedBuildingPoint>().GetBuildingPosition().rotation);

                PrepareBuildPoint();

                EventAggregator.Post(this, new EnergyUpdateEvent() { MoneyAmmount = -price });
            }
        }

        private void PrepareBuildPoint()
        {
            _selectedController.GetActiveSelected().GetComponent<Canvas>().enabled = false;
            _selectedController.GetActiveSelected().GetComponent<BoxCollider>().enabled = false;
            EventAggregator.Post(this, new DeselectedAllEvent());
        }
    }
}
