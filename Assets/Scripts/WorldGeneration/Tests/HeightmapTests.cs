using NUnit.Framework;

namespace WorldGeneration.Tests
{
	public class HeightmapTests
	{
		[Test]
		public void TestSetAt() 
		{
			Heightmap map = new Heightmap(4, 4);
			
			map.SetAt(0, 0, 42);
			Assert.AreEqual(42, map.Values[0], "Error at 0/0");
			
			map.SetAt(3, 3, 16);
			Assert.AreEqual(16, map.Values[15], "Error at 3/3");
			
			map.SetAt(2, 2, 22);
			Assert.AreEqual(22, map.Values[10], "Error at 2/2");			
		}

		[Test]
		public void TestGetAt()
		{
			Heightmap map = new Heightmap(4, 4);
			map.SetAt(0, 0, 42);
			Assert.AreEqual(map.GetAt(0, 0), 42, "Error at 0/0");
			
			map.SetAt(3, 3, 16);
			Assert.AreEqual(map.GetAt(3, 3), 16, "Error at 3/3");
			
			map.SetAt(2, 2, 22);
			Assert.AreEqual(map.GetAt(2, 2), 22, "Error at 2/2");
		}

		[Test]
		public void TestNormalize()
		{
			Heightmap map = new Heightmap(4, 4);
			map.SetAt(0, 0, -42);
			map.SetAt(3, 3, 42);
			map.SetAt(2, 2, 0);
			
			map.Normalize();
			
			Assert.AreEqual(0, map.GetAt(0, 0), "Error at 0/0");
			Assert.AreEqual(1, map.GetAt(3, 3), "Error at 3/3");
			Assert.AreEqual(0.5, map.GetAt(2, 2), "Error at 2/2");
		}

		[Test]
		public void TestSmooth()
		{
			Heightmap map = new Heightmap(4, 4);
			map.SetAt(0, 0, 0);
			map.SetAt(1, 0, 1);
			map.SetAt(0, 1, 1);
			map.Smooth();

			Assert.AreEqual(2f / 4f, map.GetAt(0, 0), "Error at 0/0");
			Assert.AreEqual(2f / 6f, map.GetAt(1, 0), "Error at 1/0");
			Assert.AreEqual(2f / 6f, map.GetAt(0, 1), "Error at 0/1");
			Assert.AreEqual(2f / 9f, map.GetAt(1, 1), "Error at 1/1");
		}

		[Test]
		public void TestSmoothWithWeighting()
		{
			Heightmap map = new Heightmap(4, 4);
			map.SetAt(0, 0, 0);
			map.SetAt(1, 0, 1);
			map.SetAt(0, 1, 1);
			map.Smooth(2);

			Assert.AreEqual(2f / 5f, map.GetAt(0, 0), "Error at 0/0");
			Assert.AreEqual(3f / 7f, map.GetAt(1, 0), "Error at 1/0");
			Assert.AreEqual(3f / 7f, map.GetAt(0, 1), "Error at 0/1");
			Assert.AreEqual(2f / 10f, map.GetAt(1, 1), "Error at 1/1");
		}
	}
}
