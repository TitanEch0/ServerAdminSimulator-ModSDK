using System;
using System.Collections.Generic;

/// <summary>
/// Reference implementation of <see cref="IServerAdminMod"/> for Server-Admin Simulator.
///
/// This file demonstrates every Get*() method and every lifecycle hook.
/// Use it as a copy-paste starting point for your own mod.
///
/// Build output: ExampleMod.dll
/// Drop it into: %APPDATA%\..\LocalLow\Ice-Phoenix\ServerAdminSimulator\Mods\
/// </summary>
public class ExampleMod : IServerAdminMod
{
    // ─────────────────────────────────────────────────────────────────────────
    //  Manifest
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the metadata block shown in the in-game Mod Manager.
    /// Always fill every field — missing data looks unprofessional on the
    /// mod browser.
    /// </summary>
    public ModManifest GetManifest() => new ModManifest
    {
        Name                  = "Example Mod",
        Version               = "1.0.0",
        Author                = "YourNameHere",
        Description           = "Demonstrates every SDK feature. Use as a starting template.",
        Website               = "https://github.com/TitanEch0/ServerAdminSimulator-ModSDK",
        Discord               = "discord.gg/example",
        MinGameVersion        = "1.0.0-beta",
        Dependencies          = new string[0],
        Tags                  = new string[] { "content", "example", "sdk" },
        RequiredForMultiplayer = false,
    };

    // ─────────────────────────────────────────────────────────────────────────
    //  API v1 — Hardware & Contracts
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// CPU / RAM / Network upgrade cards added by this mod.
    ///
    /// "ExCPU_T1" is an 8-core upgrade card that slots into any T1 CPU socket.
    /// Setting <see cref="ModComponentData.UpgradesFromAssetName"/> chains it to
    /// the base game CPU_T1 so players see it as the next step in the upgrade tree.
    /// </summary>
    public IEnumerable<ModComponentData> GetComponents() => new[]
    {
        new ModComponentData
        {
            AssetName             = "ExCPU_T1",
            DisplayName           = "ExCore 8 Pro",
            ComponentType         = "CPU",
            Value                 = 8,
            PowerCostPerTick      = 0.04f,
            UpgradeCost           = 750f,
            UpgradesFromAssetName = "CPU_T1",
        },
    };

    /// <summary>
    /// Client contracts injected into the contract board.
    ///
    /// "ExContract_DataCenter" is a long-running AI-Training contract from the
    /// fictional NovaMind Research institute. It demands beefy specs and a solid
    /// reputation score (≥ 20) before appearing.
    /// </summary>
    public IEnumerable<ModContractData> GetContracts() => new[]
    {
        new ModContractData
        {
            AssetName             = "ExContract_DataCenter",
            ClientName            = "NovaMind Research",
            Description           = "loc:contract_datacenter_desc",
            IncomePerTick         = 3.50f,
            RequiredCpuCores      = 5,
            RequiredRamGB         = 16,
            RequiredBandwidthMbps = 500,
            DurationTicks         = 300,
            MinRepScore           = 20f,
            Category              = "AITraining",
            Personality           = "Tough",
            IsLoyal               = true,
            IsVolatile            = false,
            SlaMinUptime          = 98f,
        },
    };

    /// <summary>
    /// Server rack chassis types added by this mod.
    ///
    /// "ExRack_Modded" offers 12 slots (vs. the base 4-slot rack) and an
    /// improved cooling multiplier (0.85 = 15 % cooler) at the cost of a
    /// higher purchase price.
    /// </summary>
    public IEnumerable<ModRackData> GetRacks() => new[]
    {
        new ModRackData
        {
            AssetName         = "ExRack_Modded",
            RackType          = "Modded",
            Description       = "A 12U community-built rack with extra airflow panels.",
            MaxSlots          = 12,
            PurchaseCost      = 4500f,
            CoolingMultiplier = 0.85f,
            PowerEfficiency   = 1.05f,
        },
    };

    /// <summary>
    /// No custom server chassis in this example.
    /// Return <see cref="Array.Empty{T}"/> for any method your mod does not use.
    /// </summary>
    public IEnumerable<ModServerData> GetServers() => Array.Empty<ModServerData>();

    /// <summary>
    /// Server software and hacking tools added by this mod.
    ///
    /// "ExSoftware_BoostOS" is an optimised OS kernel that passively increases
    /// all contract income by 8 % while installed on a server.
    /// </summary>
    public IEnumerable<ModSoftwareData> GetSoftware() => new[]
    {
        new ModSoftwareData
        {
            AssetName             = "ExSoftware_BoostOS",
            SoftwareName          = "BoostOS 1.0",
            Description           = "Optimised kernel — boosts contract revenue by 8 %.",
            LineId                = "BoostOS",
            Category              = "ServerSoftware",
            Effect                = "ContractIncome",
            EffectValue           = 0.08f,
            PurchaseCost          = 1200f,
            UpgradeCost           = 2000f,
            UpgradesFromAssetName = null,
        },
    };

    /// <summary>
    /// Hireable staff members added by this mod.
    ///
    /// "ExStaff_DataSci" (Maya Frost) is a Tier-1 Cooling Engineer.
    /// Her EffectValue of 0.12 means she reduces server temperature by 12 %.
    /// MaxHireable = 1 prevents the player from stacking multiple copies.
    /// </summary>
    public IEnumerable<ModStaffData> GetStaff() => new[]
    {
        new ModStaffData
        {
            AssetName             = "ExStaff_DataSci",
            StaffName             = "Maya Frost",
            Description           = "loc:staff_datasci_desc",
            LineId                = "DataSci",
            Tier                  = 1,
            Role                  = "CoolingEngineer",
            HireCost              = 1500f,
            SalaryPerTick         = 0.08f,
            EffectValue           = 0.12f,
            MaxHireable           = 1,
            UpgradesFromAssetName = null,
        },
    };

    // ─────────────────────────────────────────────────────────────────────────
    //  API v2 — Meta Content
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Custom achievements unlocked through normal gameplay.
    ///
    /// "ex_first_mod" fires once the ModLoader counts at least one loaded mod
    /// (ContextField = "ModsLoaded", Threshold = 1).
    /// </summary>
    public IEnumerable<ModAchievementData> GetAchievements() => new[]
    {
        new ModAchievementData
        {
            AssetName    = "ex_first_mod",
            Icon         = "🛠",
            Title        = "Modded!",
            Description  = "Loaded your first community mod.",
            Category     = "meta",
            ContextField = "ModsLoaded",
            Threshold    = 1f,
        },
    };

    /// <summary>No custom challenges in this example.</summary>
    public IEnumerable<ModChallengeData> GetChallenges() => Array.Empty<ModChallengeData>();

    /// <summary>
    /// Random in-game events added by this mod.
    ///
    /// "ex_bonus_event" is a positive Info-level event that grants +$200 income
    /// and lasts 30 ticks. Weight = 5 makes it appear roughly twice as often as
    /// the typical base-game event (Weight = 3).
    ///
    /// Set <see cref="ModRandomEventData.TriggerAction"/> to a lambda if you
    /// need custom side-effects when the event fires.
    /// </summary>
    public IEnumerable<ModRandomEventData> GetRandomEvents() => new[]
    {
        new ModRandomEventData
        {
            AssetName      = "ex_bonus_event",
            TitleKey       = "loc:event_bonus_title",
            DescriptionKey = "loc:event_bonus_desc",
            Title          = "Community Bonus",
            Description    = "A generous benefactor grants your datacenter a temporary income boost!",
            Weight         = 5,
            Severity       = "Info",
            IncomeBonus    = 200f,
            IncomeMult     = 0f,
            DurationTicks  = 30,
            TriggerAction  = null,
        },
    };

    // ─────────────────────────────────────────────────────────────────────────
    //  API v3 — Extended Content
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>No rival companies in this example.</summary>
    public IEnumerable<ModRivalData> GetRivals() => Array.Empty<ModRivalData>();

    /// <summary>No custom loan offers in this example.</summary>
    public IEnumerable<ModLoanOfferData> GetLoanOffers() => Array.Empty<ModLoanOfferData>();

    /// <summary>No custom insurance policies in this example.</summary>
    public IEnumerable<ModInsurancePolicyData> GetInsurancePolicies() => Array.Empty<ModInsurancePolicyData>();

    /// <summary>No seasonal events in this example.</summary>
    public IEnumerable<ModSeasonalEventData> GetSeasonalEvents() => Array.Empty<ModSeasonalEventData>();

    /// <summary>
    /// Custom terminal commands accessible via the in-game terminal.
    ///
    /// Typing "exinfo" in the terminal invokes the <see cref="ModTerminalCommandData.Handler"/>
    /// delegate and prints the mod name and version to the terminal output.
    /// The <paramref name="args"/> array contains any whitespace-separated tokens
    /// after the command word.
    /// </summary>
    public IEnumerable<ModTerminalCommandData> GetTerminalCommands() => new[]
    {
        new ModTerminalCommandData
        {
            Command     = "exinfo",
            Description = "Displays Example Mod name and version.",
            Usage       = "exinfo",
            Handler     = (args, gc) =>
            {
                ModManifest m = GetManifest();
                return string.Format("{0} v{1} by {2}", m.Name, m.Version, m.Author);
            },
        },
    };

    /// <summary>No custom news ticker entries in this example.</summary>
    public IEnumerable<ModNewsEntryData> GetNewsEntries() => Array.Empty<ModNewsEntryData>();

    /// <summary>
    /// Zero-day exploits available in the hacking minigame.
    ///
    /// "ExZeroDay_ByteFlood" raises hack success chance by 30 % and can be
    /// used 3 times before expiring. Priced at $2 500 — accessible mid-game.
    /// </summary>
    public IEnumerable<ModZeroDayData> GetZeroDayExploits() => new[]
    {
        new ModZeroDayData
        {
            Name                = "ByteFlood",
            Description         = "Floods target buffers with crafted packets, creating a brief exploitation window.",
            TargetDifficultyMin = 2,
            TargetDifficultyMax = 5,
            SuccessBonus        = 0.30f,
            Price               = 2500f,
            Uses                = 3,
        },
    };

    /// <summary>
    /// Localisation strings contributed by this mod.
    ///
    /// Keys prefixed with "loc:" in other data fields are resolved against these
    /// entries at runtime. Provide at minimum "en" as a fallback language.
    /// Supported here: "en" and "de".
    /// </summary>
    public IEnumerable<ModLocalizationEntry> GetLocalization(string language)
    {
        switch (language)
        {
            case "en":
                return new[]
                {
                    new ModLocalizationEntry
                    {
                        Language = "en",
                        Key      = "contract_datacenter_desc",
                        Value    = "Provide sustained AI training compute for NovaMind Research's neural-network clusters.",
                    },
                    new ModLocalizationEntry
                    {
                        Language = "en",
                        Key      = "staff_datasci_desc",
                        Value    = "Expert cooling engineer. Reduces server temperatures and prevents thermal throttling.",
                    },
                    new ModLocalizationEntry
                    {
                        Language = "en",
                        Key      = "event_bonus_desc",
                        Value    = "An anonymous donor transfers funds to support independent datacenter operators.",
                    },
                };

            case "de":
                return new[]
                {
                    new ModLocalizationEntry
                    {
                        Language = "de",
                        Key      = "contract_datacenter_desc",
                        Value    = "Stelle NovaMind Research dauerhaft Rechenleistung für KI-Trainings-Cluster bereit.",
                    },
                    new ModLocalizationEntry
                    {
                        Language = "de",
                        Key      = "staff_datasci_desc",
                        Value    = "Erfahrener Kühlungsingenieur. Senkt Servertemperaturen und verhindert thermisches Drosseln.",
                    },
                    new ModLocalizationEntry
                    {
                        Language = "de",
                        Key      = "event_bonus_desc",
                        Value    = "Ein anonymer Spender überweist Mittel zur Unterstützung unabhängiger Rechenzentrumsbetreiber.",
                    },
                };

            default:
                return Array.Empty<ModLocalizationEntry>();
        }
    }

    /// <summary>
    /// Returns the lifecycle hook handler for this mod.
    /// The game calls methods on the returned object at key moments in the
    /// simulation loop.
    /// </summary>
    public ModGameHooks GetHooks() => new ExampleHooks();

    // ─────────────────────────────────────────────────────────────────────────
    //  Inner class: Lifecycle hooks
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Lifecycle hooks for ExampleMod.
    ///
    /// Inherit from <see cref="ModGameHooks"/> and override the methods you need.
    /// Unoverridden methods are no-ops, so you pay zero cost for unused hooks.
    /// </summary>
    public class ExampleHooks : ModGameHooks
    {
        /// <summary>
        /// Fires once when the game session begins (after save-load or new game).
        /// Use this to initialise any mod state or print a welcome message.
        /// </summary>
        public override void OnGameStart(GameController gc)
        {
            gc.Logger.Log(
                "[ExampleMod] Mod loaded and active.",
                LogLevel.Info,
                gc.TimeSystem.GetFormattedTime());
        }

        /// <summary>
        /// Fires every simulation tick.
        /// This hook awards a $50.00 bonus every 60 ticks — roughly once per
        /// in-game minute — as a small passive income stream for testing the
        /// Economy API.
        /// </summary>
        public override void OnTick(GameController gc, int tick)
        {
            if (tick % 60 == 0)
            {
                gc.Economy.AddIncome(50f);
                gc.Logger.Log(
                    "[ExampleMod] 60-tick bonus: +$50.00",
                    LogLevel.Info,
                    gc.TimeSystem.GetFormattedTime());
            }
        }

        /// <summary>
        /// Fires when the player successfully completes a contract.
        /// Here we simply log the event; a real mod might grant bonus income,
        /// track statistics, or unlock additional content.
        /// </summary>
        public override void OnContractCompleted(ContractConfig contract, GameController gc)
        {
            gc.Logger.Log(
                string.Format("[ExampleMod] Contract '{0}' completed — well done!", contract.clientName),
                LogLevel.Info,
                gc.TimeSystem.GetFormattedTime());
        }
    }
}
