using System.Diagnostics.CodeAnalysis;

namespace NetGameState.Level;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum SubZone
{
    Unknown = -1,
    
    Any = 0,
    
    // Shore 10-29
    Shore_Default = 10,
    Shore_SnakeBeach = 11,
    Shore_RedBeach = 12,
    Shore_BlueBeach = 13,
    Shore_JellyHell = 14,
    Shore_BlackSand = 15,
    
    // Tropics 30-49
    Tropics_Default = 30,
    Tropics_Lava = 31,
    Tropics_Pillars = 32,
    Tropics_Thorny = 33,
    Tropics_Bombs = 34,
    Tropics_Ivy = 35,
    Tropics_SkyJungle = 36,
    
    // Alpine 50-69
    Alpine_Default = 50,
    Alpine_Lava = 51,
    Alpine_Spiky = 52,
    Alpine_GeyserHell = 53,
    
    // Mesa 70-89
    Mesa_Default = 70,
    
    // Caldera 90-109
    Caldera_Default = 90,
    
    // Kiln 110-129
    Kiln_Default = 110,
    
    // Peak 130-149
    Peak_Default = 130
}

public enum ShoreZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Shore_Default,
    SnakeBeach = SubZone.Shore_SnakeBeach,
    RedBeach = SubZone.Shore_RedBeach,
    BlueBeach = SubZone.Shore_BlueBeach,
    JellyHell = SubZone.Shore_JellyHell,
    BlackSand = SubZone.Shore_BlackSand,
}

public enum TropicsZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Tropics_Default,
    Lava = SubZone.Tropics_Lava,
    Pillars = SubZone.Tropics_Pillars,
    Thorny = SubZone.Tropics_Thorny,
    Bombs = SubZone.Tropics_Bombs,
    Ivy = SubZone.Tropics_Ivy,
    SkyJungle = SubZone.Tropics_SkyJungle,
}

public enum AlpineZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Alpine_Default,
    Lava = SubZone.Alpine_Lava,
    Spiky = SubZone.Alpine_Spiky,
    GeyserHell = SubZone.Alpine_GeyserHell
}

public enum MesaZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Mesa_Default
}

public enum CalderaZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Caldera_Default
}

public enum KilnZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Kiln_Default
}

public enum PeakZone
{
    Unknown = SubZone.Unknown,
    Any = SubZone.Any,
    Default = SubZone.Peak_Default
}