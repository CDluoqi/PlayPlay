using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

namespace UnityEditor.BehaveGraph
{
    internal struct FuncEntry
    {
        public string name;
    }
    
    class FunctionSearchWindowProvider
    {
        public bool regenerateEntries { get; set; }
        
        public Dictionary<string, List<FuncEntry>> currentFuncEntries  = new Dictionary<string, List<FuncEntry>>();
        
        public void Initialize()
        {
            GenerateFuncEntries();
        }
        
        public Searcher.Searcher LoadSearchWindow()
        {
            if (regenerateEntries)
            {
                GenerateFuncEntries();
                regenerateEntries = false;
            }
            var root = new List<SearcherItem>();

            foreach (var pair in currentFuncEntries)
            {
                var classItem =  new SearcherItem(pair.Key);
                root.Add(classItem);
                foreach (var child in pair.Value)
                {
                    classItem.AddChild(new SearcherItem(child.name));
                }
            }
            var nodeDatabase = SearcherDatabase.Create(root, string.Empty, false);
            
            return new Searcher.Searcher(nodeDatabase, new SearcherAdapter("Find Function"));
        }
        
        void GenerateFuncEntries()
        {
            foreach (var pair in NPBehaveFunctionCache.KnownFunctionLookupTable)
            {
                List<FuncEntry> funcEntries = new List<FuncEntry>();
                currentFuncEntries.Add(pair.Key.ToString(), funcEntries);
                foreach (var funcAttribute in pair.Value)
                {
                    funcEntries.Add(new FuncEntry()
                    {
                        name = funcAttribute.Name
                    });
                }
            }
        }

        public bool OnSearcherSelectEntry(SearcherItem entry)
        {

            return true;
        }
    }
}

