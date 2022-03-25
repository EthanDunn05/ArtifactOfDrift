using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using ArtifactOfDrift.Artifacts;
using ArtifactOfDrift.Util;

namespace ArtifactOfDrift
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class ArtifactOfDrift : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "AcidAssassin";
        public const string PluginName = "ArtifactOfDrift";
        public const string PluginVersion = "1.0.0";
        
        public static PluginInfo PInfo { get; private set; }
        
        public void Awake()
        {
            Log.Init(Logger);
            PInfo = Info;

            Assets.Init();
            
            DriftArtifact.Initialize();
            
            Log.LogInfo(nameof(Awake) + " done.");
        }
    }
}