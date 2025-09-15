using HarmonyLib;
using NetGameState.Events;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(SteamLobbyHandler))]
public static class SteamLobbyHandlerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPostfix]
    [HarmonyPatch(nameof(SteamLobbyHandler.OnLobbyCreated))]
    private static void Postfix_OnLobbyCreated(SteamLobbyHandler __instance)
    {
        GameStateEvents.RaiseOnSelfCreateLobby();
    }
    
    // ReSharper disable once InconsistentNaming
    [HarmonyPostfix]
    [HarmonyPatch(nameof(SteamLobbyHandler.OnLobbyEnter))]
    private static void Postfix_OnLobbyEnter(SteamLobbyHandler __instance)
    {
        GameStateEvents.RaiseOnSelfJoinLobby();
    }
    
    // ReSharper disable once InconsistentNaming
    [HarmonyPrefix]
    [HarmonyPatch(nameof(SteamLobbyHandler.LeaveLobby))]
    private static void Prefix_LeaveLobby(SteamLobbyHandler __instance)
    {
        if (__instance.m_currentLobby.m_SteamID != 0UL)
            GameStateEvents.RaiseOnSelfLeaveLobby();
    }
}