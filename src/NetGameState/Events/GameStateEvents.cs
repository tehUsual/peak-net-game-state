using System;
using NetGameState.LevelProgression;
using NetGameState.LevelStructure;
using NetGameState.Network;


namespace NetGameState.Events;

public static class GameStateEvents
{
    // Run info
    public static string CurrentLevel { get; private set; } = "";
    public static int CurrentAscent { get; private set; }

    public static bool IsRunActive { get; private set; }
    public static bool IsInAirport { get; private set; }
    public static bool IsOfflineMode { get; private set; }

    public static bool IsAllReady { get; private set; }

    private static bool _isLobbyCreated;

    // === Main Menu ===
    public static event Action? OnPlayOfflineClicked;
    
    // === Photon Events ===
    public static event Action<Photon.Realtime.Player>? OnPlayerEnteredRoom;
    public static event Action<Photon.Realtime.Player>? OnPlayerLeftRoom;
    
    // === Steam Lobby Handler ===
    public static event Action? OnSelfCreateLobby;                              // creating a new lobby
    public static event Action? OnSelfJoinLobby;                                // joining another player lobby
    public static event Action? OnSelfLeaveLobby;                               // exiting to the main menu

    // === Player Registration Events ===
    public static event Action<Player>? OnPlayerRegistered;                     // player character joined/spawned
    public static event Action<Player>? OnPlayerUnregistered;                   // player character left/despawned
    
    // === Airport / Lobby ===
    public static event Action? OnAirportLoaded;                                // airport loaded for self

    // === Run Start Events ===
    public static event Action<string, int>? OnRunStartLoading;                 // run is loading Level_x
    public static event Action? OnLocalPlayerReady;                             // a local player has loaded Level_x
    public static event Action<Player>? OnPlayerReady;                          // a player had loaded Level_x (including self)
    public static event Action? OnAllPlayersReady;                              // all players have loaded Level_x
    public static event Action<Player>? OnPlayerLoadTimeout;                    // one or more players did not load Level_x in time
    public static event Action<string, int>? OnRunStartLoadComplete;            // run has loaded and started Level_x

    public static event Action? OnRunStartedAndPlayersReady;                    // run has loaded and all players are ready
    

    // === Raise Methods ======================================================
    // === Main Menu ===

    internal static void RaiseOnPlayOfflineClicked()
    {
        IsOfflineMode = true;
        OnPlayOfflineClicked?.Invoke();
    }

    
    // === Photon Events ===
    internal static void RaiseOnPlayerEnteredRoom(Photon.Realtime.Player player) =>
        OnPlayerEnteredRoom?.Invoke(player);

    internal static void RaiseOnPlayerLeftRoom(Photon.Realtime.Player player) =>
        OnPlayerLeftRoom?.Invoke(player);

    
    // === Steam Lobby Handler ===
    internal static void RaiseOnSelfCreateLobby()
    {
        _isLobbyCreated = true;
        IsOfflineMode = false;
        OnSelfCreateLobby?.Invoke();    
    }

    internal static void RaiseOnSelfJoinLobby()
    {
        IsOfflineMode = false;
        
        if (!_isLobbyCreated)
            OnSelfJoinLobby?.Invoke();
    }

    internal static void RaiseOnSelfLeaveLobby()
    {
        _isLobbyCreated = false;
        IsInAirport = false;
        IsRunActive = false;
        OnSelfLeaveLobby?.Invoke();    
    }
    
    
    // === Player Registration Events ===
    internal static void RaiseOnPlayerRegistered(Player player) =>
        OnPlayerRegistered?.Invoke(player);

    internal static void RaiseOnPlayerUnregistered(Player player) =>
        OnPlayerUnregistered?.Invoke(player);

    
    // === Airport / Lobby ===
    internal static void RaiseOnAirportLoaded()
    {
        IsInAirport = true;
        IsRunActive = false;
        IsAllReady = false;

        PlayerReadyTracker.Instance.enabled = true;
        
        OnAirportLoaded?.Invoke();
    }
    
    
    // === Run Start Events ===
    internal static void RaiseOnStartLoading(string sceneName, int ascent)
    {
        CurrentLevel = sceneName;
        CurrentAscent = ascent;
        IsInAirport = false;
        OnRunStartLoading?.Invoke(sceneName, ascent);    
    }

    internal static void RaiseOnLocalPlayerReady() =>
        OnLocalPlayerReady?.Invoke();

    internal static void RaiseOnPlayerReady(Player player) =>
        OnPlayerReady?.Invoke(player);

    internal static void RaiseOnAllPlayersReady()
    {
        IsAllReady = true;
        OnAllPlayersReady?.Invoke();

        if (IsRunActive)
        {
            OnRunStartedAndPlayersReady?.Invoke();
        }
    }
    

    internal static void RaiseOnPlayerLoadTimeout(Player player)
    {
        IsAllReady = true;
        OnPlayerLoadTimeout?.Invoke(player);
    }
    
    
    internal static void RaiseOnRunLoadComplete()
    {
        MapObjectRefs.Init();                   // 9ms
        SegmentManager.DetermineRunSegments();  // 6ms
        IsRunActive = true;
        IsInAirport = false;
        OnRunStartLoadComplete?.Invoke(CurrentLevel, CurrentAscent);

        if (IsAllReady)
        {
            OnRunStartedAndPlayersReady?.Invoke();
        }
    }
}