using System;
using System.Linq;
using ConsoleTools;
using NetGameState.LevelStructure;
using NetGameState.Logging;
using UnityEngine;

namespace NetGameState.Types;

public static partial class NgsTypes
{
    public static readonly NgsVariantShore[] ShoreBiomeVariants = Enum.GetValues(typeof(NgsVariantShore))
        .Cast<NgsVariantShore>()
        .Where(v => v != NgsVariantShore.Unknown && v != NgsVariantShore.Any)
        .ToArray();
    
    public static readonly NgsVariantTropics[] TropicsBiomeVariants = Enum.GetValues(typeof(NgsVariantTropics))
        .Cast<NgsVariantTropics>()
        .Where(v => v != NgsVariantTropics.Unknown && v != NgsVariantTropics.Any)
        .ToArray();
    
    public static readonly NgsVariantAlpine[] AlpineBiomeVariants = Enum.GetValues(typeof(NgsVariantAlpine))
        .Cast<NgsVariantAlpine>()
        .Where(v => v != NgsVariantAlpine.Unknown && v != NgsVariantAlpine.Any)
        .ToArray();
    
    
    private static NgsBiomeVariant GetActiveBiomeVariant<T>(Transform? root, T[] biomeVariants, string biomeName)
    {
        if (ReferenceEquals(root, null))
        {
            LogProvider.Log?.LogColorW($"Could not find '{biomeName}' Segment");
            return NgsBiomeVariant.Unknown;
        }

        foreach (var variant in biomeVariants)
        {
            if (root.Find(variant?.ToString())?.gameObject.activeSelf ?? false)
                return (NgsBiomeVariant)Convert.ToInt32(biomeVariants);
        }
        
        LogProvider.Log?.LogColorW($"Could not find variant for '{biomeName}'");
        return NgsBiomeVariant.Unknown;
    }
    
    
    public static NgsBiomeVariant NgsBiomeToNgsBiomeVariant(NgsBiome ngsBiome)
    {
        // TODO: Add cache (use segment manager)
        return ngsBiome switch
        {
            NgsBiome.Shore => GetActiveBiomeVariant(MapObjectRefs.SegShore, ShoreBiomeVariants, "Shore"),
            NgsBiome.Tropics => GetActiveBiomeVariant(MapObjectRefs.SegTropics, TropicsBiomeVariants, "Tropics"),
            NgsBiome.Roots => NgsBiomeVariant.Roots_Default,    // TODO: Add roots variants
            NgsBiome.Alpine => GetActiveBiomeVariant(MapObjectRefs.SegAlpine, AlpineBiomeVariants, "Alpine"),
            NgsBiome.Mesa => NgsBiomeVariant.Mesa_Default,
            NgsBiome.Caldera => NgsBiomeVariant.Caldera_Default,
            NgsBiome.Kiln => NgsBiomeVariant.Kiln_Default,
            NgsBiome.Peak => NgsBiomeVariant.Peak_Default,
            _ => NgsBiomeVariant.Unknown
        };
    }
    
    public static NgsBiome NgsBiomeVariantToBiome(NgsBiomeVariant ngsBiomeVariant)
    {
        if (ngsBiomeVariant is NgsBiomeVariant.Unknown or NgsBiomeVariant.Any)
            return NgsBiome.Unknown;
        
        return ngsBiomeVariant switch
        {
            >= NgsBiomeVariant.Shore_Default and < NgsBiomeVariant.Tropics_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Shore,
            
            >= NgsBiomeVariant.Tropics_Default and < NgsBiomeVariant.Roots_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Tropics,
            >= NgsBiomeVariant.Roots_Default and < NgsBiomeVariant.Alpine_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Roots,
            
            >= NgsBiomeVariant.Alpine_Default and < NgsBiomeVariant.Mesa_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Alpine,
            >= NgsBiomeVariant.Mesa_Default and < NgsBiomeVariant.Caldera_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Mesa,
            
            >= NgsBiomeVariant.Caldera_Default and < NgsBiomeVariant.Kiln_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Kiln,
            >= NgsBiomeVariant.Kiln_Default and < NgsBiomeVariant.Peak_Default when Enum.IsDefined(typeof(NgsBiomeVariant), ngsBiomeVariant) => NgsBiome.Peak,
            
            _ => NgsBiome.Unknown
        };
    }
    
    public static string NgsBiomeVariantToString(NgsBiome ngsBiome, NgsBiomeVariant ngsBiomeVariant)
    {
        switch (ngsBiome)
        {
            case NgsBiome.Shore: return ((NgsVariantShore)ngsBiomeVariant).ToString();
            
            case NgsBiome.Tropics: return ((NgsVariantTropics)ngsBiomeVariant).ToString();
            case NgsBiome.Roots: return ((NgsVariantRoots)ngsBiomeVariant).ToString();
            
            case NgsBiome.Alpine: return ((NgsVariantAlpine)ngsBiomeVariant).ToString();
            case NgsBiome.Mesa: return ((NgsVariantMesa)ngsBiomeVariant).ToString();
            
            case NgsBiome.Caldera: return ((NgsVariantCaldera)ngsBiomeVariant).ToString();
            case NgsBiome.Kiln: return ((NgsVariantKiln)ngsBiomeVariant).ToString();
            case NgsBiome.Peak: return ((NgsVariantPeak)ngsBiomeVariant).ToString();
            
            default: return ngsBiomeVariant.ToString();
        }
    }
}