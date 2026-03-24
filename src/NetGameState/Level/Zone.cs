using System.Diagnostics.CodeAnalysis;

namespace NetGameState.Level;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Zone
{
    Unknown = -1,
    Any = 0,
    
    // Biome 1
    Shore,
    
    // Biome 2
    Tropics,
    Roots,
    
    // Biome 3
    Alpine,
    Mesa,
    
    // Biome 4
    Caldera,
    Kiln,
    Peak
}