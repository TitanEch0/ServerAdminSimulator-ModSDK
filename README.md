# Server-Admin Simulator — Modding SDK

Official SDK for creating mods for **Server-Admin Simulator** by Ice-Phoenix.  
Reference `ServerAdminSimulator.ModSDK.dll` in your mod project, implement `IServerAdminMod`,
and drop the compiled DLL into the game's Mods folder.

> **Current API version:** v3.2  
> **MinGameVersion:** `1.1.4`

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
        MinGameVersion = "1.1.4",
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
| `GetComponents()` | v1 | CPU, RAM, and Network upgrade cards |
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
