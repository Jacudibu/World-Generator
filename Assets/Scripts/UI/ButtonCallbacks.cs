using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;
using WorldGeneration.HeightmapGenerators;

namespace UI
{
    public class ButtonCallbacks : MonoBehaviour
    {
        public Image Image;

        private Heightmap _heightmap;
        private Texture2D _texture;

        public int Size = 128;

        private Mesh _terrainMesh;
        [SerializeField]
        private MeshFilter _terrainMeshFilter;

        public void GenerateHeightmap_DiamondSquare()
        {
            _heightmap = DiamondSquare.Generate(Size);
            ApplyTexture();
            GenerateMesh();
        }

        public void GenerateHeightmap_CellNoise()
        {
            _heightmap = CellNoise.Generate(Size, Size, 32);
            ApplyTexture();
            GenerateMesh();
        }

        public void SmoothHeightmap()
        {
            _heightmap.Smooth(3);
            ApplyTexture();
            GenerateMesh();
        }

        public void NormalizeHeightmap()
        {
            _heightmap.Normalize();
            ApplyTexture();
            GenerateMesh();
        }

        public void InvertHeightmap()
        {
            _heightmap.Invert();
            ApplyTexture();
            GenerateMesh();
        }

        public void SaveHeightmap()
        {
        }
        
        private void GenerateMesh()
        {
            _terrainMesh = HeightmapConverter.ToMesh(_heightmap);
            _terrainMeshFilter.mesh = _terrainMesh;
        }
    
        private void ApplyTexture()
        {
            _texture = HeightmapConverter.ToTexture2D(_heightmap);
            var rect = new Rect(0, 0, _texture.width, _texture.height);
            Image.sprite = Sprite.Create(_texture, rect, Vector2.one * 0.5f);
            Image.SetNativeSize();
        }
    }
}
