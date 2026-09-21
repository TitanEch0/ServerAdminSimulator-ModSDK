/// <summary>Stub of ContractConfig ScriptableObject.</summary>
public class ContractConfig
{
    /// <summary>Display name of the client company.</summary>
    public string clientName;
    /// <summary>Short description shown on the contract card.</summary>
    public string description;
    /// <summary>Base income awarded per simulation tick.</summary>
    public float  incomePerTick;
    /// <summary>Minimum CPU cores the assigned server must provide.</summary>
    public int    requiredCpuCores;
    /// <summary>Minimum RAM in GB the assigned server must provide.</summary>
    public int    requiredRamGB;
    /// <summary>Minimum bandwidth in Mbps the assigned server must provide.</summary>
    public int    requiredBandwidthMbps;
    /// <summary>Total contract duration in simulation ticks.</summary>
    public int    durationTicks;
    /// <summary>Minimum reputation score required before this contract appears.</summary>
    public float  minRepScore;
    /// <summary>Required uptime percentage for SLA compliance (0 = no SLA).</summary>
    public float  slaUptimePercent;
    /// <summary>Multiplier applied to the SLA penalty on violation.</summary>
    public float  slaPenaltyMultiplier;
    /// <summary>"General","AITraining","CryptoMining","VideoStreaming","Government"</summary>
    public ContractCategory category;
    /// <summary>"Standard","Tough","Friendly","Strict","Desperate"</summary>
    public ContractPersonality personality;
    /// <summary>Minimum storage in GB the assigned server must provide (0 = no SLA on storage).</summary>
    public int requiredStorageGB;
}

/// <summary>Contract workload category affecting income scaling and SLA behaviour.</summary>
public enum ContractCategory
{
    /// <summary>General-purpose hosting workload.</summary>
    General,
    /// <summary>Machine-learning training jobs with sustained high compute demand.</summary>
    AITraining,
    /// <summary>Cryptocurrency mining with volatile income per tick.</summary>
    CryptoMining,
    /// <summary>Live video transcoding with strict bandwidth requirements.</summary>
    VideoStreaming,
    /// <summary>Government contracts with strict uptime SLAs and high penalties.</summary>
    Government,
    /// <summary>Long-duration, storage-heavy website hosting.</summary>
    WebsiteHosting,
    /// <summary>Long-duration SaaS hosting, balanced CPU/RAM with heavy storage.</summary>
    SaaSHosting
}

/// <summary>Client personality affecting negotiation chance and cancellation tolerance.</summary>
public enum ContractPersonality
{
    /// <summary>Balanced negotiation behaviour.</summary>
    Standard,
    /// <summary>Drives a hard bargain — lower negotiation success chance.</summary>
    Tough,
    /// <summary>Flexible terms — higher negotiation success chance.</summary>
    Friendly,
    /// <summary>Demands precise SLA adherence — cancels on the first violation.</summary>
    Strict,
    /// <summary>Willing to accept almost any terms due to urgent need.</summary>
    Desperate
}
