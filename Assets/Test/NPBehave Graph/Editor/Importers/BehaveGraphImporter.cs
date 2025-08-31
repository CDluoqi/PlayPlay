using System.IO;
using System.Text;
using UnityEditor.AssetImporters;
using UnityEditor.BehaveGraph.Serialization;
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

            var graph = new GraphData();
            MultiJson.Deserialize(graph, mainAsset.text);
            graph.OnEnable();
            graph.ValidateGraph();

            
            ctx.AddObjectToAsset("MainAsset", mainAsset, texture);
            ctx.AddObjectToAsset("texture", texture);
            ctx.SetMainObject(mainAsset);
        }
    }
}

