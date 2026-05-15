# Server-Admin Simulator — Modding SDK

Official SDK for creating mods for **Server-Admin Simulator** by Ice-Phoenix.  
Reference `ServerAdminSimulator.ModSDK.dll` in your mod project, implement `IServerAdminMod`,
and drop the compiled DLL into the game's Mods folder.

> **Current API version:** v3  
> **MinGameVersion:** `1.0.0-beta`

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
        MinGameVersion = "1.0.0-beta",
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
%APPDATA%\..\LocalLow\Ice-Phoenix\ServerAdminSimulator\Mods\
```

The game's ModLoader scans this folder on startup and loads every valid `IServerAdminMod` implementation it finds.

---

## API Reference

### Content Methods (`IServerAdminMod`)

| Method | API | Description |
|---|:---:|---|
| `GetManifest()` | v1 | Mod metadata — name, version, author, dependencies |
| `GetComponents()` | v1 | CPU, RAM, and Network upgrade cards |
| `GetContracts()` | v1 | Client contracts shown on the contract board |
| `GetRacks()` | v1 | Server rack chassis types |
| `GetServers()` | v1 | Server form factors / chassis |
| `GetSoftware()` | v1 | Server software and hacking tools |
| `GetStaff()` | v1 | Hireable staff members |
| `GetAchievements()` | v2 | Custom unlockable achievements |
| `GetChallenges()` | v2 | Timed challenge objectives |
| `GetRandomEvents()` | v2 | Events that fire randomly during gameplay |
| `GetRivals()` | v3 | Competitor companies with market share |
| `GetLoanOffers()` | v3 | Custom bank loan products |
| `GetInsurancePolicies()` | v3 | Custom insurance policies |
| `GetSeasonalEvents()` | v3 | Month-based income modifiers |
| `GetTerminalCommands()` | v3 | Custom in-game terminal commands |
| `GetNewsEntries()` | v3 | Breaking-news ticker headlines |
| `GetZeroDayExploits()` | v3 | Special hacking tools with limited uses |
| `GetLocalization(lang)` | v3 | Translations keyed by language code (`"en"`, `"de"`, …) |
| `GetHooks()` | v3 | Returns your `ModGameHooks` subclass |

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
| `OnComponentUpgraded(componentType, gc)` | A CPU / RAM / Network card is upgraded |
| `OnReputationTierChanged(newTier, gc)` | Player's reputation tier changes |
| `OnDatacenterExpanded(newLevel, gc)` | Datacenter capacity is expanded |
| `OnGameOver(gc)` | Game-over condition is reached |
| `OnGameSaved(slot)` | Game is saved to a slot |
| `OnGameLoaded(slot)` | A save file is loaded |

---

## Repository & Issues

Source code, issue tracker, and release packages:  
**<https://github.com/TitanEch0/ServerAdminSimulator-ModSDK>**
