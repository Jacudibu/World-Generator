using UnityEngine;

namespace WorldGeneration
{
    public class Heightmap
    {

        public readonly int Width;
        public readonly int Height;
        
        public float[] Values { get; private set; }
        public bool IsNormalized { get; private set; }
        
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
            if (IsNormalized)
            {
                return;
            }
            
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

            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = (Values[i] - min) / (max - min);
            }

            IsNormalized = true;
        }
        
        public Texture2D ToTexture2D()
        {
            var texture = new Texture2D(Width, Height, TextureFormat.ARGB32, false);

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    float value = GetAt(x, y);
                    texture.SetPixel(x, y, new Color(value, value, value));;
                }
            }

            texture.filterMode = FilterMode.Point;
            texture.Apply();
            
            return texture;
        }

        public void Smooth()
        {
            Heightmap smoothedMap = new Heightmap(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SmoothAt(smoothedMap, x, y);
                }
            }

            Values = smoothedMap.Values;
        }

        private void SmoothAt(Heightmap smoothedMap, int x, int y)
        {
            float sum = 0;
            float count = 0;

            for (int someY = y - 1; someY < y + 1; someY++)
            {
                for (int someX = x - 1; someX < x + 1; someX++)
                {
                    if (!IsWithinBounds(someX, someY))
                    {
                        continue;
                    }

                    sum += GetAt(someX, someY);
                    count++;
                }
            }

            float smoothedValue = sum / count;
            smoothedMap.SetAt(x, y, smoothedValue);            
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x <= Width && y >= 0 && y <= Height;
        }
    }
}