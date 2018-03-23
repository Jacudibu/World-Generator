using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace WorldGeneration
{
    public static class HeightmapConverter
    {
        private const float MaxHeight = 50f;
        private const float DistanceBetweenVertices = 1f;
        
        public static Texture2D ToTexture2D(Heightmap heightmap)
        {
            var texture = new Texture2D(heightmap.Width, heightmap.Height, TextureFormat.ARGB32, false);

            for (int y = 0; y < heightmap.Height; y++)
            {
                for (int x = 0; x < heightmap.Width; x++)
                {
                    float value = heightmap.GetAt(x, y);
                    texture.SetPixel(x, heightmap.Height - y - 1, new Color(value, value, value));
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();
        
            return texture;
        }

        public static Sprite ToSprite(Heightmap map)
        {
            var texture = ToTexture2D(map);
            var rect = new Rect(0, 0, texture.width, texture.height);
            return Sprite.Create(texture, rect, Vector2.one * 0.5f);
        }
        
        
        public static Mesh ToMesh(Heightmap heightmap)
        {
            var mesh = new Mesh
                       {
                           vertices = GenerateVertices(heightmap),
                           triangles = GenerateTriangles(heightmap),
                           uv = GenerateUVs(heightmap)
                       };

            mesh.RecalculateNormals();
            
            return mesh;
        }

        public static void ToFile(Heightmap heightmap, string path)
        {
            var texture = ToTexture2D(heightmap);
            File.WriteAllBytes(path, texture.EncodeToPNG());
        }
        
        private static Vector3[] GenerateVertices(Heightmap heightmap)
        {
            var vertices = new Vector3[heightmap.Values.Length];

            int i = 0;
            for (int y = 0; y < heightmap.Height; y++)
            {
                for (int x = 0; x < heightmap.Width; x++)
                {
                    var vertice = new Vector3(x * DistanceBetweenVertices,
                                              heightmap.GetAt(x, y) * MaxHeight,
                                              y * DistanceBetweenVertices);

                    vertices[i++] = vertice;
                }
            }

            return vertices;
        }

        private static int[] GenerateTriangles(Heightmap heightmap)
        {
            var triangles = new int[(heightmap.Width - 1) * (heightmap.Height - 1) * 2 * 3];

            int i = 0;
            for (int y = 0; y < heightmap.Height - 1; y++)
            {
                for (int x = 0; x < heightmap.Width - 1; x++)
                {
                    triangles[i++] = x + y * heightmap.Width;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width + 1;
                    triangles[i++] = x + y * heightmap.Width + 1;

                    triangles[i++] = x + y * heightmap.Width;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width + 1;
                }
            }

            return triangles;
        }

        private static Vector2[] GenerateUVs(Heightmap heightmap)
        {
            var uvs = new Vector2[heightmap.Values.Length];

            int i = 0;
            for (int y = 0; y < heightmap.Height; y++)
            {
                for (int x = 0; x < heightmap.Width; x++)
                {
                    var uv = new Vector2((float) x / heightmap.Width, (float) y / heightmap.Width);
                    uvs[i++] = uv;
                }
            }

            return uvs;
        }
    }
}