using System;
using System.Linq;

namespace NetGameState.Types;

public enum NgsSegment
{
    Unknown = -1,
    One,            // Shore
    Two,            // Tropics / Roots
    Three,          // Alpine / Mesa
    Four,           // Caldera
    Five,           // Kiln
    Six             // Peak
}

public static class NgsSegmentExtensions
{     
    public static NgsSegment Start => Enum.GetValues(typeof(NgsSegment))
        .Cast<NgsSegment>()
        .Where(c => c != NgsSegment.Unknown)
        .Min();

    public static NgsSegment End => Enum.GetValues(typeof(NgsSegment))
        .Cast<NgsSegment>()
        .Max();
}