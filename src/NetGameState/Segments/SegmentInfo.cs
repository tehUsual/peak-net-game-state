using NetGameState.Types;
using UnityEngine;

namespace NetGameState.Segments;

public readonly struct SegmentInfo(NgsSegment ngsSegment, NgsBiome biome, NgsBiomeVariant ngsBiomeVariant, Transform? segment = null)
{
    public readonly NgsSegment NgsSegment = ngsSegment;
    public readonly NgsBiome NgsBiome = biome;
    public readonly NgsBiomeVariant NgsBiomeVariant = ngsBiomeVariant;
    public readonly Transform? SegmentTansform = segment;
}