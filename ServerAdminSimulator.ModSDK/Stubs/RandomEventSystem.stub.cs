/// <summary>Stub of RandomEventSystem — used in <see cref="ModRandomEventData.TriggerAction"/>.</summary>
public class RandomEventSystem
{
    /// <summary>Current income multiplier applied by active random events (1.0 = no effect).</summary>
    public float IncomeMultiplier { get; }
    /// <summary>True while the given server has an unresolved storage-drive fault (offline until swapped via <see cref="GameController.SwapStorage"/>).</summary>
    public bool HasActiveStorageFault(string serverId) { return false; }
}
