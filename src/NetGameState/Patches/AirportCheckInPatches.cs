using HarmonyLib;
using NetGameState.Events;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(AirportCheckInKiosk))]
public static class AirportCheckInPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(AirportCheckInKiosk.BeginIslandLoadRPC))]
    private static void Prefix(AirportCheckInKiosk __instance, string sceneName, int ascent)
    {
        GameStateEvents.RaiseOnStartLoading(sceneName, ascent);
    }
}