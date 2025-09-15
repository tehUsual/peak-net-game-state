using System.Collections;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using NetGameState.Events;
using NetGameState.Network;
using Photon.Pun;
using UnityEngine;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(LoadingScreenHandler))]
public static class LoadScenePatches
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(nameof(LoadingScreenHandler.LoadSceneProcessNetworked))]
    private static void Postfix(IEnumerator __result, LoadingScreenHandler __instance, string sceneName,
        bool yieldForCharacterSpawn, float extraYieldTimeOnEnd)
    {
        __instance.StartCoroutine(TrackLocalPlayerReady(__result, sceneName));
    }

    private static IEnumerator TrackLocalPlayerReady(IEnumerator original, string sceneName)
    {
        // Wait for the original coroutine to finish
        while (original.MoveNext())
            yield return original.Current;
        
        // Ensure character exists + message queue is back
        while (!Character.localCharacter || !PhotonNetwork.IsMessageQueueRunning)
            yield return null;
        
        // Notify local character is ready
        GameStateEvents.RaiseOnLocalPlayerReady();
        
        // Notify the master client that this player is ready
        var tracker = Object.FindFirstObjectByType<PlayerReadyTracker>();
        tracker?.Send_SetPlayerReady();
    }
}