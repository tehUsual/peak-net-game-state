using System;
using System.Collections.Generic;
using ConsoleTools;
using NetGameState.Events;
using NetGameState.LevelStructure;
using NetGameState.Logging;
using UnityEngine;

namespace NetGameState.Level.Helpers;

public static class SegmentMapper
{
    public static Chapter GetChapterFromSegment(Segment segment) => segment switch
    {
        Segment.Beach => Chapter.One,
        Segment.Tropics => Chapter.Two,
        Segment.Alpine => Chapter.Three,
        Segment.Caldera => Chapter.Four,
        Segment.TheKiln => Chapter.Five,
        Segment.Peak => Chapter.Six,
        _ => Chapter.Unknown,
    };

    public static bool TryGetSegmentFromChapter(Chapter chapter, out Segment? segment)
    {
        switch (chapter)
        {
            case Chapter.One: segment = Segment.Beach; return true;
            case Chapter.Two: segment = Segment.Tropics; return true;
            case Chapter.Three: segment = Segment.Alpine; return true;
            case Chapter.Four: segment = Segment.Caldera; return true;
            case Chapter.Five: segment = Segment.TheKiln; return true;
            case Chapter.Six: segment = Segment.Peak; return true;
            
            case Chapter.Unknown:
            default: segment = null; return false;
        }
    }

    public static Zone GetZoneFromChapter(Chapter chapter)
    {
         switch (chapter)
         {
             case Chapter.One:
                 return Zone.Shore;
             case Chapter.Two:
                 return Zone.Tropics;
             case Chapter.Three:
                 if (MapObjectRefs.BioAlpine?.gameObject.activeSelf ?? false)
                     return Zone.Alpine;
                 if (MapObjectRefs.BioMesa?.gameObject.activeSelf ?? false)
                     return Zone.Mesa;
                 break;
             case Chapter.Four:
                 return Zone.Caldera;
             case Chapter.Five:
                 return Zone.Kiln;
             case Chapter.Six:
                 return Zone.Peak;
         }

         return Zone.Unknown;
    }

    public static Chapter GetChapterFromZone(Zone zone) => zone switch
    {
        Zone.Shore => Chapter.One,
        Zone.Tropics => Chapter.Two,
        Zone.Alpine => Chapter.Three,
        Zone.Mesa => Chapter.Three,
        Zone.Caldera => Chapter.Four,
        Zone.Kiln => Chapter.Five,
        Zone.Peak => Chapter.Six,
        _ => Chapter.Unknown
    };

    public static bool TryGetBiomeTypeFromZone(Zone zone, out Biome.BiomeType? biomeType)
    {
        switch (zone)
        {
            case Zone.Shore: biomeType = Biome.BiomeType.Shore; return true;
            case Zone.Tropics: biomeType = Biome.BiomeType.Tropics; return true;
            case Zone.Alpine: biomeType = Biome.BiomeType.Alpine; return true;
            case Zone.Mesa: biomeType = Biome.BiomeType.Mesa; return true;
            case Zone.Caldera: biomeType = Biome.BiomeType.Volcano; return true;
            case Zone.Kiln: biomeType = Biome.BiomeType.Volcano; return true;
            case Zone.Peak: biomeType = Biome.BiomeType.Peak; return true;
            default: biomeType = null; return false;
        }
    }


    private static SubZone GetActiveSubZone<T>(Transform? root, T[] subZones, string zoneName)
    {
        if (ReferenceEquals(root, null))
        {
            LogProvider.Log?.LogColorW($"Could not find '{zoneName}' Segment");
            return SubZone.Unknown;
        }

        foreach (var subZone in subZones)
        {
            if (root.Find(subZone?.ToString())?.gameObject.activeSelf ?? false)
                return (SubZone)Convert.ToInt32(subZone);
        }
        
        LogProvider.Log?.LogColorW($"Could not find sub-zone for '{zoneName}'");
        return SubZone.Unknown;
    }

    public static SubZone GetSubZoneFromZone(Zone zone)
    {
        // TODO: Add cache (use segment manager)
        return zone switch
        {
            Zone.Shore => GetActiveSubZone(MapObjectRefs.SegShore, SubZoneHelper.ShoreSubZones, "Shore"),
            Zone.Tropics => GetActiveSubZone(MapObjectRefs.SegTropics, SubZoneHelper.TropicsSubZones, "Tropics"),
            Zone.Alpine => GetActiveSubZone(MapObjectRefs.SegAlpine, SubZoneHelper.AlpineSubZones, "Alpine"),
            Zone.Mesa => SubZone.Mesa_Default,
            Zone.Caldera => SubZone.Caldera_Default,
            Zone.Kiln => SubZone.Kiln_Default,
            Zone.Peak => SubZone.Peak_Default,
            _ => SubZone.Unknown
        };
    }
    
    public static Zone GetZoneFromSubZone(SubZone subZone)
    {
        return subZone switch
        {
            >= SubZone.Shore_Default and < SubZone.Tropics_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Shore,
            >= SubZone.Tropics_Default and < SubZone.Alpine_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Tropics,
            >= SubZone.Alpine_Default and < SubZone.Mesa_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Alpine,
            >= SubZone.Mesa_Default and < SubZone.Caldera_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Mesa,
            >= SubZone.Caldera_Default and < SubZone.Kiln_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Kiln,
            >= SubZone.Kiln_Default and < SubZone.Peak_Default when Enum.IsDefined(typeof(SubZone), subZone) => Zone.Peak,
            _ => Zone.Unknown
        };
    }
}