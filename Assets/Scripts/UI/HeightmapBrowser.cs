using System;
using System.Collections.Generic;
using UnityEngine;
using WorldGeneration;

namespace UI
{
    public class HeightmapBrowser : MonoBehaviour
    {
        [SerializeField] private GameObject _menuItemPrefab;
        [SerializeField] private Transform _menuItemSpawnParent;

        [SerializeField] private Texture2D _texture;
        [SerializeField] private UnityEngine.UI.Image _image;
        [SerializeField] private MeshFilter _terrainMeshFilter;
        
        private readonly List<HeightmapBrowserItem> _items = new List<HeightmapBrowserItem>();

        public void CreateNewItem(Heightmap heightmap, string name)
        {
            var newItem = GameObject.Instantiate(_menuItemPrefab);
            newItem.transform.SetParent(_menuItemSpawnParent);
            newItem.name = name;
            newItem.SetActive(true);
            
            var browserItem = newItem.GetComponent<HeightmapBrowserItem>();
            _items.Add(browserItem);
            
            browserItem.Init(heightmap, name, this);
        }

        public void RemoveItem(HeightmapBrowserItem item)
        {
            _items.Remove(item);
            UpdateVisualization();
        }

        public void UpdateVisualization()
        {
            var heightmap = CombineAll();
            GenerateMesh(heightmap);
            ApplyTexture(heightmap);
        }

        private Heightmap CombineAll()
        {
            if (_items.Count == 0)
            {
                return null;
            }
            
            var result = new Heightmap(_items[0].HeightmapModified.Width, _items[0].HeightmapModified.Height);

            foreach (var heightmap in _items)
            {
                if (!heightmap.IsVisible)
                {
                    continue;
                }

                if (heightmap.CurrentMode == HeightmapBrowserItem.Mode.Add)
                {
                    result.Add(heightmap.HeightmapModified);
                }
                else
                {
                    result.Substract(heightmap.HeightmapModified);
                }
            }

            return result;
        }
        
        private void GenerateMesh(Heightmap heightmap)
        {
            var terrainMesh = HeightmapConverter.ToMesh(heightmap);
            _terrainMeshFilter.mesh = terrainMesh;
        }
    
        private void ApplyTexture(Heightmap heightmap)
        {
            heightmap.Normalize();
            _texture = HeightmapConverter.ToTexture2D(heightmap);
            var rect = new Rect(0, 0, _texture.width, _texture.height);
            _image.sprite = Sprite.Create(_texture, rect, Vector2.one * 0.5f);
            _image.SetNativeSize();
        }
    }
}