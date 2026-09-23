# Changelog

All notable changes to the Server-Admin Simulator Modding SDK are documented here.

---

## v1.2.0 (2026-09-23)

Adds per-chassis compatibility restrictions for hardware components, and replaces the single
linear upgrade chain with a full choice of every currently compatible component (game update
covering server upgrade choice).

### API v1 — Core Content
- `ModComponentData.CompatibleChassisTypes` — comma-separated list of chassis AssetNames this
  component may be installed in. Null/empty = compatible with every chassis (fully backward
  compatible with existing mods, which leave this unset).
- Upgrading a server component now lets the player pick from every component of that type
  compatible with the server's chassis (by `CompatibleChassisTypes` and the chassis's numeric
  caps), not just the next tier in `UpgradesFromAssetName`. That field is still supported for
  display/ordering purposes.

---

## v1.1.0 (2026-09-21)

Adds SDK support for the game's Storage component type and the new long-duration hosting contract categories (game update 1.1.8).

### API v1 — Core Content
- `ModComponentData.ComponentType` now also accepts `"Storage"`
- `ModComponentData.Vendor` — fictional hardware vendor/brand shown next to the display name (purely cosmetic)
- `ModContractData.Category` now also accepts `"WebsiteHosting"` and `"SaaSHosting"`
- `ModContractData.RequiredStorageGB` — minimum storage in GB the assigned server must provide
- `ModServerData.MaxStorageGB` — maximum storage in GB a chassis supports (0 = no cap)
- `ModServerData.DefaultStorageAssetName` — default Storage component pre-installed on a chassis

### Infrastructure
- `ContractConfig` stub — added `requiredStorageGB` field and `WebsiteHosting`/`SaaSHosting` to the `ContractCategory` enum
- `GameController` stub — added `SwapStorage(server)` and `MoveContract(contract, newServer)`
- `RandomEventSystem` stub — added `HasActiveStorageFault(serverId)`
- `ServerData` stub — added `MaxStorageGB` and `StorageUsagePercent`

---

## v1.0.0 (2026-05-15)

Initial public release.

### API v1 — Core Content
- `GetComponents()` — CPU, RAM, and Network upgrade cards with upgrade-chain support
- `GetContracts()` — Custom client contracts with SLA, loyalty, and volatility flags
- `GetRacks()` — Server rack chassis with cooling multiplier and power efficiency
- `GetServers()` — Server form factors with configurable slot limits and power budget
- `GetSoftware()` — Server software (income, temperature) and hacking tools (success rate, DDoS)
- `GetStaff()` — Hireable staff across four roles: SystemAdmin, SecurityExpert, NetworkEngineer, CoolingEngineer

### API v2 — Meta Content
- `GetAchievements()` — Custom achievements with icon, category, and numeric threshold
- `GetChallenges()` — Timed objectives with money and reputation rewards
- `GetRandomEvents()` — In-game events with weight, severity, income modifiers, and optional `TriggerAction` delegate

### API v3 — Extended Content
- `GetRivals()` — Competitor companies with aggressiveness, market share, and acquisition cost
- `GetLoanOffers()` — Custom loan products with interest-per-tick and reputation gate
- `GetInsurancePolicies()` — Coverage types: DDoS, Hardware, PowerOutage, Full
- `GetSeasonalEvents()` — Month-based income boosts with duration and trigger chance
- `GetTerminalCommands()` — Custom `/terminal` commands with `Func<string[], GameController, string>` handler
- `GetNewsEntries()` — Breaking-news ticker headlines
- `GetZeroDayExploits()` — Special hacking tools with difficulty range, success bonus, and limited uses
- `GetLocalization(lang)` — Multi-language string table (key/value pairs per language code)
- `GetHooks()` — Full lifecycle hook system via `ModGameHooks` base class (14 overridable entry points)

### Infrastructure
- `ModManifest` — Metadata block: name, version, author, description, website, Discord, MinGameVersion, dependency list, tags, multiplayer flag
- `GameController` stub — Exposes Economy, Simulation, Reputation, Staff, Software, Events, TimeSystem, Logger
- `EconomyModel` stub — `AddIncome`, `AddExpense`, `TrySpend`, read-only balance and tick totals
- `ServerSimulationModel` stub — `Servers`, `Racks`, `ActiveContracts`, `PendingContracts`
- `ReputationSystem` stub — Score, TierLabel, ContractsDone, RegisterSuccess/Failure
- `TimeSystem` stub — CurrentTick, TimeScale, IsRunning, GetFormattedTime()
- `EventLogger` stub — `Log(message, level, time)`
- `ContractConfig` stub — Full ScriptableObject field set including SLA and personality enums
- `RandomEventSystem` stub — IncomeMultiplier read access
- `ExampleMod` project — Reference implementation covering all 19 interface methods and 3 lifecycle hooks
