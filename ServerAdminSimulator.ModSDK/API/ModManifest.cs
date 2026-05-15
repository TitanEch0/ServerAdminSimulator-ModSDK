/// <summary>Metadata for a mod. Fill all fields in your <see cref="IServerAdminMod"/> implementation.</summary>
public class ModManifest
{
    /// <summary>Display name shown in the in-game Mod Manager.</summary>
    public string   Name                  = "Unnamed Mod";
    /// <summary>Semantic version string (e.g. "1.0.0").</summary>
    public string   Version               = "1.0.0";
    /// <summary>Name or alias of the mod author.</summary>
    public string   Author                = "Unknown";
    /// <summary>Short description shown below the mod name in the Mod Manager.</summary>
    public string   Description           = "";
    /// <summary>Optional URL shown as a clickable button in the Mod Manager UI.</summary>
    public string   Website               = "";
    /// <summary>Optional Discord invite shown as a clickable button in the Mod Manager UI.</summary>
    public string   Discord               = "";
    /// <summary>Minimum game version required (e.g. "1.0.0-beta"). Leave empty to accept any version.</summary>
    public string   MinGameVersion        = "";
    /// <summary>Names of other mods that must be loaded before this one. The loader warns on missing dependencies.</summary>
    public string[] Dependencies          = new string[0];
    /// <summary>Descriptive tags shown in the mod list (e.g. "hardware", "economy", "challenge").</summary>
    public string[] Tags                  = new string[0];
    /// <summary>When true, all multiplayer session players must have this mod with the same version.</summary>
    public bool     RequiredForMultiplayer = true;
    /// <summary>MD5 hash of the mod DLL. Set automatically by ModLoader — do not set manually.</summary>
    public string   Checksum              = "";
}
