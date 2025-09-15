using System.Diagnostics.CodeAnalysis;

namespace NetGameState.Level;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Zone
{
    Unknown = -1,
    Shore,
    Tropics,
    Alpine,
    Mesa,
    Caldera,
    Kiln,
    Peak
}