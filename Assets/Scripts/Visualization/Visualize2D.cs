using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;

public class Visualize2D : MonoBehaviour
{
    public Image image;
    
    private Texture2D texture;

    public void RenderDiamondSquare()
    {
        Heightmap map = DiamondSquare.Generate(256);
        ApplyTexture(map.ToTexture2D());
    }
    
    private void ApplyTexture(Texture2D texture)
    {
        var rect = new Rect(0, 0, texture.width, texture.height);
        image.sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        image.SetNativeSize();
    }
}
