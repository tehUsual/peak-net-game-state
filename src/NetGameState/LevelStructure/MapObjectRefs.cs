using System;
using System.Reflection;
using ConsoleTools;
using UnityEngine;
using System.Linq;
using NetGameState.Events;

namespace NetGameState.LevelStructure;

public static class MapObjectRefs
{
    private static int _initCount;
    private const int TotalTransformFields = 39;    // could not get reflection to work
    
    // --- Biomes ---
    public static Transform? BioShore { get; private set; }
    public static Transform? BioTropics { get; private set; }
    public static Transform? BioAlpine { get; private set; }
    public static Transform? BioMesa { get; private set; }
    public static Transform? BioVolcano { get; private set; }

    // --- Segments ---
    public static Transform? SegShore { get; private set; }
    public static Transform? SegTropics { get; private set; }
    public static Transform? SegAlpine { get; private set; }
    public static Transform? SegMesa { get; private set; }
    public static Transform? SegCaldera { get; private set; }
    public static Transform? SegKiln { get; private set; }
    public static Transform? SegPeak { get; private set; }

    // --- Campfire Areas ---
    public static Transform? CampAreaShore { get; private set; }
    public static Transform? CampAreaTropics { get; private set; }
    public static Transform? CampAreaAlpine { get; private set; }
    public static Transform? CampAreaMesa { get; private set; }
    public static Transform? CampAreaCaldera { get; private set; }

    // --- Campfires ---
    public static Transform? CampfireShore { get; private set; }
    public static Transform? CampfireTropics { get; private set; }
    public static Transform? CampfireAlpine { get; private set; }
    public static Transform? CampfireMesa { get; private set; }
    public static Transform? CampfireCaldera { get; private set; }
    
    // --- Sub Zone: Shore ---
    public static Transform? SubBioShoreDefault { get; private set; }
    public static Transform? SubBioShoreSnakeBeach { get; private set; }
    public static Transform? SubBioShoreRedBeach { get; private set; }
    public static Transform? SubBioShoreBlueBeach { get; private set; }
    public static Transform? SubBioShoreJellyHell { get; private set; }
    public static Transform? SubBioShoreBlackSand { get; private set; }
    
    // --- Sub Zone: Tropics ---
    public static Transform? SubBioTropicsDefault { get; private set; }
    public static Transform? SubBioTropicsLava { get; private set; }
    public static Transform? SubBioTropicsPillars { get; private set; }
    public static Transform? SubBioTropicsThorny { get; private set; }
    public static Transform? SubBioTropicsBombs { get; private set; }
    public static Transform? SubBioTropicsIvy { get; private set; }
    public static Transform? SubBioTropicsSkyJungle { get; private set; }
    
    // --- Sub Zone: Alpine ---
    public static Transform? SubBioAlpineDefault { get; private set; }
    public static Transform? SubBioAlpineLava { get; private set; }
    public static Transform? SubBioAlpineSpiky { get; private set; }
    public static Transform? SubBioAlpineGeyserHell { get; private set; }

    
    static MapObjectRefs()
    {
        // TODO: On run ended
        GameStateEvents.OnAirportLoaded += Reset;
        GameStateEvents.OnSelfLeaveLobby += Reset;
    }
    
    /// <summary>
    /// Finds all map objects and stores their references.
    /// </summary>
    internal static void Init()
    {
        // Biomes
        BioShore = Find(MapObjectPaths.BioShore);
        BioTropics = Find(MapObjectPaths.BioTropics);
        BioAlpine = Find(MapObjectPaths.BioAlpine);
        BioMesa = Find(MapObjectPaths.BioMesa);
        BioVolcano = Find(MapObjectPaths.BioVolcano);

        // Segments
        SegShore = Find(MapObjectPaths.SegShore);
        SegTropics = Find(MapObjectPaths.SegTropics);
        SegAlpine = Find(MapObjectPaths.SegAlpine);
        SegMesa = Find(MapObjectPaths.SegMesa);
        SegCaldera = Find(MapObjectPaths.SegCaldera);
        SegKiln = Find(MapObjectPaths.SegKiln);
        SegPeak = Find(MapObjectPaths.SegPeak);

        // Campfire Areas
        CampAreaShore = Find(MapObjectPaths.CampAreaShore);
        CampAreaTropics = Find(MapObjectPaths.CampAreaTropics);
        CampAreaAlpine = Find(MapObjectPaths.CampAreaAlpine);
        CampAreaMesa = Find(MapObjectPaths.CampAreaMesa);
        CampAreaCaldera = Find(MapObjectPaths.CampAreaCaldera);

        // Campfires
        CampfireShore = Find(MapObjectPaths.CampfireShore);
        CampfireTropics = Find(MapObjectPaths.CampfireTropics);
        CampfireAlpine = Find(MapObjectPaths.CampfireAlpine);
        CampfireMesa = Find(MapObjectPaths.CampfireMesa);
        CampfireCaldera = Find(MapObjectPaths.CampfireCaldera);
        
        // Sub Zone: Shore
        SubBioShoreDefault = Find(MapObjectPaths.SubBioShoreDefault);
        SubBioShoreSnakeBeach = Find(MapObjectPaths.SubBioShoreSnakeBeach);
        SubBioShoreRedBeach = Find(MapObjectPaths.SubBioShoreRedBeach);
        SubBioShoreBlueBeach = Find(MapObjectPaths.SubBioShoreBlueBeach);
        SubBioShoreJellyHell = Find(MapObjectPaths.SubBioShoreJellyHell);
        SubBioShoreBlackSand = Find(MapObjectPaths.SubBioShoreBlackSand);
        
        // Sub Zone: Tropics
        SubBioTropicsDefault = Find(MapObjectPaths.SubBioTropicsDefault);
        SubBioTropicsLava = Find(MapObjectPaths.SubBioTropicsLava);
        SubBioTropicsPillars = Find(MapObjectPaths.SubBioTropicsPillars);
        SubBioTropicsThorny = Find(MapObjectPaths.SubBioTropicsThorny);
        SubBioTropicsBombs = Find(MapObjectPaths.SubBioTropicsBombs);
        SubBioTropicsIvy = Find(MapObjectPaths.SubBioTropicsIvy);
        SubBioTropicsSkyJungle = Find(MapObjectPaths.SubBioTropicsSkyJungle);
        
        // Sub Zone: Alpine
        SubBioAlpineDefault = Find(MapObjectPaths.SubBioAlpineDefault);
        SubBioAlpineLava = Find(MapObjectPaths.SubBioAlpineLava);
        SubBioAlpineSpiky = Find(MapObjectPaths.SubBioAlpineSpiky);
        SubBioAlpineGeyserHell = Find(MapObjectPaths.SubBioAlpineGeyserHell);
        
        // Log init status
        if (_initCount == TotalTransformFields)
            Plugin.Log.LogColorS($"All map object references initialized: {_initCount}/{TotalTransformFields}");
        else
            Plugin.Log.LogColorW($"Not all map object references initialized: {_initCount}/{TotalTransformFields}");
    }

    /// <summary>
    /// Clears all references back to null.
    /// </summary>
    internal static void Reset()
    {
        // Could not get foreach reflection reset to work
        _initCount = 0;
        
        BioShore = null;
        BioTropics = null;
        BioAlpine = null;
        BioMesa = null;
        BioVolcano = null;
        
        SegShore = null;
        SegTropics = null;
        SegAlpine = null;
        SegMesa = null;
        SegCaldera = null;
        SegKiln = null;
        SegPeak = null;
        
        CampAreaShore = null;
        CampAreaTropics = null;
        CampAreaAlpine = null;
        CampAreaMesa = null;
        CampAreaCaldera = null;
        
        CampfireShore = null;
        CampfireTropics = null;
        CampfireAlpine = null;
        CampfireMesa = null;
        CampfireCaldera = null;
        
        SubBioShoreDefault = null;
        SubBioShoreSnakeBeach = null;
        SubBioShoreRedBeach = null;
        SubBioShoreBlueBeach = null;
        SubBioShoreJellyHell = null;
        SubBioShoreBlackSand = null;
        
        SubBioTropicsDefault = null;
        SubBioTropicsLava = null;
        SubBioTropicsPillars = null;
        SubBioTropicsThorny = null;
        SubBioTropicsBombs = null;
        SubBioTropicsIvy = null;
        SubBioTropicsSkyJungle = null;
        
        SubBioAlpineDefault = null;
        SubBioAlpineLava = null;
        SubBioAlpineSpiky = null;
        SubBioAlpineGeyserHell = null;
    }

    private static Transform? Find(string path)
    {
        Transform? transform = GameObject.Find(path)?.transform;
        if (ReferenceEquals(transform, null))
            Plugin.Log.LogColor($"Could not find transform for: {path}");
        else
            _initCount++;
        
        return GameObject.Find(path)?.transform;
    }
}