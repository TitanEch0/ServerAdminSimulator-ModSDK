# Changelog

All notable changes to the Server-Admin Simulator Modding SDK are documented here.

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
