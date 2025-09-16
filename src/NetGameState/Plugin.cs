using System;
using BepInEx;
using ConsoleTools.Patches;
using ConsoleTools;
using HarmonyLib;
using NetGameState.Events;
using NetGameState.Listeners;
using NetGameState.Logging;
using NetGameState.Network;
using NetGameState.Patches;
using NetGameState.Tests;
using NetGameState.Util;
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
    
    private const string CompatibleVersion = "1.29.a";

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
        harmony.PatchAll(typeof(AirportCheckInPatches));
        harmony.PatchAll(typeof(LoadScenePatches));
        harmony.PatchAll(typeof(MainMenuPatches));
        harmony.PatchAll(typeof(MapHandlerPatches));
        harmony.PatchAll(typeof(MountainProgressHandlerPatches));
        harmony.PatchAll(typeof(PlayerHandlerPatches));
        harmony.PatchAll(typeof(RunManagerPatches));
        harmony.PatchAll(typeof(SteamLobbyHandlerPatches));
        
        
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
                TeleportHandler.TeleportToCampfire(Util.Campfire.Shore);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                TeleportHandler.TeleportToCampfire(Util.Campfire.Tropics);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                TeleportHandler.TeleportToCampfire(Util.Campfire.AlpineMesa);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                TeleportHandler.TeleportToCampfire(Util.Campfire.Caldera);
            if (Input.GetKeyDown(KeyCode.Keypad5))
                TeleportHandler.TeleportToCampfire(Util.Campfire.PeakFlagpole);
        }
    }
#endif

    private static void CreatePersistentGameObjects()
    {
        // Don't allow duplicates
        if (GameObject.Find("NetGameState_Tracker")?.activeInHierarchy ?? false)
            return;
        
        _netGameStateTracker = new GameObject("NetGameState_Tracker");
        _netGameStateTracker.AddComponent<PlayerReadyTracker>();
        _netGameStateTracker.AddComponent<PhotonCallbacks>();
        var pv = _netGameStateTracker.AddComponent<PhotonView>();
        pv.ViewID = NetGameStateViewID;
        DontDestroyOnLoad(_netGameStateTracker);
    }
    
    private void OnDestroy()
    {
        CallbackTests.Reset();
        if (!ReferenceEquals(_netGameStateTracker, null))
            Destroy(_netGameStateTracker);
    }
}

