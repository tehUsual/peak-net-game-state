using System;
using System.Collections.Generic;
using System.Linq;
using NetGameState.GameState;
using NetGameState.Types;
using NetGameState.MapRefs;
using UnityEngine;

namespace NetGameState.Segments;

public static class SegmentManager
{
    public static event Action<SegmentInfo, SegmentInfo>? OnSegmentLoading;
    public static event Action<SegmentInfo>? OnSegmentLoadComplete;

    public static readonly SegmentInfo[] CurrentRunSegments = new SegmentInfo[Enum.GetValues(typeof(Segment)).Length];
    public static readonly Dictionary<NgsBiome, NgsBiomeVariant> CurrentRunBiomeVariants = [];

    public static NgsSegment CurrentNgsSegment { get; private set; } =  NgsSegment.Unknown;
    public static NgsBiome CurrentNgsBiome { get; private set; } = NgsBiome.Unknown;
    public static NgsBiomeVariant CurrentNgsBiomeVariant { get; private set; } = NgsBiomeVariant.Unknown;

    public static SegmentInfo CurrentSegmentInfo { get; private set; } =
        new(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant);
    public static SegmentInfo PreviousSegmentInfo { get; set; } =
        new(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant);

    public static bool IsAlpine { get; private set; }
    public static bool IsTropics { get; private set; }


    static SegmentManager()
    {
        // TODO: On run ended
        GameStateEvents.OnAirportLoaded += Reset;
        GameStateEvents.OnSelfLeaveLobby += Reset;
    }

    private static void Reset()
    {
        CurrentNgsSegment = NgsSegment.Unknown;
        CurrentNgsBiome = NgsBiome.Unknown;
        CurrentNgsBiomeVariant = NgsBiomeVariant.Unknown;
        
        CurrentSegmentInfo = new SegmentInfo(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant);
        PreviousSegmentInfo = new SegmentInfo(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant);

        for (int i = 0; i < CurrentRunSegments.Length; i++)
        {
            CurrentRunSegments[i] = new SegmentInfo(NgsSegment.Unknown, NgsBiome.Unknown, NgsBiomeVariant.Unknown);
        }

        CurrentRunBiomeVariants.Clear();
        
        IsAlpine = false;
        IsTropics = false;
    }


    internal static void SetCurrentSegment(Segment segment)
    {
        PreviousSegmentInfo = new SegmentInfo(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant);
        
        CurrentNgsSegment = NgsTypes.SegmentToNgsSegment(segment);
        CurrentNgsBiome = NgsTypes.NgsSegmentToNgsBiome(CurrentNgsSegment);
        CurrentNgsBiomeVariant = NgsTypes.NgsBiomeToNgsBiomeVariant(CurrentNgsBiome);
        Transform? segTansform = GetBiomeSegment(CurrentNgsBiome);

        CurrentSegmentInfo = new SegmentInfo(CurrentNgsSegment, CurrentNgsBiome, CurrentNgsBiomeVariant, segTansform);
        OnSegmentLoading?.Invoke(PreviousSegmentInfo, CurrentSegmentInfo);
    }

    internal static void RaiseOnSegmentLoadComplete()
    {
        OnSegmentLoadComplete?.Invoke(CurrentSegmentInfo);
    }

    internal static void DetermineRunSegments()
    {
        foreach (var pair in Enum.GetValues(typeof(Segment))
                     .Cast<Segment>()
                     .Select((segment, index) => new { segment, index }))
        {
            NgsSegment ngsSegment = NgsTypes.SegmentToNgsSegment(pair.segment);
            NgsBiome ngsBiome = NgsTypes.NgsSegmentToNgsBiome(ngsSegment);
            NgsBiomeVariant ngsBiomeVariant = NgsTypes.NgsBiomeToNgsBiomeVariant(ngsBiome);
            Transform? segTansform = GetBiomeSegment(ngsBiome);
            CurrentRunSegments[pair.index] = new SegmentInfo(ngsSegment, ngsBiome, ngsBiomeVariant, segTansform);
            CurrentRunBiomeVariants[ngsBiome] = ngsBiomeVariant;

            if (ngsBiome == NgsBiome.Alpine)
                IsAlpine = true;
            if (ngsBiome == NgsBiome.Tropics)
                IsTropics = true;
        }
        
        CurrentNgsSegment = CurrentRunSegments[0].NgsSegment;
        CurrentNgsBiome = CurrentRunSegments[0].NgsBiome;
        CurrentNgsBiomeVariant = CurrentRunSegments[0].NgsBiomeVariant;
    }

    private static Transform? GetBiomeSegment(NgsBiome ngsBiome)
    {
        return (ngsBiome) switch
        {
            NgsBiome.Shore => MapObjectRefs.SegShore,
            
            NgsBiome.Tropics => MapObjectRefs.SegTropics,
            NgsBiome.Roots => MapObjectRefs.SegRoots,
            
            NgsBiome.Alpine => MapObjectRefs.SegAlpine,
            NgsBiome.Mesa => MapObjectRefs.SegMesa,
            
            NgsBiome.Caldera => MapObjectRefs.SegCaldera,
            NgsBiome.Kiln => MapObjectRefs.SegKiln,
            NgsBiome.Peak => MapObjectRefs.SegPeak,
            
            _ => null
        };
    }
}
