using Command;
using UnityEngine;
using Helpers;
using Helpers.Events;

namespace MVP
{
    public class CreateTowerCommand : ICommand<GameObject>
    {
        private SelectedController _selectedController;

        public CreateTowerCommand(SelectedController selectedController)
        {
            _selectedController = selectedController;
        }

        public void Execute(GameObject data)
        {
            var tower = Object.Instantiate(data, _selectedController.GetActiveSelected().transform);
            tower.transform.SetPositionAndRotation(_selectedController.GetActiveSelected().GetComponent<SelectedBuildingPoint>().GetBuildingPosition().position,
                _selectedController.GetActiveSelected().GetComponent<SelectedBuildingPoint>().GetBuildingPosition().rotation);

            PrepareBuildPoint();
        }

        private void PrepareBuildPoint()
        {
            _selectedController.GetActiveSelected().GetComponent<Canvas>().enabled = false;
            _selectedController.GetActiveSelected().GetComponent<BoxCollider>().enabled = false;
            EventAggregator.Post(this, new DeselectedAllEvent());
        }
    }
}
