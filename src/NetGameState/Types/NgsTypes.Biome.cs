using NetGameState.MapRefs;

namespace NetGameState.Types;

public static partial class NgsTypes
{
    public static NgsBiome BiomeToNgsBiome(Biome.BiomeType biomeType)
    {
        return biomeType switch
        {
            Biome.BiomeType.Shore => NgsBiome.Shore,        // BiomeType.0
            
            Biome.BiomeType.Tropics => NgsBiome.Tropics,    // BiomeType.1
            Biome.BiomeType.Roots => NgsBiome.Roots,        // BiomeType.7
            
            Biome.BiomeType.Alpine => NgsBiome.Alpine,      // BiomeType.2
            Biome.BiomeType.Mesa => NgsBiome.Mesa,          // BiomeType.6
            
            Biome.BiomeType.Volcano => NgsBiome.Caldera,    // BiomeType.3
            // NOTE: Volcano is a combination of Caldera and Kiln?

            Biome.BiomeType.Peak => NgsBiome.Peak,          // BiomeType.5
            
            _ => NgsBiome.Unknown,
        };
    }

    public static Biome.BiomeType? NgsBiomeToBiome(NgsBiome biome)
    {
        return biome switch
        {
            NgsBiome.Shore => Biome.BiomeType.Shore,

            NgsBiome.Tropics => Biome.BiomeType.Tropics,
            NgsBiome.Roots => Biome.BiomeType.Roots,

            NgsBiome.Alpine => Biome.BiomeType.Alpine,
            NgsBiome.Mesa => Biome.BiomeType.Mesa,

            NgsBiome.Caldera => Biome.BiomeType.Volcano,
            NgsBiome.Kiln => Biome.BiomeType.Volcano,

            NgsBiome.Peak => Biome.BiomeType.Peak,
            _ => null // (NgsBiome.Unknown || NgsBiome.Any)
        };
    }

    public static NgsBiome NgsSegmentToNgsBiome(NgsSegment segment)
    {
        switch (segment)
        {
            case NgsSegment.One:
                return NgsBiome.Shore;
            case NgsSegment.Two:
                if (MapObjectRefs.BioTropics?.gameObject.activeSelf ?? false)
                    return NgsBiome.Tropics;
                if (MapObjectRefs.BioRoots?.gameObject.activeSelf ?? false)
                    return NgsBiome.Roots;
                break;
            case NgsSegment.Three:
                if (MapObjectRefs.BioAlpine?.gameObject.activeSelf ?? false)
                    return NgsBiome.Alpine;
                if (MapObjectRefs.BioMesa?.gameObject.activeSelf ?? false)
                    return NgsBiome.Mesa;
                break;
            case NgsSegment.Four:
                return NgsBiome.Caldera;
            case NgsSegment.Five:
                return NgsBiome.Kiln;
            case NgsSegment.Six:
                return NgsBiome.Peak;
        }
        return NgsBiome.Unknown;
    }
}