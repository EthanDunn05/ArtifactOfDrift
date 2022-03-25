using System.IO;
using ArtifactOfDrift.Util;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using Path = System.IO.Path;

namespace ArtifactOfDrift.Artifacts.Base;

[R2APISubmoduleDependency(nameof(ContentAddition))]
public class Artifact
{
    public ArtifactDef Def = ScriptableObject.CreateInstance<ArtifactDef>();

    public Artifact(string name, string description)
    {
        BasePath = "Artifacts" + "/" + name;
        
        Def.nameToken = name;
        Def.descriptionToken = description;
        
        Def.smallIconSelectedSprite = Assets.LoadAsset<Sprite>($"{BasePath}/enabled.png") 
                                      ?? throw new FileNotFoundException();
        Def.smallIconDeselectedSprite = Assets.LoadAsset<Sprite>($"{BasePath}/disabled.png") 
                                        ?? throw new FileNotFoundException();
        
        ContentAddition.AddArtifactDef(Def);
    }
    
    public string BasePath { get; }
}