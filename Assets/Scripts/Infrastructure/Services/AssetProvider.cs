using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, GameObject> _assets = new Dictionary<string, GameObject>();
        
        public GameObject Instantiate(string path)
        {
            return Object.Instantiate(LoadObject(path));
        }
        
        public GameObject Instantiate(string path, Vector3 position)
        {
            return Object.Instantiate(LoadObject(path), position, Quaternion.identity);
        }

        private GameObject LoadObject(string path)
        {
            GameObject obj;
            if (_assets.ContainsKey(path))
            {
                obj = _assets[path];
            }
            else
            {
                obj =  Resources.Load(path) as GameObject;
                _assets.Add(path, obj);
            }

            return obj;
        }
    }
}
