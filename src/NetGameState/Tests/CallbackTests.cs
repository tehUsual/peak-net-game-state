using ConsoleTools;
using NetGameState.Events;
using NetGameState.LevelProgression;
using Photon.Pun;

namespace NetGameState.Tests;

internal static class CallbackTests
{
    /* === Start online game ===
     * OnSelfCreatedLobby()
     * OnPlayerRegistered()
     * OnLobbyLoaded()
     */
    
    /* === Start offline game ===
     * OnPlayOfflineClicked()
     * OnPlayerRegistered()
     * OnAirportLoaded()
     */
    
    /* === Leave game ===
     * OnSelfLeaveLobby()
     */

    internal static void Init(bool all = false)
    {
        // ==== Callbacks - Start game ==================================
        GameStateEvents.OnPlayOfflineClicked += OnPlayOfflineClicked;
        
        // ==== Callbacks - Connection ==================================
        GameStateEvents.OnPlayerEnteredRoom += OnPlayerEnteredRoom;
        GameStateEvents.OnPlayerLeftRoom += OnPlayerLeftRoom;
        
        // ==== Callbacks - Steam Lobby Handler =========================
        GameStateEvents.OnSelfCreateLobby += OnSelfCreateLobby;;
        GameStateEvents.OnSelfJoinLobby += OnSelfJoinLobby;
        GameStateEvents.OnSelfLeaveLobby += OnSelfLeaveLobby;
        
        // ==== Callbacks - Player Registration =========================
        GameStateEvents.OnPlayerRegistered += OnPlayerRegistered;
        GameStateEvents.OnPlayerUnregistered += OnPlayerUnregistered;
        
        // ==== Callbacks - Airport / Lobby =============================
        GameStateEvents.OnAirportLoaded += OnAirportLoaded;
        
        // ==== Callbacks - Run Start ===================================
        GameStateEvents.OnRunStartLoading += OnRunStartLoading;
        if (all) GameStateEvents.OnLocalPlayerReady += OnLocalPlayerReady;
        GameStateEvents.OnPlayerReady += OnPlayerReady;
        GameStateEvents.OnAllPlayersReady += OnAllPlayersReady;
        GameStateEvents.OnRunStartLoadComplete += OnRunStartLoadComplete;
        
        // ==== Callbacks - Segments ====================================
        SegmentManager.OnSegmentLoading += OnSegmentLoading;
        SegmentManager.OnSegmentLoadComplete += OnSegmentLoadComplete;
    }

    // ==== Callbacks - Start game ==================================
    private static void OnPlayOfflineClicked()
    {
        Plugin.Log.LogColor("Play 'offline' clicked.");
    }


    // ==== Callbacks - Connection ==================================
    private static void OnPlayerEnteredRoom(Photon.Realtime.Player player)
    {
        Plugin.Log.LogColor($"Player '{player.NickName}' with actor number {player.ActorNumber} entered the room.");
    }

    private static void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        Plugin.Log.LogColor($"Player '{player.NickName}' with actor number {player.ActorNumber} left the room.");
    }
    
    
    // ==== Callbacks - Steam Lobby Handler =========================
    private static void OnSelfCreateLobby()
    {
        Plugin.Log.LogColor("Local player created lobby.");
    }
    
    private static void OnSelfJoinLobby()
    {
        Plugin.Log.LogColor("Local player joined lobby.");
    }
    
    private static void OnSelfLeaveLobby()
    {
        Plugin.Log.LogColor("Local player left lobby.");
    }
    
    
    // ==== Callbacks - Player Registration =========================
    private static void OnPlayerRegistered(Player player)
    {
        Plugin.Log.LogColor($"Player '{player.view.Owner.NickName}' with actor number {player.photonView.Owner.ActorNumber} registered.");
    }

    private static void OnPlayerUnregistered(Player player)
    {
        Plugin.Log.LogColor($"Player '{player.view.Owner.NickName}' with actor number {player.photonView.Owner.ActorNumber} unregistered.");
    }


    // ==== Callbacks - Run Start ===================================
    private static void OnRunStartLoading(string sceneName, int ascent)
    {
        Plugin.Log.LogColor($"Scene {sceneName} is loading. Ascent: {ascent}");
    }

    private static void OnLocalPlayerReady()
    {
        Plugin.Log.LogColor($"Local player '{Player.localPlayer.view.Owner.NickName}' is ready!");
    }

    private static void OnPlayerReady(Player player)
    {
        Plugin.Log.LogColor($"Player '{player.view.Owner.NickName}' is ready!");
    }

    private static void OnAllPlayersReady()
    {
        Plugin.Log.LogColor("All players are ready!");
        foreach (var photonPlayer in PhotonNetwork.PlayerList)
        {
            Player player = PlayerHandler.GetPlayer(photonPlayer);
            Plugin.Log.LogColor($" - '{player.view.Owner.NickName}' has actor number {player.photonView.Owner.ActorNumber}");
        }
    }
    
    private static void OnRunStartLoadComplete(string sceneName, int ascent)
    {
        Plugin.Log.LogColor($"Scene {sceneName} is loaded. Ascent: {ascent}");

        for (int i = 0; i < SegmentManager.CurrentRunSegments.Length; i++)
        {
            SegmentManager.SegmentInfo segInfo = SegmentManager.CurrentRunSegments[i];
            Plugin.Log.LogColorS($"Segment [{i+1}] is: [{segInfo.Chapter.ToString()}.{segInfo.Zone.ToString()}.{segInfo.SubZone.ToString()}]");
        }
    }
    
    
    // ==== Callbacks - Airport / Lobby =============================
    private static void OnAirportLoaded()
    {
        Plugin.Log.LogColor($"Lobby (airport) is loaded.");
    }

    // ==== Callbacks - Segments ====================================
    private static void OnSegmentLoading(SegmentManager.SegmentInfo prevSeg, SegmentManager.SegmentInfo nextSeg)
    {
        Plugin.Log.LogColor($"Segment loading: [{prevSeg.Chapter.ToString()}.{prevSeg.Zone.ToString()}.{prevSeg.SubZone.ToString()}]" +
                            $" -> [{nextSeg.Chapter.ToString()}.{nextSeg.Zone.ToString()}.{nextSeg.SubZone.ToString()}]");
    }

    private static void OnSegmentLoadComplete(SegmentManager.SegmentInfo seg)
    {
        Plugin.Log.LogColor($"Segment loaded: [{seg.Chapter.ToString()}.{seg.Zone.ToString()}.{seg.SubZone.ToString()}]");
    }
}