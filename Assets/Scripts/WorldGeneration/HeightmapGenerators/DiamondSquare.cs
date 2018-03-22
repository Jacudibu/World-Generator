using UnityEngine;
using Random = UnityEngine.Random;

namespace WorldGeneration.HeightmapGenerators
{
	public static class DiamondSquare
	{
		private static Heightmap _map;

		private static int _size;
		private static int _distance;

		private static float _randomNoiseFactor = 0.5f;
		private static float _randomNoiseFalloff = 0.75f;
		
		public static Heightmap Generate(int size, float randomNoiseFactor = 0.5f, float randomNoiseFalloff = 0.75f)
		{
			_size = size + 1;
			int depth = (int) Mathf.Log(size, 2) - 1;
			
			InitializeArray();
			
			_randomNoiseFactor = randomNoiseFactor;
			_randomNoiseFalloff = randomNoiseFalloff;
			
			while (depth >= 0)
			{
				_distance = (int) Mathf.Pow(2, depth);
				
				DiamondStep();
				SquareStep();

				depth--;
				_randomNoiseFactor *= _randomNoiseFalloff;
			}

			_map.Normalize();
			// reduce size by one again
			return TrimByOne(_map);
		}

		private static void InitializeArray()
		{
			_map = new Heightmap(_size, _size);
		
			_map.SetAt(0, 0, GetRandomValue());
			_map.SetAt(0, _size - 1, GetRandomValue());
			_map.SetAt(_size - 1, 0, GetRandomValue());
			_map.SetAt(_size - 1, _size - 1, GetRandomValue());
		}

		private static void DiamondStep()
		{
			for (int y = _distance; y < _size; y += _distance * 2)
			{
				for (int x = _distance; x < _size; x += _distance * 2)
				{
					PerformDiamondStepAt(x, y);
				}
			}
		}

		private static void PerformDiamondStepAt(int x, int y)
		{
			float diamondSum = 0;
			diamondSum += _map.GetAt(x - _distance, y - _distance);
			diamondSum += _map.GetAt(x + _distance, y - _distance);
			diamondSum += _map.GetAt(x - _distance, y + _distance);
			diamondSum += _map.GetAt(x + _distance, y + _distance);
			
			_map.SetAt(x, y, diamondSum * 0.25f + GetRandomValue());
		}

		private static void SquareStep()
		{
			for (int y = _distance; y < _size; y += _distance * 2)
			{
				for (int x = 0; x < _size; x += _distance * 2)
				{
					PerformSquareStep(x, y);
				}
			}
			
			for (int y = 0; y < _size; y += _distance * 2)
			{
				for (int x =_distance; x < _size; x += _distance * 2)
				{
					PerformSquareStep(x, y);
				}
			}
		}

		private static void PerformSquareStep(int x, int y)
		{
			float squareSum = 0f;
			float factor = 0.25f;

			// Left
			if (x - _distance >= 0)
			{
				squareSum += _map.GetAt(x - _distance, y);
			}
			else
			{
				factor = 1f / 3f;
			}
			
			// Right
			if (x + _distance < _size)
			{
				squareSum += _map.GetAt(x + _distance, y);
			}
			else
			{
				factor = 1f / 3f;
			}
			
			// Below
			if (y - _distance >= 0)
			{
				squareSum += _map.GetAt(x, y - _distance);
			}
			else
			{
				factor = 1f / 3f;
			}
			
			// Above
			if (y + _distance < _size)
			{
				squareSum += _map.GetAt(x, y + _distance);
			}
			else
			{
				factor = 1f / 3f;
			}
			
			_map.SetAt(x, y, squareSum * factor + GetRandomValue());
				
		}

		private static Heightmap TrimByOne(Heightmap heightmap)
		{
			var result = new Heightmap(heightmap.Width - 1, heightmap.Height - 1);
			for (int y = 0; y < result.Height; y++)
			{
				for (int x = 0; x < result.Width; x++)
				{
					result.SetAt(x, y, heightmap.GetAt(x, y));
				}
			}

			return result;
		}

		private static float GetRandomValue()
		{
			return Random.Range(-_randomNoiseFactor, _randomNoiseFactor);
		}
	}
}
