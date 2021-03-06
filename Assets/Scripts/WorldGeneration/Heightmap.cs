﻿using System;
using UnityEngine;
using Utility;

namespace WorldGeneration
{
    public class Heightmap : ICloneable
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

        public void SetAt(Point point, float value)
        {
            SetAt(point.X, point.Y, value);
        }

        public void SetAt(int x, int y, float value)
        {
            Values[x + y * Width] = value;
        }

        public float GetAt(Point point)
        {
            return GetAt(point.X, point.Y);
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

        public void Invert()
        {
            float min = GetMin();
            float max = GetMax();
            
            for (int i = 0; i < Values.Length; i++)
            {
                float current = Values[i];
                Values[i] = max - current + min;
            }
        }

        public void Smooth(int weighting = 1)
        {
            Heightmap smoothedMap = new Heightmap(Width, Height);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    SmoothAt(smoothedMap, x, y, weighting);
                }
            }

            Values = smoothedMap.Values;
        }
        
        private void SmoothAt(Heightmap smoothedMap, int x, int y, int weighting)
        {
            float sum = 0;
            float count = 0;

            for (int ySurroundings = y - 1; ySurroundings <= y + 1; ySurroundings++)
            {
                for (int xSurroundings = x - 1; xSurroundings <= x + 1; xSurroundings++)
                {
                    if (!IsWithinBounds(xSurroundings, ySurroundings))
                    {
                        continue;
                    }

                    sum += GetAt(xSurroundings, ySurroundings);
                    count++;
                }
            }

            sum += (weighting - 1) * GetAt(x, y);
            count += weighting - 1;
            
            float smoothedValue = sum / count;
            smoothedMap.SetAt(x, y, smoothedValue);            
        }

        public void AddToAllValues(float amount)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] = Mathf.Clamp01(Values[i] + amount);
            }
        }

        private bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        private float GetMax()
        {
            float max = float.NegativeInfinity;

            foreach (float value in Values)
            {
                if (value > max)
                {
                    max = value;
                }
            }

            return max;
        }
        
        private float GetMin()
        {
            float min = float.PositiveInfinity;

            foreach (float value in Values)
            {
                if (value < min)
                {
                    min = value;
                }
            }

            return min;
        }

        public object Clone()
        {
            var clone = new Heightmap(Width, Height)
                            {
                                Values = Values.Clone() as float[]
                            };

            return clone;
        }

        public void Add(Heightmap other)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] += other.Values[i];
            }
        }

        public void Substract(Heightmap other)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                Values[i] -= other.Values[i];
            }
        }

        public void RotateClockwise()
        {
            var original = Clone() as Heightmap;
            System.Diagnostics.Debug.Assert(original != null);

            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    SetAt(x, y, original.GetAt(y, Height - x - 1));
                }
            }
        }

        public void RotateCounterClockwise()
        {
            var original = Clone() as Heightmap;
            System.Diagnostics.Debug.Assert(original != null);

            for (int x = 0; x < Height; x++)
            {
                for (int y = 0; y < Width; y++)
                {
                    SetAt(x, y, original.GetAt(Width - y - 1, x));
                }
            }
        }
        
        
    }
}