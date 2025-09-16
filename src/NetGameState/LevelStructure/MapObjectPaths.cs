using NetGameState.Level;

namespace NetGameState.LevelStructure;

public static class MapObjectPaths
{
    // --- Map Root ---
    public const string MapRoot = "Map";

    // --- Biomes ---
    private const string Biome1 = $"{MapRoot}/Biome_1";
    private const string Biome2 = $"{MapRoot}/Biome_2";
    private const string Biome3 = $"{MapRoot}/Biome_3";
    private const string Biome4 = $"{MapRoot}/Biome_4";
    
    // --- Biome Types ---
    public const string BioShore = $"{Biome1}/Beach";
    public const string BioTropics = $"{Biome2}/Jungle";
    public const string BioAlpine = $"{Biome3}/Snow";
    public const string BioMesa = $"{Biome3}/Desert";
    public const string BioVolcano = $"{Biome4}/Volcano";
    
    // --- Segments ---
    public const string SegShore = $"{BioShore}/Beach_Segment";
    public const string SegTropics = $"{BioTropics}/Jungle_Segment";
    public const string SegAlpine = $"{BioAlpine}/Snow_Segment";
    public const string SegMesa =  $"{BioMesa}/Desert_Segment";
    public const string SegCaldera = $"{BioVolcano}/Caldera_Segment";
    public const string SegKiln = $"{BioVolcano}/Volcano_Segment";
    public const string SegPeak = $"{BioVolcano}/Peak";
    
    // --- Campfire Areas ---
    public const string CampAreaShore = $"{BioShore}/Beach_Campfire";
    public const string CampAreaTropics = $"{BioTropics}/Jungle_Campfire";
    public const string CampAreaAlpine = $"{BioAlpine}/Snow_Campfire";
    public const string CampAreaMesa =  $"{BioMesa}/Desert_Campfire/Snow_Campfire";
    public const string CampAreaCaldera = $"{BioVolcano}/Volcano_Campfire";
    
    // --- Campfire ---
    public const string CampfireShore = $"{CampAreaShore}/Campfire/Campfire";
    public const string CampfireTropics = $"{CampAreaTropics}/Campfire/Campfire";
    public const string CampfireAlpine = $"{CampAreaAlpine}/Campfire/Campfire";
    public const string CampfireMesa = $"{CampAreaMesa}/Campfire/Campfire";
    public const string CampfireCaldera = $"{CampAreaCaldera}/Campfire/Campfire-Kiln";
    
    
    // --- Sub Zone: Shore ---
    public const string SubBioShoreDefault = $"{SegShore}/Default";
    public const string SubBioShoreSnakeBeach = $"{SegShore}/SnakeBeach";
    public const string SubBioShoreRedBeach = $"{SegShore}/RedBeach";
    public const string SubBioShoreBlueBeach = $"{SegShore}/BlueBeach";
    public const string SubBioShoreJellyHell = $"{SegShore}/JellyHell";
    public const string SubBioShoreBlackSand = $"{SegShore}/BlackSand";
    
    // --- Sub Zone: Tropics ---
    public const string SubBioTropicsDefault =  $"{SegTropics}/Default";
    public const string SubBioTropicsLava =  $"{SegTropics}/Lava";
    public const string SubBioTropicsPillars = $"{SegTropics}/Pillars";
    public const string SubBioTropicsThorny = $"{SegTropics}/Thorny";
    public const string SubBioTropicsBombs = $"{SegTropics}/Bombs";
    public const string SubBioTropicsIvy = $"{SegTropics}/Ivy";
    public const string SubBioTropicsSkyJungle = $"{SegTropics}/SkyJungle";
    
    // --- Sub Zone: Alpine ---
    public const string SubBioAlpineDefault = $"{SegAlpine}/Default";
    public const string SubBioAlpineLava = $"{SegAlpine}/Lava";
    public const string SubBioAlpineSpiky  = $"{SegAlpine}/Spiky";
    public const string SubBioAlpineGeyserHell = $"{SegAlpine}/GeyserHell";

    // --- Peak ---
    public const string PeakFlagPole = $"{SegPeak}/Flag_planted_seagull/Flag Pole";
    
    
    // --- Helper Methods ---
    public static bool TryGetBiomeRoot(Zone biome, out string biomeRoot)
    {
        switch (biome)
        {
            case Zone.Shore: biomeRoot = BioShore; return true;
            case Zone.Tropics: biomeRoot = BioTropics; return true;
            case Zone.Alpine: biomeRoot = BioAlpine; return true;
            case Zone.Mesa: biomeRoot = BioMesa; return true;
            case Zone.Caldera: biomeRoot = BioVolcano; return true;
            case Zone.Kiln: biomeRoot = BioVolcano; return true;
            case Zone.Peak: biomeRoot = BioVolcano; return true;
            default: biomeRoot = ""; return false;
        }
    }

    public static bool TryGetSegmentRoot(Zone biome, out string segmentRoot)
    {
        switch (biome)
        {
            case Zone.Shore: segmentRoot = SegShore; return true;
            case Zone.Tropics: segmentRoot = SegTropics; return true;
            case Zone.Alpine: segmentRoot = SegAlpine; return true;
            case Zone.Mesa: segmentRoot = SegMesa; return true;
            case Zone.Caldera: segmentRoot = SegCaldera; return true;
            case Zone.Kiln: segmentRoot = SegKiln; return true;
            case Zone.Peak: segmentRoot = SegPeak; return true;
            default: segmentRoot = ""; return false;
        }
    }

    public static bool TryGetCampAreaRoot(Zone zone, out string campAreaRoot)
    {
        switch (zone)
        {
            case Zone.Shore: campAreaRoot = CampAreaShore; return true;
            case Zone.Tropics: campAreaRoot = CampAreaTropics; return true;
            case Zone.Alpine: campAreaRoot = CampAreaAlpine; return true;
            case Zone.Mesa: campAreaRoot = CampAreaMesa; return true;
            case Zone.Caldera: campAreaRoot = CampAreaCaldera; return true;
            default: campAreaRoot = ""; return false;
        }
    }

    public static bool TryGetCampfireRoot(Zone zone, out string campfireRoot)
    {
        switch (zone)
        {
            case Zone.Shore: campfireRoot = CampfireShore; return true;
            case Zone.Tropics: campfireRoot = CampfireTropics; return true;
            case Zone.Alpine: campfireRoot = CampfireAlpine; return true;
            case Zone.Mesa: campfireRoot = CampfireMesa; return true;
            case Zone.Caldera: campfireRoot = CampfireCaldera; return true;
            default: campfireRoot = ""; return false;
        }
    }

    public static bool TryGetSubZoneRoot(SubZone subZone, out string subZoneRoot)
    {
        switch (subZone)
        {
            // Shore sub zones
            case SubZone.Shore_Default: subZoneRoot = SubBioShoreDefault; return true;
            case SubZone.Shore_SnakeBeach: subZoneRoot = SubBioShoreSnakeBeach; return true;
            case SubZone.Shore_RedBeach: subZoneRoot = SubBioShoreRedBeach; return true;
            case SubZone.Shore_BlueBeach: subZoneRoot = SubBioShoreBlueBeach; return true;
            case SubZone.Shore_JellyHell: subZoneRoot = SubBioShoreJellyHell; return true;
            case SubZone.Shore_BlackSand: subZoneRoot = SubBioShoreBlackSand; return true;

            // Tropics sub zones
            case SubZone.Tropics_Default: subZoneRoot = SubBioTropicsDefault; return true;
            case SubZone.Tropics_Lava: subZoneRoot = SubBioTropicsLava; return true;
            case SubZone.Tropics_Pillars: subZoneRoot = SubBioTropicsPillars; return true;
            case SubZone.Tropics_Thorny: subZoneRoot = SubBioTropicsThorny; return true;
            case SubZone.Tropics_Bombs: subZoneRoot = SubBioTropicsBombs; return true;
            case SubZone.Tropics_Ivy: subZoneRoot = SubBioTropicsIvy; return true;
            case SubZone.Tropics_SkyJungle: subZoneRoot = SubBioTropicsSkyJungle; return true;

            // Alpine sub zones
            case SubZone.Alpine_Default: subZoneRoot = SubBioAlpineDefault; return true;
            case SubZone.Alpine_Lava: subZoneRoot = SubBioAlpineLava; return true;
            case SubZone.Alpine_Spiky: subZoneRoot = SubBioAlpineSpiky; return true;
            case SubZone.Alpine_GeyserHell: subZoneRoot = SubBioAlpineGeyserHell; return true;
            
            default: subZoneRoot = ""; return false;
        }
    }
}