using System;
using ConsoleTools;
using NetGameState.Events;
using NetGameState.LevelProgression;
using NetGameState.LevelStructure;
using Photon.Pun;
using UnityEngine;

namespace NetGameState.Util;

internal enum Campfire
{
    Shore,
    Tropics,
    AlpineMesa,
    Caldera,
    PeakFlagpole,
}

internal static class TeleportHandler
{
    private static Vector3 _offset = new(0, 10, 0);

    internal static void TeleportToCampfire(Campfire campfire)
    {
        if (!GameStateEvents.IsRunActive)
            return;

        Transform? campfireTarget = campfire switch
        {
            Campfire.Shore => MapObjectRefs.CampfireShore,
            Campfire.Tropics => MapObjectRefs.CampfireTropics,
            Campfire.AlpineMesa => SegmentManager.IsAlpine ? MapObjectRefs.CampfireAlpine : MapObjectRefs.CampfireMesa,
            Campfire.Caldera => MapObjectRefs.CampfireCaldera,
            Campfire.PeakFlagpole => MapObjectRefs.PeakFlagPole,
            _ => throw new ArgumentOutOfRangeException(nameof(campfire), campfire, null)
        };

        if (!ReferenceEquals(campfireTarget, null))
        {
            foreach (var character in PlayerHandler.GetAllPlayerCharacters())
                character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, campfireTarget.position + _offset, false);
        }
    }
}