using NetGameState.Types;

namespace NetGameState.MapRefs;

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
    public const string BioTropics = $"{Biome2}/Tropics";
    public const string BioRoots = $"{Biome2}/Roots";
    public const string BioAlpine = $"{Biome3}/Alpine";
    public const string BioMesa = $"{Biome3}/Mesa";
    public const string BioVolcano = $"{Biome4}/Volcano";
    
    // --- Segments ---
    public const string SegShore = $"{BioShore}/Beach_Segment";
    public const string SegTropics = $"{BioTropics}/Jungle_Segment";
    public const string SegRoots = $"{BioRoots}/Roots Segment";                         // space?..
    public const string SegAlpine = $"{BioAlpine}/Snow_Segment";
    public const string SegMesa =  $"{BioMesa}/Desert_Segment";
    public const string SegCaldera = $"{BioVolcano}/Caldera_Segment";
    public const string SegKiln = $"{BioVolcano}/Volcano_Segment";
    public const string SegPeak = $"{BioVolcano}/Peak";
    
    // --- Campfire Areas ---
    public const string CampAreaShore = $"{BioShore}/Beach_Campfire";
    public const string CampAreaTropics = $"{BioTropics}/Jungle_Campfire";
    public const string CampAreaRoots = $"{BioRoots}/Roots_Campfire";
    public const string CampAreaAlpine = $"{BioAlpine}/Snow_Campfire";
    public const string CampAreaMesa =  $"{BioMesa}/Desert_Campfire/Snow_Campfire";     // snow in desert campfire
    public const string CampAreaCaldera = $"{BioVolcano}/Volcano_Campfire";
    
    // --- Campfire Spawners ---
    public const string CampSpawnerShore = $"{CampAreaShore}/Campfire";
    public const string CampSpawnerTropics = $"{CampAreaTropics}/Campfire";
    public const string CampSpawnerRoots = $"{CampAreaRoots}/Campfire";
    public const string CampSpawnerAlpine = $"{CampAreaAlpine}/Campfire";
    public const string CampSpawnerMesa = $"{CampAreaMesa}/Campfire";
    public const string CampSpawnerCaldera = $"{CampAreaCaldera}/Campfire";
    
    // --- Campfires ---
    public const string CampfireShore = $"{CampSpawnerShore}/Campfire";         // INFO: might not exist in object tree
    public const string CampfireTropics = $"{CampSpawnerTropics}/Campfire";     // INFO: might not exist in object tree
    public const string CampfireRoots = $"{CampSpawnerRoots}/Campfire";         // INFO: might not exist in object tree
    public const string CampfireAlpine = $"{CampSpawnerAlpine}/Campfire";       // INFO: might not exist in object tree
    public const string CampfireMesa = $"{CampSpawnerMesa}/Campfire";           // INFO: might not exist in object tree
    public const string CampfireCaldera = $"{CampSpawnerCaldera}/Campfire-Kiln";// INFO: might not exist in object tree
    
    
    // --- Biome Variants: Shore ---
    public const string SubBioShoreDefault = $"{SegShore}/Default";
    public const string SubBioShoreSnakeBeach = $"{SegShore}/SnakeBeach";
    public const string SubBioShoreRedBeach = $"{SegShore}/RedBeach";
    public const string SubBioShoreBlueBeach = $"{SegShore}/BlueBeach";
    public const string SubBioShoreJellyHell = $"{SegShore}/JellyHell";
    public const string SubBioShoreBlackSand = $"{SegShore}/BlackSand";
    
    // --- Biome Variants: Tropics ---
    public const string SubBioTropicsDefault =  $"{SegTropics}/Default";
    public const string SubBioTropicsLava =  $"{SegTropics}/Lava";
    public const string SubBioTropicsPillars = $"{SegTropics}/Pillars";
    public const string SubBioTropicsThorny = $"{SegTropics}/Thorny";
    public const string SubBioTropicsBombs = $"{SegTropics}/Bombs";
    public const string SubBioTropicsIvy = $"{SegTropics}/Ivy";
    public const string SubBioTropicsSkyJungle = $"{SegTropics}/SkyJungle";
    
    // --- Biome Variants: Alpine ---
    public const string SubBioAlpineDefault = $"{SegAlpine}/Default";
    public const string SubBioAlpineLava = $"{SegAlpine}/Lava";
    public const string SubBioAlpineSpiky  = $"{SegAlpine}/Spiky";
    public const string SubBioAlpineGeyserHell = $"{SegAlpine}/GeyserHell";

    // --- Peak ---
    public const string PeakFlagPole = $"{SegPeak}/Flag_planted_seagull/Flag Pole";
    
    
    // --- Helper Methods ---
    public static bool TryGetBiomeRoot(NgsBiome biome, out string biomeRoot)
    {
        switch (biome)
        {
            case NgsBiome.Shore: biomeRoot = BioShore; return true;
            case NgsBiome.Tropics: biomeRoot = BioTropics; return true;
            case NgsBiome.Roots: biomeRoot = BioRoots; return true;
            case NgsBiome.Alpine: biomeRoot = BioAlpine; return true;
            case NgsBiome.Mesa: biomeRoot = BioMesa; return true;
            case NgsBiome.Caldera: biomeRoot = BioVolcano; return true;
            case NgsBiome.Kiln: biomeRoot = BioVolcano; return true;
            case NgsBiome.Peak: biomeRoot = BioVolcano; return true;
            default: biomeRoot = ""; return false;
        }
    }

    public static bool TryGetSegmentRoot(NgsBiome biome, out string segmentRoot)
    {
        switch (biome)
        {
            case NgsBiome.Shore: segmentRoot = SegShore; return true;
            case NgsBiome.Tropics: segmentRoot = SegTropics; return true;
            case NgsBiome.Roots: segmentRoot = SegRoots; return true;
            case NgsBiome.Alpine: segmentRoot = SegAlpine; return true;
            case NgsBiome.Mesa: segmentRoot = SegMesa; return true;
            case NgsBiome.Caldera: segmentRoot = SegCaldera; return true;
            case NgsBiome.Kiln: segmentRoot = SegKiln; return true;
            case NgsBiome.Peak: segmentRoot = SegPeak; return true;
            default: segmentRoot = ""; return false;
        }
    }

    public static bool TryGetCampAreaRoot(NgsBiome ngsBiome, out string campAreaRoot)
    {
        switch (ngsBiome)
        {
            case NgsBiome.Shore: campAreaRoot = CampAreaShore; return true;
            case NgsBiome.Tropics: campAreaRoot = CampAreaTropics; return true;
            case NgsBiome.Roots: campAreaRoot = CampAreaRoots; return true;
            case NgsBiome.Alpine: campAreaRoot = CampAreaAlpine; return true;
            case NgsBiome.Mesa: campAreaRoot = CampAreaMesa; return true;
            case NgsBiome.Caldera: campAreaRoot = CampAreaCaldera; return true;
            default: campAreaRoot = ""; return false;
        }
    }

    public static bool TryGetCampSpawnerRoot(NgsBiome ngsBiome, out string campfireSpawnerRoot)
    {
        switch (ngsBiome)
        {
            case NgsBiome.Shore: campfireSpawnerRoot = CampSpawnerShore; return true;
            case NgsBiome.Tropics: campfireSpawnerRoot = CampSpawnerTropics; return true;
            case NgsBiome.Roots: campfireSpawnerRoot = CampSpawnerRoots; return true;
            case NgsBiome.Alpine: campfireSpawnerRoot = CampSpawnerAlpine; return true;
            case NgsBiome.Mesa: campfireSpawnerRoot = CampSpawnerMesa; return true;
            case NgsBiome.Caldera: campfireSpawnerRoot = CampSpawnerCaldera; return true;
            default: campfireSpawnerRoot = ""; return false;
        }
    }

    public static bool TryGetCampfireRoot(NgsBiome ngsBiome, out string campfireRoot)
    {
        switch (ngsBiome)
        {
            case NgsBiome.Shore: campfireRoot = CampfireShore; return true;     // INFO: might not exist in object tree
            case NgsBiome.Tropics: campfireRoot = CampfireTropics; return true; // INFO: might not exist in object tree
            case NgsBiome.Roots: campfireRoot = CampfireRoots; return true;     // INFO: might not exist in object tree
            case NgsBiome.Alpine: campfireRoot = CampfireAlpine; return true;   // INFO: might not exist in object tree
            case NgsBiome.Mesa: campfireRoot = CampfireMesa; return true;       // INFO: might not exist in object tree
            case NgsBiome.Caldera: campfireRoot = CampfireCaldera; return true; // INFO: might not exist in object tree
            default: campfireRoot = ""; return false;
        }
    }

    public static bool TryGetBiomeVariantRoot(NgsBiomeVariant ngsBiomeVariant, out string biomeVariantRoot)
    {
        switch (ngsBiomeVariant)
        {
            // Shore biome variants
            case NgsBiomeVariant.Shore_Default: biomeVariantRoot = SubBioShoreDefault; return true;
            case NgsBiomeVariant.Shore_SnakeBeach: biomeVariantRoot = SubBioShoreSnakeBeach; return true;
            case NgsBiomeVariant.Shore_RedBeach: biomeVariantRoot = SubBioShoreRedBeach; return true;
            case NgsBiomeVariant.Shore_BlueBeach: biomeVariantRoot = SubBioShoreBlueBeach; return true;
            case NgsBiomeVariant.Shore_JellyHell: biomeVariantRoot = SubBioShoreJellyHell; return true;
            case NgsBiomeVariant.Shore_BlackSand: biomeVariantRoot = SubBioShoreBlackSand; return true;

            // Tropics biome variants
            case NgsBiomeVariant.Tropics_Default: biomeVariantRoot = SubBioTropicsDefault; return true;
            case NgsBiomeVariant.Tropics_Lava: biomeVariantRoot = SubBioTropicsLava; return true;
            case NgsBiomeVariant.Tropics_Pillars: biomeVariantRoot = SubBioTropicsPillars; return true;
            case NgsBiomeVariant.Tropics_Thorny: biomeVariantRoot = SubBioTropicsThorny; return true;
            case NgsBiomeVariant.Tropics_Bombs: biomeVariantRoot = SubBioTropicsBombs; return true;
            case NgsBiomeVariant.Tropics_Ivy: biomeVariantRoot = SubBioTropicsIvy; return true;
            case NgsBiomeVariant.Tropics_SkyJungle: biomeVariantRoot = SubBioTropicsSkyJungle; return true;

            // Alpine biome variants
            case NgsBiomeVariant.Alpine_Default: biomeVariantRoot = SubBioAlpineDefault; return true;
            case NgsBiomeVariant.Alpine_Lava: biomeVariantRoot = SubBioAlpineLava; return true;
            case NgsBiomeVariant.Alpine_Spiky: biomeVariantRoot = SubBioAlpineSpiky; return true;
            case NgsBiomeVariant.Alpine_GeyserHell: biomeVariantRoot = SubBioAlpineGeyserHell; return true;
            
            default: biomeVariantRoot = ""; return false;
        }
    }
}