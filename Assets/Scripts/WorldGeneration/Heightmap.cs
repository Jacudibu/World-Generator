using UnityEngine;

namespace WorldGeneration
{
    public class Heightmap
    {
        public readonly float[] Values;

        public readonly int Width;
        public readonly int Height;
        
        public Heightmap(int width, int height)
        {
            Width = width;
            Height = height;

            Values = new float[width * height];
        }

        public void SetAt(int x, int y, float value)
        {
            Values[x + y * Width] = value;
        }

        public float GetAt(int x, int y)
        {
            return Values[x + y * Width];
        }

        public void Normalize()
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (float value in Values)
            {
                if (value < min)
                {
                    min = value;
                }

                if (value > max)
                {
                    max = value;
                }
            }

            if (min == 0f && max == 1f)
            {
                return;
            }
            
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = (Values[i] - min) / (max - min);
            }
        }

        public Texture2D ToTexture2D()
        {
            var texture = new Texture2D(Width, Height, TextureFormat.ARGB32, false);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    float value = GetAt(x, y);
                    texture.SetPixel(x, y, new Color(value, value, value));;
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();
            
            return texture;
        }
        
    }
}