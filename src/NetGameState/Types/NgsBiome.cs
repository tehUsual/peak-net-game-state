using System.Diagnostics.CodeAnalysis;

namespace NetGameState.Types;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum NgsBiome
{
    Unknown = -1,
    Any = 0,
    
    // Segment 1
    Shore,
    
    // Segment 2
    Tropics,
    Roots,
    
    // Segment 3
    Alpine,
    Mesa,
    
    // Segment 4
    Caldera,
    
    // Segment 5
    Kiln,
    
    // Segment 6
    Peak
}