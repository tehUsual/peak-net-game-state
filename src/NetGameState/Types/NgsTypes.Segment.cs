namespace NetGameState.Types;


public static partial class NgsTypes
{
    public static NgsSegment SegmentToNgsSegment(Segment segment)
    {
        return segment switch
        {
            Segment.Beach   => NgsSegment.One,
            Segment.Tropics => NgsSegment.Two,
            Segment.Alpine  => NgsSegment.Three,
            Segment.Caldera => NgsSegment.Four,
            Segment.TheKiln => NgsSegment.Five,
            Segment.Peak    => NgsSegment.Six,
            _ => NgsSegment.Unknown
        };
    }
    
    public static Segment? NgsSegmentToSegment(this NgsSegment segment)
    {
        return segment switch
        {
            NgsSegment.One   => Segment.Beach,
            NgsSegment.Two   => Segment.Tropics,
            NgsSegment.Three => Segment.Alpine,
            NgsSegment.Four  => Segment.Caldera,
            NgsSegment.Five  => Segment.TheKiln,
            NgsSegment.Six   => Segment.Peak,
            _ => null // ( NgsSegment.Unknown || NgsSegment.Any)
        };
    }

    public static NgsSegment NgsBiomeToNgsSegment(NgsBiome biome)
    {
        return biome switch
        {
            NgsBiome.Shore => NgsSegment.One,

            NgsBiome.Tropics => NgsSegment.Two,
            NgsBiome.Roots => NgsSegment.Two,

            NgsBiome.Alpine => NgsSegment.Three,
            NgsBiome.Mesa => NgsSegment.Three,

            NgsBiome.Caldera => NgsSegment.Four,
            NgsBiome.Kiln => NgsSegment.Five,
            NgsBiome.Peak => NgsSegment.Six,

            _ => NgsSegment.Unknown
        };
    }
}