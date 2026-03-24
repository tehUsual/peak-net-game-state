using System;
using System.Collections.Generic;
using System.Linq;
using NetGameState.Events;
using NetGameState.Level.Helpers;
using NetGameState.Level;
using NetGameState.LevelStructure;
using UnityEngine;

namespace NetGameState.LevelProgression;

public static class SegmentManager
{
    public readonly struct SegmentInfo(Chapter chapter, Zone biome, SubZone subZone, Transform? segment = null)
    {
        public readonly Chapter Chapter = chapter;
        public readonly Zone Zone = biome;
        public readonly SubZone SubZone = subZone;
        public readonly Transform? SegTansform = segment;
    }

    public static event Action<SegmentInfo, SegmentInfo>? OnSegmentLoading;
    public static event Action<SegmentInfo>? OnSegmentLoadComplete;

    public static readonly SegmentInfo[] CurrentRunSegments = new SegmentInfo[Enum.GetValues(typeof(Segment)).Length];
    public static readonly Dictionary<Zone, SubZone> CurrentRunSubZones = [];

    public static Chapter CurrentChapter { get; private set; } =  Chapter.Unknown;
    public static Zone CurrentZone { get; private set; } = Zone.Unknown;
    public static SubZone CurrentSubZone { get; private set; } = SubZone.Unknown;

    public static SegmentInfo CurrentSegmentInfo { get; private set; } =
        new(CurrentChapter, CurrentZone, CurrentSubZone);
    public static SegmentInfo PreviousSegmentInfo { get; set; } =
        new(CurrentChapter, CurrentZone, CurrentSubZone);

    public static bool IsAlpine { get; private set; }


    static SegmentManager()
    {
        // TODO: On run ended
        GameStateEvents.OnAirportLoaded += Reset;
        GameStateEvents.OnSelfLeaveLobby += Reset;
    }

    private static void Reset()
    {
        CurrentChapter = Chapter.Unknown;
        CurrentZone = Zone.Unknown;
        CurrentSubZone = SubZone.Unknown;
        
        CurrentSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone);
        PreviousSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone);

        for (int i = 0; i < CurrentRunSegments.Length; i++)
        {
            CurrentRunSegments[i] = new SegmentInfo(Chapter.Unknown, Zone.Unknown, SubZone.Unknown);
        }
        
        IsAlpine = false;
    }


    internal static void SetCurrentSegment(Segment segment)
    {
        PreviousSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone);

        CurrentChapter = SegmentMapper.GetChapterFromSegment(segment);
        CurrentZone = SegmentMapper.GetZoneFromChapter(CurrentChapter);
        CurrentSubZone = SegmentMapper.GetSubZoneFromZone(CurrentZone);
        Transform? segTansform = GetBiomeSegment(CurrentZone);

        CurrentSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone, segTansform);
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
            Chapter chapter = SegmentMapper.GetChapterFromSegment(pair.segment);
            Zone zone = SegmentMapper.GetZoneFromChapter(chapter);
            SubZone subZone = SegmentMapper.GetSubZoneFromZone(zone);
            Transform? segTansform = GetBiomeSegment(zone);
            CurrentRunSegments[pair.index] = new SegmentInfo(chapter, zone, subZone, segTansform);
            CurrentRunSubZones[zone] = subZone;

            if (zone == Zone.Alpine)
                IsAlpine = true;
        }
        
        CurrentChapter = CurrentRunSegments[0].Chapter;
        CurrentZone = CurrentRunSegments[0].Zone;
        CurrentSubZone = CurrentRunSegments[0].SubZone;
    }

    private static Transform? GetBiomeSegment(Zone zone)
    {
        return (zone) switch
        {
            Zone.Shore => MapObjectRefs.SegShore,
            
            Zone.Tropics => MapObjectRefs.SegTropics,
            Zone.Roots => MapObjectRefs.SegRoots,
            
            Zone.Alpine => MapObjectRefs.SegAlpine,
            Zone.Mesa => MapObjectRefs.SegMesa,
            
            Zone.Caldera => MapObjectRefs.SegCaldera,
            Zone.Kiln => MapObjectRefs.SegKiln,
            Zone.Peak => MapObjectRefs.SegPeak,
            
            _ => null
        };
    }
}
