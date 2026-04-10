using System.Diagnostics.CodeAnalysis;

namespace NetGameState.Types;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum NgsBiomeVariant
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
    
    // Tropics 30-39
    Tropics_Default = 30,
    Tropics_Lava = 31,
    Tropics_Pillars = 32,
    Tropics_Thorny = 33,
    Tropics_Bombs = 34,
    Tropics_Ivy = 35,
    Tropics_SkyJungle = 36,
    
    // Roots 40-49
    Roots_Default = 40,
    
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
