using System.Linq;
using ArtifactOfDrift.Artifacts.Base;
using ArtifactOfDrift.Util;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using Run = On.RoR2.Run;
using SceneDirector = On.RoR2.SceneDirector;

namespace ArtifactOfDrift.Artifacts;

public static class DriftArtifact
{
    private static Artifact Drift;

    private const string Name = "Artifact of Drift";
    private const string Description = "Every stage is randomized.";
    private const string LunarTeleporterKey =
        "RoR2/Base/Teleporters/iscLunarTeleporter.asset";
    
    private static bool Enabled => RunArtifactManager.instance.IsArtifactEnabled(Drift.Def.artifactIndex);

    public static void Initialize()
    {
        Drift = new Artifact(Name, Description);

        Hooks();
    }
    
    
    private static void Hooks()
    {
        Run.PickNextStageScene += RunOnPickNextStageScene;
        SceneDirector.PlaceTeleporter += SceneDirectorOnPlaceTeleporter;
    }

    private static void SceneDirectorOnPlaceTeleporter(SceneDirector.orig_PlaceTeleporter orig, RoR2.SceneDirector self)
    {
        if (Enabled && (RoR2.Run.instance.stageClearCount + 1) % 5 == 0)
        {
            var pt = Addressables
                .LoadAssetAsync<InteractableSpawnCard>(LunarTeleporterKey).WaitForCompletion();
            self.teleporterSpawnCard = pt;
        }
        
        orig(self);
    }

    private static void RunOnPickNextStageScene(Run.orig_PickNextStageScene orig, RoR2.Run self, WeightedSelection<SceneDef> choices)
    {
        var validStages = SceneCatalog.allStageSceneDefs
            .Where(stage => stage.validForRandomSelection)
            .Where(stage => stage.requiredExpansion == null || self.IsExpansionEnabled(stage.requiredExpansion))
            .ToArray();

        if (Enabled)
        {
            choices = new WeightedSelection<SceneDef>();
            foreach (var stage in validStages)
            {
                choices.AddChoice(stage, 1f);
            }
        }

        orig(self, choices);
    }
}