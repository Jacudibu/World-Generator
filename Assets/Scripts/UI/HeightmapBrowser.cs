using System;
using System.Collections.Generic;
using UnityEngine;
using WorldGeneration;

namespace UI
{
    public class HeightmapBrowser : MonoBehaviour
    {
        [SerializeField] private GameObject _MenuItemPrefab;
        [SerializeField] private Transform _MenuItemSpawnParent;
        
        private List<HeightmapBrowserItem> _items = new List<HeightmapBrowserItem>();

        public void CreateNewItem(Heightmap heightmap, String name)
        {
            var newItem = GameObject.Instantiate(_MenuItemPrefab);
            newItem.transform.SetParent(_MenuItemSpawnParent);
            newItem.name = name;
            newItem.SetActive(true);
            
            var browserItem = newItem.GetComponent<HeightmapBrowserItem>();
            browserItem.Init(heightmap, name);

            _items.Add(browserItem);
        }

        public void RemoveItem(HeightmapBrowserItem item)
        {
            _items.Remove(item);
        }

        public Heightmap CombineAll()
        {
            foreach (var item in _items)
            {
            }

            return null;
        }
    }
}