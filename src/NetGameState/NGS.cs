using HarmonyLib;
using NetGameState.Network;
using NetGameState.Patches;
using Photon.Pun;
using UnityEngine;

namespace NetGameState;

public static class NGS
{
    public static void ApplyPatches(Harmony harmony)
    {
        harmony.PatchAll(typeof(AirportCheckInPatches));
        harmony.PatchAll(typeof(LoadScenePatches));
        harmony.PatchAll(typeof(MainMenuPatches));
        harmony.PatchAll(typeof(MapHandlerPatches));
        harmony.PatchAll(typeof(MountainProgressHandlerPatches));
        harmony.PatchAll(typeof(PlayerHandlerPatches));
        harmony.PatchAll(typeof(RunManagerPatches));
        harmony.PatchAll(typeof(SteamLobbyHandlerPatches));
    }

    public static void ApplyNetworkComponents(GameObject go, int viewID)
    {
        if (!go.TryGetComponent<PlayerReadyTracker>(out _))
            go.AddComponent<PlayerReadyTracker>();
        
        if (!go.TryGetComponent<PhotonCallbacks>(out _))
            go.AddComponent<PhotonCallbacks>();

        if (!go.TryGetComponent<PhotonView>(out _))
            go.AddComponent<PhotonView>();
        
        var pv = go.AddComponent<PhotonView>();
        pv.ViewID = viewID;
    }
}