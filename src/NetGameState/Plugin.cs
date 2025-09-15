using System;
using BepInEx;
using BepInEx.Logging;
using ConsoleTools.Patches;
using ConsoleTools;
using HarmonyLib;
using NetGameState.Events;
using NetGameState.Listeners;
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
    internal static ManualLogSource Log { get; private set; } = null!;
    internal static bool Debug { get; private set; } = true;
    
    private const int NetGameStateViewID = 9969;

    private void Awake()
    {
        Log = Logger;
        Log.LogInfo($"Plugin {Name} is loaded!");
        
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
        
        harmony.PatchAll(typeof(ConsoleLogListenerPatches));
        
        // === Create game objects
        CreatePersistentGameObjects();
        
        // === Configure console
        ConsoleConfig.Register(Name);
        ConsoleConfig.SetLogging(Name, Debug);
        ConsoleConfig.ShowUnityLogs = false;
        ConsoleConfig.SetDefaultSourceColor(ConsoleColor.DarkCyan);
        ConsoleConfig.SetDefaultCallerColor(ConsoleColor.DarkYellow);
        
        // === Tests
        CallbackTests.Init();
        
        // === Utilities
        GameStateEvents.OnRunStartLoadComplete += OnRunStartLoadComplete;
        GameStateEvents.OnAirportLoaded += OnLobbyLoaded;
    }
    
    private void Start()
    {
    }

    private void Update()
    {
        // Toggle unity console
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ConsoleConfig.ShowUnityLogs = !ConsoleConfig.ShowUnityLogs;
            Log.LogColor($"Unity console logs {(ConsoleConfig.ShowUnityLogs ? "enabled" : "disabled")}");
        }
        
        // Master only
        if (PhotonNetwork.IsMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
                TeleportHandler.TeleportToCampfire(Util.Campfire.Shore);
            if (Input.GetKeyDown(KeyCode.Keypad2))
                TeleportHandler.TeleportToCampfire(Util.Campfire.Tropics);
            if (Input.GetKeyDown(KeyCode.Keypad3))
                TeleportHandler.TeleportToCampfire(Util.Campfire.AlpineMesa);
            if (Input.GetKeyDown(KeyCode.Keypad4))
                TeleportHandler.TeleportToCampfire(Util.Campfire.Caldera);
        }
    }

    private static void CreatePersistentGameObjects()
    {
        // Don't allow duplicates
        if (GameObject.Find("NetGameState_Tracker")?.activeInHierarchy ?? false)
            return;
        
        var trackerObj = new GameObject("NetGameState_Tracker");
        trackerObj.AddComponent<PlayerReadyTracker>();
        trackerObj.AddComponent<PhotonCallbacks>();
        var pv = trackerObj.AddComponent<PhotonView>();
        pv.ViewID = NetGameStateViewID;
        DontDestroyOnLoad(trackerObj);
    }
    
    private void OnDestroy()
    {
    }



    private void OnRunStartLoadComplete(string sceneName, int ascent)
    {
        TeleportHandler.Init();
    }

    private void OnLobbyLoaded()
    {
        TeleportHandler.Reset();
    }
}
