using HarmonyLib;
using NetGameState.LevelProgression;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(MountainProgressHandler))]
public static class MountainProgressHandlerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(MountainProgressHandler.SetSegmentComplete))]
    private static void Postfix(MountainProgressHandler __instance, int segment)
    {
        SegmentManager.RaiseOnSegmentLoadComplete();
    }
}