using System.IO;
using System.Text;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace UnityEditor.BehaveGraph
{
    
    [ScriptedImporter(1, Extension, -900)]
    public class BehaveGraphImporter : ScriptedImporter
    {
        public const string Extension = "behavegraph";
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string path = ctx.assetPath;
            TextAsset mainAsset = new TextAsset(File.ReadAllText(ctx.assetPath));
            Texture2D texture = Resources.Load<Texture2D>("Icons/ase64");
            if (texture == null)
            {
                Debug.LogError("No Icon");
            }
            ctx.AddObjectToAsset("MainAsset", mainAsset, texture);
            ctx.SetMainObject(mainAsset);
        }
    }
}

