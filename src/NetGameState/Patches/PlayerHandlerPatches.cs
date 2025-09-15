using HarmonyLib;
using NetGameState.Events;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(PlayerHandler))]
public static class PlayerHandlerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(PlayerHandler.RegisterPlayer))]
    private static void Postfix(PlayerHandler __instance, Player player)
    {
        GameStateEvents.RaiseOnPlayerRegistered(player);
    }
}