using System.Collections.Generic;

/// <summary>
/// Implement this interface in your mod DLL.
/// Reference ServerAdminSimulator.ModSDK.dll from your mod project.
///
/// All Get*() methods may return null or an empty collection — the loader
/// handles null-safety. Override only what your mod provides.
///
/// API versions:
///   v1  — Components, Contracts, Racks, Servers, Software, Staff
///   v2  — Achievements, Challenges, RandomEvents
///   v3  — Rivals, LoanOffers, InsurancePolicies, SeasonalEvents,
///          TerminalCommands, NewsEntries, Localization, GameHooks,
///          ZeroDayExploits
/// </summary>
public interface IServerAdminMod
{
    /// <summary>Returns the mod metadata shown in the in-game Mod Manager.</summary>
    ModManifest GetManifest();

    /// <summary>Returns CPU, RAM and Network component cards added by this mod.</summary>
    IEnumerable<ModComponentData>         GetComponents();
    /// <summary>Returns client contracts injected into the contract board.</summary>
    IEnumerable<ModContractData>          GetContracts();
    /// <summary>Returns server rack types added by this mod.</summary>
    IEnumerable<ModRackData>              GetRacks();
    /// <summary>Returns server chassis types added by this mod.</summary>
    IEnumerable<ModServerData>            GetServers();
    /// <summary>Returns server software and hacking tools added by this mod.</summary>
    IEnumerable<ModSoftwareData>          GetSoftware();
    /// <summary>Returns hireable staff members added by this mod.</summary>
    IEnumerable<ModStaffData>             GetStaff();

    /// <summary>Returns custom achievements added by this mod.</summary>
    IEnumerable<ModAchievementData>       GetAchievements();
    /// <summary>Returns daily and weekly challenges added by this mod.</summary>
    IEnumerable<ModChallengeData>         GetChallenges();
    /// <summary>Returns random in-game events added by this mod.</summary>
    IEnumerable<ModRandomEventData>       GetRandomEvents();

    /// <summary>Returns rival companies added to the market by this mod.</summary>
    IEnumerable<ModRivalData>             GetRivals();
    /// <summary>Returns loan offers added to the Finance panel by this mod.</summary>
    IEnumerable<ModLoanOfferData>         GetLoanOffers();
    /// <summary>Returns insurance policies added to the Finance panel by this mod.</summary>
    IEnumerable<ModInsurancePolicyData>   GetInsurancePolicies();
    /// <summary>Returns seasonal events (month-based triggers) added by this mod.</summary>
    IEnumerable<ModSeasonalEventData>     GetSeasonalEvents();
    /// <summary>Returns custom terminal commands registered by this mod.</summary>
    IEnumerable<ModTerminalCommandData>   GetTerminalCommands();
    /// <summary>Returns headlines added to the in-game news ticker pool.</summary>
    IEnumerable<ModNewsEntryData>         GetNewsEntries();
    /// <summary>Returns Zero-Day exploits added to the hacking market by this mod.</summary>
    IEnumerable<ModZeroDayData>           GetZeroDayExploits();

    /// <summary>
    /// Returns localisation strings for the given language code ("de", "en", "fr", …).
    /// Called once per supported language when the mod loads.
    /// Return null or an empty collection to skip localisation for this language.
    /// </summary>
    IEnumerable<ModLocalizationEntry>     GetLocalization(string language);

    /// <summary>
    /// Returns a <see cref="ModGameHooks"/> instance to subscribe to game lifecycle events.
    /// Return null if this mod has no lifecycle hooks.
    /// </summary>
    ModGameHooks GetHooks();
}
