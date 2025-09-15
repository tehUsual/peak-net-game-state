using System;
using System.Linq;
using NetGameState.Events;
using NetGameState.Level.Helpers;
using NetGameState.Level;

namespace NetGameState.LevelProgression;

public static class SegmentManager
{
    public readonly struct SegmentInfo(Chapter chapter, Zone biome, SubZone subZone)
    {
        public readonly Chapter Chapter = chapter;
        public readonly Zone Zone = biome;
        public readonly SubZone SubZone = subZone;
    }

    public static event Action<SegmentInfo, SegmentInfo>? OnSegmentLoading;
    public static event Action<SegmentInfo>? OnSegmentLoadComplete;

    public static readonly SegmentInfo[] CurrentRunSegments = new SegmentInfo[Enum.GetValues(typeof(Segment)).Length];

    public static Chapter CurrentChapter { get; private set; } =  Chapter.Unknown;
    public static Zone CurrentZone { get; private set; } = Zone.Unknown;
    public static SubZone CurrentSubZone { get; private set; } = SubZone.Unknown;

    public static SegmentInfo CurrentSegmentInfo { get; private set; } =
        new(CurrentChapter, CurrentZone, CurrentSubZone);
    public static SegmentInfo PreviousSegmentInfo { get; set; } =
        new(CurrentChapter, CurrentZone, CurrentSubZone);


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
    }

    internal static void SetCurrentSegment(Segment segment)
    {
        PreviousSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone);

        CurrentChapter = SegmentMapper.GetChapterFromSegment(segment);
        CurrentZone = SegmentMapper.GetZoneFromChapter(CurrentChapter);
        CurrentSubZone = SegmentMapper.GetSubZoneFromZone(CurrentZone);

        CurrentSegmentInfo = new SegmentInfo(CurrentChapter, CurrentZone, CurrentSubZone);
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
            CurrentRunSegments[pair.index] = new SegmentInfo(chapter, zone, subZone);
        }
    }
}
