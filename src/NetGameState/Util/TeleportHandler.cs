using System;
using ConsoleTools;
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
}

internal static class TeleportHandler
{
    private static Transform? _shoreCampfire;
    private static Transform? _tropicsCampfire;
    private static Transform? _alpineMesaCampfire;
    private static Transform? _calderaCampfire;

    private static bool IsInitialized { get; set; }

    private static Vector3 _offset = new(0, 10, 0);

    internal static void Init()
    {
        var go = GameObject.Find(MapObjectPaths.CampfireShore);
        if (ReferenceEquals(go, null))
            Plugin.Log.LogColorW("Could not find 'Shore Campfire'");
        _shoreCampfire = go?.transform;
        go = null;

        go = GameObject.Find(MapObjectPaths.CampfireTropics);
        if (ReferenceEquals(go, null))
            Plugin.Log.LogColorW("Could not find 'Tropics Campfire'");
        _tropicsCampfire = go?.transform;
        go = null;
        
        // Determine Alpine Mesa Campfire
        go = GameObject.Find(MapObjectPaths.BioAlpine);
        if (!ReferenceEquals(go, null))
        {
            if (go.activeSelf)
            {
                go = GameObject.Find(MapObjectPaths.CampfireAlpine);
                if (ReferenceEquals(go, null))
                    Plugin.Log.LogColorW("Could not find 'Alpine Campfire'");
                _alpineMesaCampfire = go?.transform;
                go = null;
            }
            else
            {
                go = GameObject.Find(MapObjectPaths.CampfireMesa);
                if (ReferenceEquals(go, null))
                    Plugin.Log.LogColorW("Could not find 'Mesa Campfire'");
                _alpineMesaCampfire = go?.transform;
                go = null;
            }
        }
        else
        {
            Plugin.Log.LogColorW("Could not find 'Alpine/Mesa Campfire'");
        }

        go = GameObject.Find(MapObjectPaths.CampfireCaldera);
        if (ReferenceEquals(go, null))
            Plugin.Log.LogColorW("Could not find 'Caldera Campfire'");
        _calderaCampfire = go?.transform;
        go = null;

        IsInitialized = true;
    }

    internal static void Reset()
    {
        _shoreCampfire = null;
        _tropicsCampfire = null;
        _alpineMesaCampfire = null;
        _calderaCampfire = null;
        IsInitialized = false;
    }

    internal static void TeleportToCampfire(Campfire campfire)
    {
        if (!IsInitialized)
            return;
        
        Transform? campfireTarget;
        switch (campfire)
        {
            case Campfire.Shore: campfireTarget = _shoreCampfire; break;
            case Campfire.Tropics: campfireTarget = _tropicsCampfire; break;
            case Campfire.AlpineMesa: campfireTarget = _alpineMesaCampfire; break;
            case Campfire.Caldera: campfireTarget = _calderaCampfire; break;
            default: throw new ArgumentOutOfRangeException(nameof(campfire), campfire, null);
        }

        if (!ReferenceEquals(campfireTarget, null))
        {
            foreach (var character in PlayerHandler.GetAllPlayerCharacters())
                character.photonView.RPC("WarpPlayerRPC", RpcTarget.All, campfireTarget.position + _offset, false);
        }
    }
}