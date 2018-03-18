using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace WorldGeneration.HeightmapGenerators
{
    public static class CellNoise
    {
        private static int _height;
        private static int _width;
        private static float _falloff;
        private static Point[] _points;
        
        public static Heightmap Generate(int height, int width, int cellAmount, float falloff = 2f)
        {
            _height = height;
            _width = width;
            _falloff = falloff;
            GeneratePoints(cellAmount);


            var map = GenerateAndFillHeightmap();
            map.Normalize();

            return map;
        }

        private static void GeneratePoints(int cellAmount)
        {
            _points = new Point[cellAmount];

            for (int i = 0; i < _points.Length; i++)
            {
                int x = Random.Range(0, _width);
                int y = Random.Range(0, _height);
                
                _points[i] = new Point(x, y);
            }
        }
        
        private static Heightmap GenerateAndFillHeightmap()
        {
            var map = new Heightmap(_width, _height);

            Point point;
            for (point.Y = 0; point.Y < _height; point.Y++)
            {
                for (point.X = 0; point.X < _width; point.X++)
                {
                    float value = GetDistanceToClosestPoint(point);
                    value = Mathf.Pow(value, _falloff);
                    map.SetAt(point.X, point.Y, value);
                }
            }

            return map;
        }

        private static float GetDistanceToClosestPoint(Point point)
        {
            float distance = float.PositiveInfinity;
            
            foreach (var checkedPoint in _points)
            {
                float currentDistance = point.Distance(checkedPoint);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                }
            }

            return distance;
        }
    }
}