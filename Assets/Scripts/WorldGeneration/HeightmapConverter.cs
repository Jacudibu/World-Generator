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
                    texture.SetPixel(x, y, new Color(value, value, value));;
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();
        
            return texture;
        }
        
        public static Mesh ToMesh(Heightmap heightmap)
        {
            var mesh = new Mesh();

            mesh.vertices = GenerateVertices(heightmap);
            mesh.triangles = GenerateTriangles(heightmap);
            mesh.RecalculateNormals();
            
            return mesh;
        }

        private static Vector3[] GenerateVertices(Heightmap heightmap)
        {
            var vertices = new Vector3[heightmap.Values.Length];

            int i = 0;
            for (int y = 0; y < heightmap.Height; y++)
            {
                for (int x = 0; x < heightmap.Width; x++)
                {
                    float value = heightmap.GetAt(x, y);
                    var vertice = new Vector3(x * DistanceBetweenVertices,
                                              heightmap.GetAt(x, y) * MaxHeight,
                                              y * DistanceBetweenVertices);

                    vertices[i] = vertice;
                    i++;
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
                    triangles[i++] = x + y * heightmap.Width + 1;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width + 1;

                    triangles[i++] = x + y * heightmap.Width;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width + 1;
                    triangles[i++] = x + y * heightmap.Width + heightmap.Width;
                }
            }

            return triangles;
        }
    }
}