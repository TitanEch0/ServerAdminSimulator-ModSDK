/// <summary>
/// Override this class in your mod and return it from <see cref="IServerAdminMod.GetHooks"/>.
/// All methods are optional (no-op by default) — override only the hooks you need.
/// </summary>
public abstract class ModGameHooks
{
    /// <summary>Called once after the game session has fully loaded (new game or save loaded).</summary>
    public virtual void OnGameStart(GameController gc) { }
    /// <summary>Called every simulation tick. Use <paramref name="tick"/> for interval-based logic.</summary>
    public virtual void OnTick(GameController gc, int tick) { }
    /// <summary>Called when the player successfully accepts a contract.</summary>
    public virtual void OnContractAccepted(ContractConfig contract, GameController gc) { }
    /// <summary>Called when a contract runs to completion and the final payment is made.</summary>
    public virtual void OnContractCompleted(ContractConfig contract, GameController gc) { }
    /// <summary>Called when a contract is manually cancelled by the player.</summary>
    public virtual void OnContractCancelled(ContractConfig contract, GameController gc) { }
    /// <summary>Called when a terminal hack attempt succeeds. <paramref name="reward"/> is the payout in dollars.</summary>
    public virtual void OnHackSucceeded(float reward, GameController gc) { }
    /// <summary>Called when a terminal hack attempt fails.</summary>
    public virtual void OnHackFailed(GameController gc) { }
    /// <summary>Called when the player buys a new server. <paramref name="chassisName"/> is the asset name.</summary>
    public virtual void OnServerPurchased(string chassisName, GameController gc) { }
    /// <summary>Called when the player upgrades a component. <paramref name="componentType"/> is "CPU", "RAM" or "Network".</summary>
    public virtual void OnComponentUpgraded(string componentType, GameController gc) { }
    /// <summary>Called when the player reaches a new reputation tier. <paramref name="newTier"/> is the localised tier label.</summary>
    public virtual void OnReputationTierChanged(string newTier, GameController gc) { }
    /// <summary>Called when the datacenter expands to a new room. <paramref name="newLevel"/> is 0-based.</summary>
    public virtual void OnDatacenterExpanded(int newLevel, GameController gc) { }
    /// <summary>Called on bankruptcy — the game session is ending.</summary>
    public virtual void OnGameOver(GameController gc) { }
    /// <summary>Called when the player manually saves the game. <paramref name="slot"/> is 0-based.</summary>
    public virtual void OnGameSaved(int slot) { }
    /// <summary>Called when a save slot is loaded into an active session. <paramref name="slot"/> is 0-based.</summary>
    public virtual void OnGameLoaded(int slot) { }
}
