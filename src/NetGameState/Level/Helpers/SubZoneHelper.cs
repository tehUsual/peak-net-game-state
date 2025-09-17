using System;
using System.Linq;

namespace NetGameState.Level.Helpers;

public static class SubZoneHelper
{
    public static readonly ShoreZone[] ShoreSubZones = Enum.GetValues(typeof(ShoreZone))
        .Cast<ShoreZone>()
        .Where(v => v != ShoreZone.Unknown && v != ShoreZone.Any)
        .ToArray();
    
    public static readonly TropicsZone[] TropicsSubZones = Enum.GetValues(typeof(TropicsZone))
        .Cast<TropicsZone>()
        .Where(v => v != TropicsZone.Unknown && v != TropicsZone.Any)
        .ToArray();
    
    public static readonly AlpineZone[] AlpineSubZones = Enum.GetValues(typeof(AlpineZone))
        .Cast<AlpineZone>()
        .Where(v => v != AlpineZone.Unknown && v != AlpineZone.Any)
        .ToArray();
}