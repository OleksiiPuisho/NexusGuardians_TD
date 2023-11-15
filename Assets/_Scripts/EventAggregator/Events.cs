namespace Helpers.Events
{
    public class GameWinEvent { }
    public class GameOverEvent { }
    public class EnergyUpdateEvent { public int MoneyAmmount; }
    public class EnergyUpdateUIEvent { public int MoneyAmmount; }

    public class SelectedObjectEvent { public TypeSelectedObject TypeSelectedObject;  public SelectedObject SelectedObject; }
    public class DeselectedAllEvent { }

    public class EnemyDeathEvent { }
    public class SelectedBuildPointEvent { public SelectedBuildingPoint BuildingPoint; }
    public class UpdateInfoWavePanel { }
}