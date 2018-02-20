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

    public void GenerateHeightmap_DiamondSquare()
    {
        heightmap = DiamondSquare.Generate(1024);
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
    
    private void ApplyTexture()
    {
        texture = heightmap.ToTexture2D();
        var rect = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        image.SetNativeSize();
    }
}
