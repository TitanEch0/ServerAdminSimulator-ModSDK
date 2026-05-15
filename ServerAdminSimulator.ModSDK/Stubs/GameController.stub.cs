/// <summary>
/// Stub of GameController — only members relevant for mod hooks are exposed.
/// The real implementation lives in Assembly-CSharp.dll (shipped with the game).
/// </summary>
public class GameController
{
    /// <summary>Economy model — read balance, add income/expenses.</summary>
    public EconomyModel Economy { get; }
    /// <summary>Simulation model — access servers, racks and contracts.</summary>
    public ServerSimulationModel Simulation { get; }
    /// <summary>Reputation system — read score and tier.</summary>
    public ReputationSystem  Reputation  { get; }
    /// <summary>Staff manager — query hired staff.</summary>
    public StaffManager      Staff       { get; }
    /// <summary>Software manager — query installed software.</summary>
    public SoftwareManager   Software    { get; }
    /// <summary>Random event system — read active income multiplier.</summary>
    public RandomEventSystem Events      { get; }
    /// <summary>Time system — read current tick, time scale and pause state.</summary>
    public TimeSystem        TimeSystem  { get; }
    /// <summary>Event logger — write messages to the in-game log panel.</summary>
    public EventLogger       Logger      { get; }
    /// <summary>True when the game is running in Demo mode.</summary>
    public static bool IsDemoMode { get; set; }
    /// <summary>Player-chosen display name for the datacenter.</summary>
    public string DatacenterName  { get; }
}

/// <summary>Provides read/write access to the player's economy.</summary>
public class EconomyModel
{
    /// <summary>Current player balance in dollars.</summary>
    public float Balance               { get; }
    /// <summary>Total income earned in the current simulation tick.</summary>
    public float TotalIncomeThisTick   { get; }
    /// <summary>Total expenses paid in the current simulation tick.</summary>
    public float TotalExpensesThisTick { get; }
    /// <summary>Adds unconditional income to the player balance.</summary>
    public void  AddIncome (float amount) { }
    /// <summary>Adds unconditional expense to the player balance.</summary>
    public void  AddExpense(float amount) { }
    /// <summary>Deducts <paramref name="amount"/> if the balance allows it. Returns false if insufficient funds.</summary>
    public bool  TrySpend  (float amount) { return false; }
}

/// <summary>Read-only view of the active server simulation state.</summary>
public class ServerSimulationModel
{
    /// <summary>All servers currently installed across all racks.</summary>
    public System.Collections.Generic.IReadOnlyList<ServerData>  Servers          { get; }
    /// <summary>All racks currently installed in the datacenter.</summary>
    public System.Collections.Generic.IReadOnlyList<RackData>    Racks            { get; }
    /// <summary>Contracts that are currently running and generating income.</summary>
    public System.Collections.Generic.List<ContractData>         ActiveContracts  { get; }
    /// <summary>Contracts that are available to accept or decline.</summary>
    public System.Collections.Generic.List<ContractData>         PendingContracts { get; }
}

/// <summary>Tracks player reputation score, tier and contract history.</summary>
public class ReputationSystem
{
    /// <summary>Numeric reputation score (0–100+).</summary>
    public float  Score         { get; }
    /// <summary>Localised tier label (e.g. "Startup", "Trusted", "Elite").</summary>
    public string TierLabel     { get; }
    /// <summary>Total number of contracts successfully completed this session.</summary>
    public int    ContractsDone { get; }
    /// <summary>Registers a successfully completed contract, increasing score.</summary>
    public void   RegisterSuccess(float incomePerTick) { }
    /// <summary>Registers an SLA violation or failed contract, decreasing score.</summary>
    public void   RegisterFailure() { }
}

/// <summary>Controls and reports simulation time.</summary>
public class TimeSystem
{
    /// <summary>Number of simulation ticks elapsed since the session started.</summary>
    public int   CurrentTick { get; }
    /// <summary>Current time-scale multiplier (1×, 2×, 5×).</summary>
    public float TimeScale   { get; }
    /// <summary>True when the simulation is running (not paused).</summary>
    public bool  IsRunning   { get; }
    /// <summary>Returns a formatted time string such as "Day 3 - 14:20".</summary>
    public string GetFormattedTime() { return ""; }
}

/// <summary>Writes messages to the in-game event log panel.</summary>
public class EventLogger
{
    /// <summary>Appends a log entry with the specified severity and timestamp.</summary>
    public void Log(string message, LogLevel level, string time) { }
}

/// <summary>Log entry severity levels.</summary>
public enum LogLevel
{
    /// <summary>Informational message.</summary>
    Info,
    /// <summary>Non-critical warning.</summary>
    Warning,
    /// <summary>Critical error.</summary>
    Error
}

/// <summary>Stub for staff management — exposes only the type for hook signatures.</summary>
public class StaffManager { }
/// <summary>Stub for software management — exposes only the type for hook signatures.</summary>
public class SoftwareManager { }

/// <summary>A server installed in a rack.</summary>
public class ServerData
{
    /// <summary>Unique identifier for this server instance.</summary>
    public string id;
    /// <summary>Player-visible name of the server.</summary>
    public string displayName;
    /// <summary>True when the server is powered on and accepting contracts.</summary>
    public bool IsOnline;
}

/// <summary>A rack installed in the datacenter.</summary>
public class RackData
{
    /// <summary>Unique identifier for this rack instance.</summary>
    public string id;
    /// <summary>Player-visible name of the rack.</summary>
    public string displayName;
}

/// <summary>A contract instance (active or pending).</summary>
public class ContractData
{
    /// <summary>Unique identifier for this contract instance.</summary>
    public string id;
    /// <summary>The config asset this contract was generated from.</summary>
    public ContractConfig config;
}
