using NetGameState.MapRefs;
using NetGameState.Network;
using NetGameState.Segments;

namespace NetGameState.GameState;

public static partial class GameStateEvents
{
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