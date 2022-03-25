using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace ArtifactOfDrift.Util;

// This class is really funky but I don't care to improve it
public static class Assets
{
    public const string AssetFolder = "Assets";

    public static void Init()
    {
        // Return the path without the dll file at the end
        // This is so incredibly over-engineered
        var folder = ArtifactOfDrift.PInfo.Location.Split('\\')
            .Aggregate("", (str, next) => {
                if (next.Contains(".dll"))
                    return str;
                return $"{str}\\{next}";
            }, str => str);
        folder = folder.Substring(1);

        var path = Path.Combine(folder, AssetFolder.ToLower());
        Bundle = AssetBundle.LoadFromFile(path);
    }
    
    public static AssetBundle Bundle { get; private set; }
    
    #nullable enable
    public static T? LoadAsset<T> (string file) where T : Object
    {
        var path = $"{AssetFolder}/{file}";
        Log.LogInfo("Loading asset at: " + path);
        return Bundle.LoadAsset<T>(path);
    }
}