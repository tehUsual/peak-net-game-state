using ConsoleTools;
using HarmonyLib;
using NetGameState.Events;
using UnityEngine.SceneManagement;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(RunManager))]
public static class RunManagerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(RunManager.StartRun))]
    private static void Postfix(RunManager __instance)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (sceneName.StartsWith("Airport"))
            GameStateEvents.RaiseOnAirportLoaded();
        if (sceneName.StartsWith("Level_") || sceneName.StartsWith("WilIsland"))
            GameStateEvents.RaiseOnRunLoadComplete();
    }
}