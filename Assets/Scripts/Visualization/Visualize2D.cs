using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;

public class Visualize2D : MonoBehaviour
{
    public Image image;

    private Heightmap heightmap;
    private Texture2D texture;

    public int size = 128;

    public Mesh mesh;

    public void GenerateHeightmap_DiamondSquare()
    {
        heightmap = DiamondSquare.Generate(size);
        ApplyTexture();
    }

    public void SmoothHeightmap()
    {
        heightmap.Smooth();
        ApplyTexture();
    }

    public void NormalizeHeightmap()
    {
        heightmap.Normalize();
        ApplyTexture();
    }

    public void GenerateMesh()
    {
        mesh = HeightmapConverter.ToMesh(heightmap);
    }
    
    private void ApplyTexture()
    {
        texture = HeightmapConverter.ToTexture2D(heightmap);
        var rect = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        image.SetNativeSize();
    }
}
