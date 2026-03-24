using System;
using ConsoleTools;
using NetGameState.Events;
using NetGameState.LevelProgression;
using NetGameState.LevelStructure;
using Photon.Pun;
using UnityEngine;

namespace NetGameState.Util;

public enum Campfire
{
    Shore,
    TropicsRoots,
    AlpineMesa,
    Caldera,
    PeakFlagpole,
}

public static class TeleportHandler
{
    private static readonly Vector3 Offset = new(0, 10, 0);

    public static void TeleportToCampfire(Campfire campfire)
    {
        if (!GameStateEvents.IsRunActive)
            return;

        Transform? campfireTarget = campfire switch
        {
            Campfire.Shore => MapObjectRefs.CampfireShore,
            Campfire.TropicsRoots => SegmentManager.IsTropics ? MapObjectRefs.CampfireTropics : MapObjectRefs.CampfireRoots,
            Campfire.AlpineMesa => SegmentManager.IsAlpine ? MapObjectRefs.CampfireAlpine : MapObjectRefs.CampfireMesa,
            Campfire.Caldera => MapObjectRefs.CampfireCaldera,
            Campfire.PeakFlagpole => MapObjectRefs.PeakFlagPole,
            _ => throw new ArgumentOutOfRangeException(nameof(campfire), campfire, null)
        };

        if (!ReferenceEquals(campfireTarget, null))
        {
            foreach (var character in PlayerHandler.GetAllPlayerCharacters())
                character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, campfireTarget.position + Offset, false);
        }
    }
}