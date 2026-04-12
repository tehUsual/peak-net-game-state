using System.Collections.Generic;
using ConsoleTools;
using NetGameState.GameState;
using NetGameState.Logging;
using Photon.Pun;
using UnityEngine;

namespace NetGameState.Network;

public class PlayerReadyTracker : MonoBehaviourPun
{
    public static PlayerReadyTracker Instance { get; private set; } = null!;
    private readonly HashSet<int> _readyPlayers = [];
    private readonly Dictionary<int, float> _loadStartTimes = [];
    private const float TimeoutSeconds = 15f;
    private bool _allIsReady;
    private bool _waitForAllReady;

    private void Awake()
    {
        if (!ReferenceEquals(Instance, null))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameStateEvents.OnRunStartLoading += OnRunStartLoading;
        ResetTracking(false);
    }

    private void OnDisable()
    {
        GameStateEvents.OnRunStartLoading -= OnRunStartLoading;
    }

    private void OnDestroy()
    {
        GameStateEvents.OnRunStartLoading -= OnRunStartLoading;
    }

    private void OnRunStartLoading(string sceneName, int ascent)
    {
        ResetTracking(true);
    }

    private void ResetTracking(bool waitForAll)
    {
        if (waitForAll)
            LogProvider.Log?.LogColor("Player ready tracker waiting for all players to be ready.");
        else
            LogProvider.Log?.LogColor("Player ready tracker reset");
        
        _readyPlayers.Clear();
        _loadStartTimes.Clear();
        _allIsReady = false;
        _waitForAllReady = waitForAll;
    }

    internal void Send_SetPlayerReady()
    {
        photonView.RPC(nameof(RPC_NGS_SetPlayerReady), RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber);
        if (!PhotonNetwork.IsMasterClient)
            enabled = false;
    }

    [PunRPC]
    public void RPC_NGS_SetPlayerReady(int actorNumber, PhotonMessageInfo info)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        
        if (!_readyPlayers.Add(actorNumber))
            return;

        if (PlayerHandler.TryGetPlayer(actorNumber, out var playerHandler))
        {
            // Notify any player ready
            _loadStartTimes.Remove(actorNumber);
            GameStateEvents.RaiseOnPlayerReady(playerHandler);    
        }
        else
        {
            LogProvider.Log?.LogColorW($"Could not find player with actor number {actorNumber}");
            return;
        }
        
        // Notify all players are ready
        if (_readyPlayers.Count == PhotonNetwork.PlayerList.Length)
        {
            _allIsReady = true;
            _waitForAllReady = false;
            GameStateEvents.RaiseOnAllPlayersReady();
            enabled = false;
        }
    }

    private void Update()
    {
        if (_allIsReady || !PhotonNetwork.IsMasterClient || !PhotonNetwork.InRoom || !_waitForAllReady)
            return;
        
        foreach (var p in PhotonNetwork.PlayerList)
        {
            if (_readyPlayers.Contains(p.ActorNumber))
                continue;
            
            if (!_loadStartTimes.ContainsKey(p.ActorNumber))
                _loadStartTimes[p.ActorNumber] = Time.realtimeSinceStartup;
            
            // Timeout check
            if (!(Time.realtimeSinceStartup - _loadStartTimes[p.ActorNumber] > TimeoutSeconds))
                continue;
            
            if (PlayerHandler.TryGetPlayer(p.ActorNumber, out var playerHandler))
            {
                // Notify player load timeout
                _waitForAllReady = false;
                GameStateEvents.RaiseOnPlayerLoadTimeout(playerHandler);
                enabled = false;
            }
            else
            {
                LogProvider.Log?.LogColorW($"Could not resolve PlayerHandler for actor number {p.ActorNumber}");
            }
                
            _loadStartTimes[p.ActorNumber] = float.MaxValue;
        }
    }
}