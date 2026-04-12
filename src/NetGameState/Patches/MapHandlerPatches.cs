using HarmonyLib;
using NetGameState.Segments;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(MapHandler))]
public static class MapHandlerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(MapHandler.GoToSegment))]
    private static void Prefix(MapHandler __instance, Segment s)
    {
        SegmentManager.SetCurrentSegment(s);
    }
}