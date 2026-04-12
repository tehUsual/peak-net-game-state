using System;

namespace NetGameState.GameState;

public static partial class GameStateEvents
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

}