using HarmonyLib;
using NetGameState.GameState;
using UnityEngine.SceneManagement;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(RunManager))]
public static class RunManagerPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(RunManager.Start))]
    private static void Prefix(RunManager __instance)
    {
        string sceneName = SceneManager.GetActiveScene().name.ToLower();
        
        //LogProvider.Log?.LogColor($"RunManager.StartRun: {sceneName}");
        
        if (sceneName.StartsWith("airport"))
            GameStateEvents.RaiseOnAirportLoaded();
        if (sceneName.StartsWith("level_") || sceneName.StartsWith("wilisland"))
            GameStateEvents.RaiseOnRunLoadComplete();
    }
}