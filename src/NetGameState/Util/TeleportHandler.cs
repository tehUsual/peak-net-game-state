using System;
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

        Transform? campfireTarget;
        switch (campfire)
        {
            case Campfire.Shore:
                campfireTarget = GameObject.Find(MapObjectPaths.CampfireShore)?.transform;
                if (ReferenceEquals(campfireTarget, null))
                    campfireTarget = MapObjectRefs.CampSpawnerShore;
                break;
                
            case Campfire.TropicsRoots:
                if (SegmentManager.IsTropics)
                {
                    campfireTarget = GameObject.Find(MapObjectPaths.CampfireTropics)?.transform;
                    if (ReferenceEquals(campfireTarget, null))
                        campfireTarget = MapObjectRefs.CampSpawnerTropics;
                }
                else
                {
                    campfireTarget = GameObject.Find(MapObjectPaths.CampfireRoots)?.transform;
                    if (ReferenceEquals(campfireTarget, null))
                        campfireTarget = MapObjectRefs.CampSpawnerRoots;
                }
                break;

            case Campfire.AlpineMesa:
                if (SegmentManager.IsAlpine)
                {
                    campfireTarget = GameObject.Find(MapObjectPaths.CampfireAlpine)?.transform;
                    if (ReferenceEquals(campfireTarget, null))
                        campfireTarget = MapObjectRefs.CampSpawnerAlpine;
                }
                else
                {
                    campfireTarget = GameObject.Find(MapObjectPaths.CampfireMesa)?.transform;
                    if (ReferenceEquals(campfireTarget, null))
                        campfireTarget = MapObjectRefs.CampSpawnerMesa;
                }
                break;
            
            case Campfire.Caldera:
                campfireTarget = GameObject.Find(MapObjectPaths.CampfireCaldera)?.transform;
                if (ReferenceEquals(campfireTarget, null))
                    campfireTarget = MapObjectRefs.CampSpawnerCaldera;
                break;
            
            case Campfire.PeakFlagpole:
                campfireTarget = GameObject.Find(MapObjectPaths.PeakFlagPole)?.transform;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(campfire), campfire, null);
                
        }

        if (!ReferenceEquals(campfireTarget, null))
        {
            foreach (var character in PlayerHandler.GetAllPlayerCharacters())
                character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, campfireTarget.position + Offset, false);
        }
    }
}