using System;
using System.Collections.Generic;

// ── v1: Hardware & Contracts ──────────────────────────────────────────────────

/// <summary>A CPU, RAM or Network component card added by a mod.</summary>
public class ModComponentData
{
    /// <summary>Unique asset name used for upgrade-chain linking (e.g. "MyCPU_T1").</summary>
    public string AssetName             = "";
    /// <summary>Player-visible name shown on the component card.</summary>
    public string DisplayName           = "";
    /// <summary>"CPU", "RAM" or "Network"</summary>
    public string ComponentType         = "CPU";
    /// <summary>Fictional hardware vendor/brand shown next to the display name (e.g. "Ambel", "Corvex"). Purely cosmetic.</summary>
    public string Vendor = "";
    /// <summary>Component capacity: cores for CPU, GB for RAM, Mbps for Network.</summary>
    public int    Value                 = 4;
    /// <summary>Power cost added to the server's energy bill per simulation tick.</summary>
    public float  PowerCostPerTick      = 0.02f;
    /// <summary>Cost in dollars to upgrade to this component from the previous tier.</summary>
    public float  UpgradeCost           = 500f;
    /// <summary>AssetName of the component this upgrades FROM. Null = root tier.</summary>
    public string UpgradesFromAssetName = null;
    /// <summary>Comma-separated list of chassis AssetNames (ModServerData.AssetName — the unique chassis
    /// identifier, NOT the display ServerType label) this component may be installed in, e.g.
    /// "RackChassisElite,RackChassisPro". Null or empty = compatible with every chassis. Players choose from
    /// all currently compatible components of a type when upgrading — this list is what "compatible" means.</summary>
    public string CompatibleChassisTypes = null;
}

/// <summary>A client contract injected into the contract board by a mod.</summary>
public class ModContractData
{
    /// <summary>Unique asset name for this contract template.</summary>
    public string AssetName             = "";
    /// <summary>Display name of the client company.</summary>
    public string ClientName            = "";
    /// <summary>Short description shown on the contract card (supports "loc:" prefix for localisation).</summary>
    public string Description           = "";
    /// <summary>Income awarded to the player per simulation tick while the contract is active.</summary>
    public float  IncomePerTick         = 1f;
    /// <summary>Minimum CPU cores the assigned server must provide.</summary>
    public int    RequiredCpuCores      = 2;
    /// <summary>Minimum RAM in GB the assigned server must provide.</summary>
    public int    RequiredRamGB         = 4;
    /// <summary>Minimum bandwidth in Mbps the assigned server must provide.</summary>
    public int    RequiredBandwidthMbps = 100;
    /// <summary>Total contract duration in simulation ticks.</summary>
    public int    DurationTicks         = 120;
    /// <summary>Minimum reputation score required before this contract appears on the board.</summary>
    public float  MinRepScore           = 0f;
    /// <summary>"General","AITraining","CryptoMining","VideoStreaming","Government"</summary>
    public string Category              = "General";
    /// <summary>Minimum storage in GB the assigned server must provide (0 = no storage requirement).</summary>
    public int RequiredStorageGB = 0;
    /// <summary>"Standard","Tough","Friendly","Strict","Desperate"</summary>
    public string Personality           = "Standard";
    /// <summary>Loyal clients return with follow-up contracts after successful completion.</summary>
    public bool   IsLoyal               = false;
    /// <summary>Volatile contracts (e.g. CryptoMining) have randomised income per tick.</summary>
    public bool   IsVolatile            = false;
    /// <summary>Required uptime percentage for SLA compliance (0 = no SLA check).</summary>
    public float  SlaMinUptime          = 0f;
}

/// <summary>A server rack type added by a mod.</summary>
public class ModRackData
{
    /// <summary>Unique asset name for this rack type.</summary>
    public string AssetName         = "";
    /// <summary>Display type label shown in the shop (e.g. "Open Frame").</summary>
    public string RackType          = "";
    /// <summary>Short description shown in the shop card.</summary>
    public string Description       = "";
    /// <summary>Total rack capacity in rack units (U).</summary>
    public int    MaxSlots          = 4;
    /// <summary>Purchase price in dollars.</summary>
    public float  PurchaseCost      = 1000f;
    /// <summary>Cooling efficiency multiplier applied to all servers in this rack. Values below 1.0 reduce heat.</summary>
    public float  CoolingMultiplier = 1f;
    /// <summary>Power efficiency multiplier applied to all servers in this rack. Values below 1.0 reduce energy costs.</summary>
    public float  PowerEfficiency   = 1f;
}

/// <summary>A server chassis type added by a mod.</summary>
public class ModServerData
{
    /// <summary>Unique asset name for this chassis type.</summary>
    public string AssetName               = "";
    /// <summary>Display type label shown in the shop (e.g. "Tower", "1U Rack").</summary>
    public string ServerType              = "";
    /// <summary>Short description shown in the shop card.</summary>
    public string Description             = "";
    /// <summary>Height of this server in rack units (U). Determines how many slots it occupies.</summary>
    public int    FormFactor              = 1;
    /// <summary>Purchase price in dollars.</summary>
    public float  PurchaseCost            = 2000f;
    /// <summary>Base power cost per simulation tick before component costs are added.</summary>
    public float  BasePowerCostPerTick    = 0.05f;
    /// <summary>Maximum safe operating temperature in °C. Exceeded → performance degradation.</summary>
    public int    MaxTemperature          = 85;
    /// <summary>Maximum CPU cores this chassis supports (0 = no cap).</summary>
    public int    MaxCpuCores             = 0;
    /// <summary>Maximum RAM in GB this chassis supports (0 = no cap).</summary>
    public int    MaxRamGB                = 0;
    /// <summary>Maximum bandwidth in Mbps this chassis supports (0 = no cap).</summary>
    public int    MaxBandwidthMbps        = 0;
    /// <summary>Maximum storage in GB this chassis supports (0 = no cap).</summary>
    public int MaxStorageGB = 0;
    /// <summary>AssetName of the default Storage component pre-installed on this chassis.</summary>
    public string DefaultStorageAssetName = "";
    /// <summary>AssetName of the default CPU component pre-installed on this chassis.</summary>
    public string DefaultCpuAssetName     = "CPU_T1";
    /// <summary>AssetName of the default RAM component pre-installed on this chassis.</summary>
    public string DefaultRamAssetName     = "RAM_T1";
    /// <summary>AssetName of the default Network component pre-installed on this chassis.</summary>
    public string DefaultNetworkAssetName = "Net_T1";
}

/// <summary>A server software item or hacking tool added by a mod.</summary>
public class ModSoftwareData
{
    /// <summary>Unique asset name for this software item.</summary>
    public string AssetName             = "";
    /// <summary>Display name shown in the shop and install confirmation.</summary>
    public string SoftwareName          = "";
    /// <summary>Short description shown in the shop card.</summary>
    public string Description           = "";
    /// <summary>Line identifier that groups upgrade tiers of the same software together.</summary>
    public string LineId                = "";
    /// <summary>"ServerSoftware" or "HackingTool"</summary>
    public string Category              = "ServerSoftware";
    /// <summary>"ContractIncome","TemperatureReduce","HackSuccess","ProbeDetectReduce","TraceGainReduce","DDoSMultiplier"</summary>
    public string Effect                = "ContractIncome";
    /// <summary>Magnitude of the effect (e.g. 0.08 = 8% for ContractIncome).</summary>
    public float  EffectValue           = 0.05f;
    /// <summary>Initial purchase price in dollars.</summary>
    public float  PurchaseCost          = 500f;
    /// <summary>Upgrade price in dollars when replacing a lower tier.</summary>
    public float  UpgradeCost           = 1000f;
    /// <summary>AssetName of the software this upgrades FROM. Null = root tier.</summary>
    public string UpgradesFromAssetName = null;
}

/// <summary>A hireable staff member added by a mod.</summary>
public class ModStaffData
{
    /// <summary>Unique asset name for this staff configuration.</summary>
    public string AssetName             = "";
    /// <summary>Display name shown in the shop and hire confirmation.</summary>
    public string StaffName             = "";
    /// <summary>Short description shown in the shop card.</summary>
    public string Description           = "";
    /// <summary>Line identifier that groups upgrade tiers of the same staff role together.</summary>
    public string LineId                = "";
    /// <summary>Tier level within the upgrade line (1 = entry level).</summary>
    public int    Tier                  = 1;
    /// <summary>"SystemAdmin","SecurityExpert","NetworkEngineer","CoolingEngineer"</summary>
    public string Role                  = "CoolingEngineer";
    /// <summary>One-time hiring cost in dollars.</summary>
    public float  HireCost              = 1000f;
    /// <summary>Ongoing salary deducted from the player's balance per simulation tick.</summary>
    public float  SalaryPerTick         = 0.05f;
    /// <summary>Magnitude of the role-specific effect (e.g. 0.10 = 10% cooling reduction).</summary>
    public float  EffectValue           = 0.10f;
    /// <summary>Maximum number of this staff member the player can hire simultaneously.</summary>
    public int    MaxHireable           = 1;
    /// <summary>AssetName of the staff config this upgrades FROM. Null = root tier.</summary>
    public string UpgradesFromAssetName = null;
}

// ── v2: Meta Content ──────────────────────────────────────────────────────────

/// <summary>A custom achievement unlocked through normal gameplay.</summary>
public class ModAchievementData
{
    /// <summary>Unique asset name for this achievement.</summary>
    public string AssetName;
    /// <summary>Emoji or short symbol displayed on the achievement badge.</summary>
    public string Icon          = "🏆";
    /// <summary>Achievement title shown in the achievements panel.</summary>
    public string Title;
    /// <summary>Description explaining how to unlock the achievement.</summary>
    public string Description;
    /// <summary>Category tag used to group achievements in the UI (e.g. "economy", "hardware", "meta").</summary>
    public string Category      = "meta";
    /// <summary>MissionContext field name to evaluate (e.g. "Balance", "ContractsDone", "ModsLoaded").</summary>
    public string ContextField;
    /// <summary>Numeric threshold the ContextField must reach to unlock this achievement.</summary>
    public float  Threshold;
}

/// <summary>A daily or weekly challenge added by a mod.</summary>
public class ModChallengeData
{
    /// <summary>Unique asset name for this challenge.</summary>
    public string AssetName;
    /// <summary>Localisation key for the challenge title.</summary>
    public string TitleKey;
    /// <summary>Localisation key for the challenge description.</summary>
    public string DescKey;
    /// <summary>"EarnBalance","CompleteContracts","DoHacks","SurviveAlarmFree","HireOrTrain","UpgradeComponent"</summary>
    public string ChallengeType = "EarnBalance";
    /// <summary>Numeric goal the player must reach to complete the challenge.</summary>
    public float  Target        = 5000f;
    /// <summary>Dollar reward granted on completion.</summary>
    public float  RewardMoney   = 500f;
    /// <summary>Reputation points awarded on completion.</summary>
    public float  RewardRep     = 2f;
}

/// <summary>A random in-game event added by a mod.</summary>
public class ModRandomEventData
{
    /// <summary>Unique asset name for this event.</summary>
    public string AssetName;
    /// <summary>Localisation key for the event title (overrides <see cref="Title"/> when set).</summary>
    public string TitleKey;
    /// <summary>Localisation key for the event description (overrides <see cref="Description"/> when set).</summary>
    public string DescriptionKey;
    /// <summary>Fallback event title used when no localisation key is set.</summary>
    public string Title;
    /// <summary>Fallback event description used when no localisation key is set.</summary>
    public string Description;
    /// <summary>Relative weight in the random pool. Higher = more frequent (base events use Weight = 3).</summary>
    public int    Weight        = 3;
    /// <summary>"Info","Warning","Critical"</summary>
    public string Severity      = "Info";
    /// <summary>Flat income bonus in dollars added to the player's balance while the event is active.</summary>
    public float  IncomeBonus   = 0f;
    /// <summary>Income multiplier applied while the event is active (0 = no multiplier effect).</summary>
    public float  IncomeMult    = 0f;
    /// <summary>How long the event stays active in simulation ticks (0 = instant/one-off).</summary>
    public int    DurationTicks = 0;
    /// <summary>Optional callback invoked when this event fires. Receives the live <see cref="RandomEventSystem"/>.</summary>
    public Action<RandomEventSystem> TriggerAction;
}

// ── v3: Economy & World ───────────────────────────────────────────────────────

/// <summary>A rival company added to the market by a mod.</summary>
public class ModRivalData
{
    /// <summary>Display name of the rival company.</summary>
    public string Name            = "Unnamed Rival";
    /// <summary>Short description shown in the Finance → Rivals panel.</summary>
    public string Description     = "";
    /// <summary>Probability per tick of the rival stealing a pending contract from the player (0–1).</summary>
    public float  Aggressiveness  = 0.4f;
    /// <summary>Initial visible market share fraction (0–1).</summary>
    public float  MarketShare     = 0.08f;
    /// <summary>Cost in dollars to acquire (neutralise) this rival.</summary>
    public float  AcquisitionCost = 30000f;
}

/// <summary>A loan offer added to the Finance panel by a mod.</summary>
public class ModLoanOfferData
{
    /// <summary>Display label shown in the Finance → Loans panel.</summary>
    public string Label           = "Mod Loan";
    /// <summary>Loan principal in dollars credited to the player on acceptance.</summary>
    public float  Amount          = 10000f;
    /// <summary>Interest repaid per simulation tick until the loan is cleared.</summary>
    public float  InterestPerTick = 0.15f;
    /// <summary>Minimum reputation score required for this loan to appear.</summary>
    public float  MinRepScore     = 0f;
}

/// <summary>An insurance policy added to the Finance panel by a mod.</summary>
public class ModInsurancePolicyData
{
    /// <summary>Display label shown in the Finance → Insurance panel.</summary>
    public string Label              = "Mod Policy";
    /// <summary>Short description of what the policy covers.</summary>
    public string Description        = "";
    /// <summary>"DDoS","Hardware","PowerOutage","Full"</summary>
    public string Coverage           = "DDoS";
    /// <summary>Premium deducted from the player's balance per simulation tick.</summary>
    public float  PremiumPerTick     = 0.30f;
    /// <summary>Dollar amount paid out when a covered incident occurs.</summary>
    public float  CompensationAmount = 1000f;
}

/// <summary>A seasonal event triggered on a specific in-game month by a mod.</summary>
public class ModSeasonalEventData
{
    /// <summary>Unique identifier for this seasonal event.</summary>
    public string Id            = "";
    /// <summary>Display title shown in the news ticker when the event triggers.</summary>
    public string Title         = "";
    /// <summary>Longer description shown in the event notification.</summary>
    public string Description   = "";
    /// <summary>In-game month (1–12) when this event can trigger.</summary>
    public int    Month         = 1;
    /// <summary>Flat income bonus in dollars applied while the event is active.</summary>
    public float  IncomeBonus   = 0f;
    /// <summary>Duration of the event in simulation ticks.</summary>
    public int    DurationTicks = 48;
    /// <summary>Probability (0–1) that this event fires during its target month check.</summary>
    public float  TriggerChance = 0.5f;
}

/// <summary>A custom terminal command registered by a mod.</summary>
public class ModTerminalCommandData
{
    /// <summary>The command keyword the player types (lowercase, no spaces).</summary>
    public string Command     = "modcmd";
    /// <summary>One-line description shown in the terminal "help" listing.</summary>
    public string Description = "A custom mod command";
    /// <summary>Usage hint displayed in the "help" listing (e.g. "modcmd [args]").</summary>
    public string Usage       = "modcmd [args]";
    /// <summary>Handler invoked when the command is executed. args[0] = command, args[1..n] = arguments.
    /// Return the text to print, or null to print nothing.</summary>
    public Func<string[], GameController, string> Handler = null;
}

/// <summary>A headline added to the in-game news ticker pool by a mod.</summary>
public class ModNewsEntryData
{
    /// <summary>Headline text displayed in the scrolling news ticker.</summary>
    public string Headline = "";
}

/// <summary>A localisation key/value pair contributed by a mod for a specific language.</summary>
public class ModLocalizationEntry
{
    /// <summary>BCP-47 language code (e.g. "en", "de", "fr").</summary>
    public string Language = "en";
    /// <summary>Localisation key referenced elsewhere in mod data (e.g. "contract_desc").</summary>
    public string Key      = "";
    /// <summary>Translated string value for this key and language.</summary>
    public string Value    = "";
}

/// <summary>A Zero-Day exploit added to the hacking market pool by a mod.</summary>
public class ModZeroDayData
{
    /// <summary>Display name of the exploit shown in the Zero-Day market.</summary>
    public string Name;
    /// <summary>Short description of the exploit's mechanics.</summary>
    public string Description;
    /// <summary>Minimum hack difficulty level this exploit applies to (inclusive).</summary>
    public int    TargetDifficultyMin = 1;
    /// <summary>Maximum hack difficulty level this exploit applies to (inclusive).</summary>
    public int    TargetDifficultyMax = 5;
    /// <summary>Additive bonus to hack success probability while this exploit is active (e.g. 0.25 = +25%).</summary>
    public float  SuccessBonus        = 0.25f;
    /// <summary>Purchase price in dollars.</summary>
    public float  Price               = 3000f;
    /// <summary>Number of times the exploit can be used before it is consumed.</summary>
    public int    Uses                = 2;
}
