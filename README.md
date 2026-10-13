# Server-Admin Simulator — Modding SDK

Official SDK for creating mods for **Server-Admin Simulator** by Ice-Phoenix.  
Reference `ServerAdminSimulator.ModSDK.dll` in your mod project, implement `IServerAdminMod`,
and drop the compiled DLL into the game's Mods folder.

> **Current API version:** v4.0  
> **MinGameVersion:** `1.3.0` (for the hardware content of v4; everything else works from `1.2.0`)

---

## Quick Start

**Step 1 — Create a C# Class Library** targeting `.NET Framework 4.8`:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net48</TargetFramework>
    <AssemblyName>MyAwesomeMod</AssemblyName>
    <LangVersion>8.0</LangVersion>
  </PropertyGroup>
</Project>
```

**Step 2 — Reference the SDK DLL:**

```xml
<ItemGroup>
  <Reference Include="ServerAdminSimulator.ModSDK">
    <HintPath>path\to\ServerAdminSimulator.ModSDK.dll</HintPath>
  </Reference>
</ItemGroup>
```

**Step 3 — Implement `IServerAdminMod`** in a `public` class:

```csharp
public class MyMod : IServerAdminMod
{
    public ModManifest GetManifest() => new ModManifest
    {
        Name           = "My Mod",
        Version        = "1.0.0",
        Author         = "You",
        MinGameVersion = "1.2.0",
    };

    public IEnumerable<ModContractData> GetContracts() => new[]
    {
        new ModContractData { AssetName = "MyContract_01", ClientName = "Acme Corp", IncomePerTick = 2f }
    };

    // Return Array.Empty<T>() for any method your mod does not use.
    public IEnumerable<ModComponentData>       GetComponents()        => Array.Empty<ModComponentData>();
    public IEnumerable<ModRackData>            GetRacks()             => Array.Empty<ModRackData>();
    // ... (implement all remaining interface members)
}
```

**Step 4 — Copy the compiled DLL** to the Mods folder:

```
%APPDATA%\..\LocalLow\Ice-Phoenix\Server-Admin Simulator\Mods\
```

The game's ModLoader scans this folder on startup and loads every valid `IServerAdminMod` implementation it finds. You only need to copy your own mod DLL — the game already ships with `ServerAdminSimulator.ModSDK.dll` bundled internally, so your reference to it resolves automatically at runtime.

---

## Compatibility Mode

Mods built against this SDK reference a separate, independently-compiled copy of `IServerAdminMod` — not the game's own internal one. The game detects this automatically (as of **v1.1.4**) and loads your mod through a compatibility bridge, so you don't need to do anything differently. Two things don't carry over through that bridge, though:

- **`GetHooks()` is not supported.** Hook method signatures reference the game's internal `GameController`/`ContractConfig`/`RandomEventSystem` types directly, which can't be bridged across assemblies. Return `null` (or don't override it) — the rest of your mod loads normally.
- **Delegate fields are dropped**, specifically `ModTerminalCommandData.Handler` and `ModRandomEventData.TriggerAction`. The surrounding data (command name/description/usage, event title/description/weight/etc.) still loads and displays fine — only the executable callback itself is skipped, since it also carries a `GameController` parameter.

Everything else — components, contracts, racks, servers, software, staff, achievements, challenges, rivals, loans, insurance, seasonal events, news entries, zero-day exploits, and localization — works exactly as documented below.

If you see `"was built against an incompatible IServerAdminMod copy ... loading in compatibility mode"` in the game's log, that's expected and not an error — it just means the two limitations above apply to that mod.

---

## API Reference

### Content Methods (`IServerAdminMod`)

| Method | API | Description |
|---|:---:|---|
| `GetManifest()` | v1 | Mod metadata — name, version, author, dependencies |
| `GetComponents()` | v1 | CPU, RAM, Network, and Storage upgrade cards *(can be restricted to specific chassis — see [Chassis Compatibility](#chassis-compatibility))* |
| `GetContracts()` | v1 | Client contracts shown on the contract board |
| `GetRacks()` | v1 | Server rack chassis types |
| `GetServers()` | v1 | Server form factors / chassis |
| `GetSoftware()` | v1 | Server software and hacking tools |
| `GetStaff()` | v1 | Hireable staff members |
| `GetAchievements()` | v2 | Custom unlockable achievements |
| `GetChallenges()` | v2 | Timed challenge objectives |
| `GetRandomEvents()` | v2 | Events that fire randomly during gameplay *(`TriggerAction` dropped in [Compatibility Mode](#compatibility-mode))* |
| `GetRivals()` | v3 | Competitor companies with market share |
| `GetLoanOffers()` | v3 | Custom bank loan products |
| `GetInsurancePolicies()` | v3 | Custom insurance policies |
| `GetSeasonalEvents()` | v3 | Month-based income modifiers |
| `GetTerminalCommands()` | v3 | Custom in-game terminal commands *(handler dropped in [Compatibility Mode](#compatibility-mode))* |
| `GetNewsEntries()` | v3 | Breaking-news ticker headlines |
| `GetZeroDayExploits()` | v3 | Special hacking tools with limited uses |
| `GetLocalization(lang)` | v3 | Translations keyed by language code (`"en"`, `"de"`, …) |
| `GetHooks()` | v3 | Returns your `ModGameHooks` subclass *(not available in [Compatibility Mode](#compatibility-mode))* |

### Hardware Methods (`IServerAdminModHardware`, optional)

Implement this second interface on the same class to add mainboards and switches — see [Mainboards, Switches & Towers](#mainboards-switches--towers).

| Method | API | Description |
|---|:---:|---|
| `GetMotherboards()` | v4 | Mainboards for the server configurator (sockets, RAM slots, PCIe slots, drive connectors, NIC limit) |
| `GetSwitchModels()` | v4 | Network switch models for the network manager shop |

### Lifecycle Hooks (`ModGameHooks`)

Extend `ModGameHooks` and override only what you need — all methods are no-ops by default.

| Hook | Fires when… |
|---|---|
| `OnGameStart(gc)` | A new game session begins |
| `OnTick(gc, tick)` | Each simulation tick |
| `OnContractAccepted(contract, gc)` | Player accepts a contract |
| `OnContractCompleted(contract, gc)` | Contract is fulfilled successfully |
| `OnContractCancelled(contract, gc)` | Contract is cancelled |
| `OnHackSucceeded(reward, gc)` | A hack attempt succeeds |
| `OnHackFailed(gc)` | A hack attempt fails |
| `OnServerPurchased(chassisName, gc)` | Player purchases a new server |
| `OnComponentUpgraded(componentType, gc)` | A CPU / RAM / Network / Storage card is upgraded |
| `OnReputationTierChanged(newTier, gc)` | Player's reputation tier changes |
| `OnDatacenterExpanded(newLevel, gc)` | Datacenter capacity is expanded |
| `OnGameOver(gc)` | Game-over condition is reached |
| `OnGameSaved(slot)` | Game is saved to a slot |
| `OnGameLoaded(slot)` | A save file is loaded |

---

## Component Upgrade Chains

Each server upgrades one component at a time along a fixed chain — the player never gets a
free choice of any compatible component, only a single "Upgrade → NextTier ($cost)" button that
always leads to whatever you designated as the next tier. (An earlier release let players pick
freely among every compatible component regardless of value, which amounted to letting them
"upgrade" into something weaker than what was already installed — that's been removed.)

Link tiers together with `ModComponentData.UpgradesFromAssetName`, set on the **higher** tier,
naming the `AssetName` of the tier it comes after:

```csharp
new ModComponentData { AssetName = "MyCPU_T1", DisplayName = "MyCorp CPU I",   Value = 4,  UpgradeCost = 500f  },
new ModComponentData { AssetName = "MyCPU_T2", DisplayName = "MyCorp CPU II",  Value = 8,  UpgradeCost = 1200f, UpgradesFromAssetName = "MyCPU_T1" },
new ModComponentData { AssetName = "MyCPU_T3", DisplayName = "MyCorp CPU III", Value = 16, UpgradeCost = 2400f, UpgradesFromAssetName = "MyCPU_T2" },
```

A server running `MyCPU_T1` shows one button, "Upgrade → MyCorp CPU II ($1,200)"; once bought, that
same button switches to point at `MyCPU_T3`. Leave `UpgradesFromAssetName` empty/null for a root
tier (nothing upgrades into it — it's only ever a chassis's starting component or a standalone
item). A tier nothing else names via `UpgradesFromAssetName` is the end of its chain — the button
shows "MAX" there. You can extend the base game's own hardware chains this way too (e.g. link a new
`CPU_T11` onto the base game's `CPU_T10`) by naming the existing asset as `UpgradesFromAssetName`.

## Mainboards, Switches & Towers

*Added in v1.3.0 / API v4.*

The game's hardware layer has three more things mods can extend. None of them needs an asset — you describe them in plain data.

### Mainboards

Every server has a mainboard; it decides what the server can carry. Return your own from `GetMotherboards()`:

```csharp
public class MyMod : IServerAdminMod, IServerAdminModHardware   // second interface, same class
{
    public IEnumerable<ModMotherboardData> GetMotherboards() => new[]
    {
        new ModMotherboardData
        {
            Name = "MyBoard Duo", Generation = 3, Sockets = 2, PcieSlots = 4, RamSlots = 12,
            DriveConnectors = 8, MaxNicMbps = 100000, MinSize = 2, Price = 7500f, PowerCost = 0.04f,
        },
    };
    // ... GetSwitchModels() and the rest of IServerAdminMod
}
```

| Field | Meaning |
|---|---|
| `Generation` (1–3) | Which CPU socket (LGA-1…3) and RAM type (DDR3…5) it takes; newer boards also take older parts |
| `Sockets` (1–8) | How many processors can be installed (cores add up) |
| `PcieSlots` (0–16) | Slots for accelerator cards (GPUs) |
| `RamSlots` (1–64) | How many memory modules can be installed |
| `DriveConnectors` (1–16) | How many drives can be attached (the chassis' bays limit it further) |
| `MaxNicMbps` | Fastest network card the board can drive |
| `MinSize` (1–4) | Smallest chassis size class it fits |
| `Price`, `PowerCost` | Purchase price; power draw per tick (× 1000 = Watts) |

Boards get the next free index, so **saves are bound to the exact mod set** (like any other mod content) — the game already blocks loading a save when a mod is missing.
Mod components follow the generation rules automatically: a component's generation is derived from its `Value`
(≤ 16 → gen 1, ≤ 64 → gen 2, above → gen 3).

### Switch models

`GetSwitchModels()` adds models to the network manager shop:

```csharp
public IEnumerable<ModSwitchModelData> GetSwitchModels() => new[]
{
    new ModSwitchModelData { Name = "MySwitch 48x10G", Ports = 48, PortSpeedMbps = 10000, UplinkMbps = 100000, Price = 14000f, PowerCost = 0.09f },
};
```

A server's link speed is the smaller of its network card and the switch port; the switch's own uplink limits everything behind it.

### Tower servers

A chassis from `GetServers()` becomes a free-standing tower with `IsTower = true`. It stands on the data center floor
(not in a rack) and needs a free floor space. `SizeClass` (1 mini, 2 mid, 3 max, up to 4) decides which mainboards fit,
`DriveBays` (1–8) how many drives it takes:

```csharp
new ModServerData { AssetName = "MyTower", ServerType = "Workhorse Tower", IsTower = true, SizeClass = 3, DriveBays = 6, PurchaseCost = 6500f /* … */ }
```

Mod chassis get their default parts from `DefaultCpuAssetName` / `DefaultRamAssetName` / `DefaultNetworkAssetName` /
`DefaultStorageAssetName` (a name that can't be found falls back to the smallest part of that kind).

---

## Chassis Compatibility

*Added in v1.2.0 / API v3.3.*

By default, a component you return from `GetComponents()` can be installed on any server chassis
(subject to the chassis's own numeric caps — max CPU cores, RAM, etc.). Set
`ModComponentData.CompatibleChassisTypes` to restrict it to specific chassis instead:

```csharp
new ModComponentData
{
    AssetName              = "MyCPU_Elite",
    DisplayName            = "Elite CPU",
    ComponentType          = "CPU",
    Value                  = 16,
    UpgradeCost            = 1200f,
    // Comma-separated chassis AssetNames — NOT the display ServerType label.
    // Empty/null (the default) = compatible with every chassis.
    CompatibleChassisTypes = "Chassis_T2,Chassis_T3",
}
```

The base game's chassis AssetNames are `Chassis_Basic`, `Chassis_T1`, `Chassis_T1_5`, `Chassis_T2`,
`Chassis_Workstation`, `Chassis_T2_5`, and `Chassis_T3`. This field is checked in addition to the
upgrade chain above: if the chain's next tier isn't compatible with a server's specific chassis, the
Upgrade button shows "MAX" there even though the chain itself continues on other chassis.

### Chassis numeric caps

Independently of `CompatibleChassisTypes`, every chassis also enforces a hard numeric ceiling on
`ModComponentData.Value` per component type. If a server's chain-next component exceeds the target
chassis's cap for that type, the Upgrade button shows "MAX" instead — this is not an error and won't
log anything, so if your component's chain "stops early" on some chassis, check this table first:

| Chassis AssetName     | ServerType label | Max CPU (cores) | Max RAM (GB) | Max Network (Mbps) | Max Storage (GB) |
|------------------------|-------------------|-----------------:|--------------:|---------------------:|--------------------:|
| `Chassis_Basic`        | 1U Server          |               32 |             32 |                10,000 |                2,000 |
| `Chassis_T1`           | 1U Server          |               32 |             32 |                10,000 |                2,000 |
| `Chassis_T1_5`         | 1U Server Pro      |               48 |             48 |                25,000 |                3,000 |
| `Chassis_T2`           | 2U Server          |               64 |             64 |                50,000 |                8,000 |
| `Chassis_Workstation`  | 2U Server          |               64 |             64 |                50,000 |                8,000 |
| `Chassis_T2_5`         | 3U Server          |               96 |             96 |               100,000 |               12,000 |
| `Chassis_T3`           | 4U Server Pro      |              128 |          1,024 |               250,000 |               16,000 |

A component only needs to clear the cap of whichever chassis it should actually be installed on —
e.g. `Value = 64` for a RAM component works fine on `Chassis_T3` (cap 1,024) but will never be
reachable on `Chassis_Basic` (cap 32), regardless of what `CompatibleChassisTypes` says.

---

## Repository & Issues

Source code, issue tracker, and release packages:  
**<https://github.com/TitanEch0/ServerAdminSimulator-ModSDK>**
