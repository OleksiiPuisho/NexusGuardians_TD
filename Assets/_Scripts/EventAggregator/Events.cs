namespace Helpers.Events
{
    public class GameWinEvent { }
    public class GameOverEvent { }
    public class MoneyUpdateEvent { public int MoneyAmmount; }

    public class SelectedObjectEvent { public TypeSelectedObject TypeSelectedObject;  public SelectedObject SelectedObject; }
    public class DeselectedAll { }

    public class EnemyDeathEvent { }
}