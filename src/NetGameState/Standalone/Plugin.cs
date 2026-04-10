using System;
using BepInEx;
using ConsoleTools.Patches;
using ConsoleTools;
using HarmonyLib;
using NetGameState.Events;
using NetGameState.Logging;
using NetGameState.Tests;
using Photon.Pun;
using UnityEngine;

namespace NetGameState;


[BepInAutoPlugin]
public partial class Plugin : BaseUnityPlugin
{
    //internal static ManualLogSource Log { get; private set; } = null!;
    internal static bool Debug { get; private set; } = false;

    private static GameObject? _netGameStateTracker;
    
    public static Plugin Instance { get; private set; } = null!;
    
    private const int NetGameStateViewID = 9969;
    
    private const string CompatibleVersion = "1.54.c";

    private void Awake()
    {
        Instance = this;
        
        LogProvider.Log = Logger;
        LogProvider.Log.LogInfo($"Plugin {Name} is loaded!");

        if (Application.version.Trim('.') != CompatibleVersion)
            LogProvider.Log.LogColorW($"This plugin is only compatible with v{CompatibleVersion}. The library may not work correctly."
                          + $" Current game version: v{Application.version}");
        
        // === Apply harmony patches
        var harmony = new Harmony("com.github.tehUsual.NetGameState");
        NetGameState.ApplyPatches(harmony);
        
        
#if NETGAMESTATE_STANDALONE
        if (Debug)
            harmony.PatchAll(typeof(ConsoleLogListenerPatches));
#endif
        
        // === Create game objects
        CreatePersistentGameObjects();
        
#if NETGAMESTATE_STANDALONE
        if (Debug)
        {
            // === Configure console
            ConsoleConfig.Register(Name);
            ConsoleConfig.SetLogging(Name, Debug);
            ConsoleConfig.ShowUnityLogs = false;
            ConsoleConfig.SetDefaultSourceColor(ConsoleColor.DarkCyan);
            ConsoleConfig.SetDefaultCallerColor(ConsoleColor.DarkYellow);    
        }
#endif 
      
        
#if NETGAMESTATE_STANDALONE
        if (Debug)
        {
            // === Tests
            CallbackTests.Init();
        }
#endif
        
        LogProvider.Log.LogColor($"Plugin {Name} load complete!");
    }
    

#if NETGAMESTATE_STANDALONE
    private void Update()
    {
        if (!Debug)
            return;
        
        // Toggle unity console
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ConsoleConfig.ShowUnityLogs = !ConsoleConfig.ShowUnityLogs;
            LogProvider.Log?.LogColor($"Unity console logs {(ConsoleConfig.ShowUnityLogs ? "enabled" : "disabled")}");
        }
        
        // Master only
        if (PhotonNetwork.IsMasterClient && GameStateEvents.IsRunActive)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
                TeleportHandler.TeleportToCampfire(Tests.Campfire.Shore);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                TeleportHandler.TeleportToCampfire(Tests.Campfire.TropicsRoots);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                TeleportHandler.TeleportToCampfire(Tests.Campfire.AlpineMesa);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                TeleportHandler.TeleportToCampfire(Tests.Campfire.Caldera);
            if (Input.GetKeyDown(KeyCode.Keypad5))
                TeleportHandler.TeleportToCampfire(Tests.Campfire.PeakFlagpole);
        }
    }
#endif

    private static void CreatePersistentGameObjects()
    {
        // Don't allow duplicates
        if (GameObject.Find("NetGameState_Tracker")?.activeInHierarchy ?? false)
            return;
        
        _netGameStateTracker = new GameObject("NetGameState_Tracker");
        NetGameState.ApplyNetworkComponents(_netGameStateTracker, NetGameStateViewID);
    }
    
    private void OnDestroy()
    {
        CallbackTests.Reset();
        if (!ReferenceEquals(_netGameStateTracker, null))
            Destroy(_netGameStateTracker);
    }
}

