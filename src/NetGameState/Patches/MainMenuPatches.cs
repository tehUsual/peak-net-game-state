using HarmonyLib;
using NetGameState.Events;

namespace NetGameState.Patches;

[HarmonyPatch(typeof(MainMenu))]
public static class MainMenuPatches
{
    // ReSharper disable once InconsistentNaming
    [HarmonyPatch(nameof(MainMenu.PlaySoloClicked))]
    private static void Postfix(MainMenu __instance)
    {
        GameStateEvents.RaiseOnPlayOfflineClicked();
    }
}