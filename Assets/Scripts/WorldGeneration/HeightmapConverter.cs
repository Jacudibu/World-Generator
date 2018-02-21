using UnityEngine;

namespace WorldGeneration
{
    public static class HeightmapConverter
    {
        public static Texture2D ToTexture2D(Heightmap heightmap)
        {
            var texture = new Texture2D(heightmap.Width, heightmap.Height, TextureFormat.ARGB32, false);

            for (int y = 0; y < heightmap.Height; y++)
            {
                for (int x = 0; x < heightmap.Width; x++)
                {
                    float value = heightmap.GetAt(x, y);
                    texture.SetPixel(x, y, new Color(value, value, value));;
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();
        
            return texture;
        }
        
        public static Mesh ToMesh(Heightmap heightmap)
        {
            Mesh mesh = new Mesh();

            return mesh;
        }
    }
}