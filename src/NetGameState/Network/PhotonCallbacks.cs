using NetGameState.Events;
using Photon.Pun;


namespace NetGameState.Listeners;

public class PhotonCallbacks : MonoBehaviourPunCallbacks
{
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        // Notify player joined the room
        GameStateEvents.RaiseOnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        // Notify player left the room
        GameStateEvents.RaiseOnPlayerLeftRoom(otherPlayer);
        
        if (PlayerHandler.TryGetPlayer(otherPlayer.ActorNumber, out var playerHandler))
        {
            // Notify player unregistered
            GameStateEvents.RaiseOnPlayerUnregistered(playerHandler);
        }
    }
}