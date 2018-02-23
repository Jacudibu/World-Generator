using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;

namespace Visualization
{
    public class VisualizeUI : MonoBehaviour
    {
        public Image Image;

        private Heightmap heightmap;
        private Texture2D texture;

        public int Size = 128;

        private Mesh terrainMesh;
        [SerializeField]
        private MeshFilter terrainMeshFilter;

        public void GenerateHeightmap_DiamondSquare()
        {
            heightmap = DiamondSquare.Generate(Size);
            ApplyTexture();
        }

        public void SmoothHeightmap()
        {
            heightmap.Smooth();
            ApplyTexture();
        }

        public void NormalizeHeightmap()
        {
            heightmap.Normalize();
            ApplyTexture();
        }

        public void GenerateMesh()
        {
            terrainMesh = HeightmapConverter.ToMesh(heightmap);
            terrainMeshFilter.mesh = terrainMesh;
        }
    
        private void ApplyTexture()
        {
            texture = HeightmapConverter.ToTexture2D(heightmap);
            var rect = new Rect(0, 0, texture.width, texture.height);
            Image.sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
            Image.SetNativeSize();
        }
    }
}
