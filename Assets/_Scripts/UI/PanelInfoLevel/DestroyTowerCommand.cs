using Command;
using UnityEngine;
using Helpers;
using Helpers.Events;

namespace MVP
{
    public class DestroyTowerCommand : ICommand
    {
        private SelectedController _selectedController;

        public DestroyTowerCommand(SelectedController selectedController)
        {
            _selectedController = selectedController;
        }

        public void Execute()
        {
            var tower = _selectedController.GetActiveSelected().gameObject;

            var buildPoint = tower.transform.parent;
            buildPoint.GetComponent<Canvas>().enabled = true;
            buildPoint.GetComponent<BoxCollider>().enabled = true;

            Object.Destroy(tower);
            EventAggregator.Post(this, new DeselectedAllEvent());

        }
    }
}
