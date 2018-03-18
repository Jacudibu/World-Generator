using System;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;
using WorldGeneration.HeightmapGenerators;

namespace UI
{
    public class NewMapCallbacks : MonoBehaviour
    {     
        [SerializeField] private InputField _sizeXInput;
        [SerializeField] private InputField _sizeYInput;
        [SerializeField] private InputField _cellCountInput;

        private int _sizeX;
        private int _sizeY;
        private int _cellCount;

        private int _diamondMapsGenerated = 0;
        private int _cellMapsGenerated = 0;
        
        public void GenerateHeightmap_DiamondSquare()
        {
            ParseInputFields();
            var heightmap = DiamondSquare.Generate(_sizeX);
            CreateNewMenuItem(heightmap, "DiamondSquare_" + ++_diamondMapsGenerated);
        }

        public void GenerateHeightmap_CellNoise()
        {
            ParseInputFields();
            var heightmap = CellNoise.Generate(_sizeX, _sizeY, _cellCount);
            CreateNewMenuItem(heightmap, "CellNoise_" + ++_cellMapsGenerated);
        }

        private void CreateNewMenuItem(Heightmap heightmap, String name)
        {
            FindObjectOfType<HeightmapBrowser>().CreateNewItem(heightmap, name);
        }
        
        private void ParseInputFields()
        {
            _sizeX = int.Parse(_sizeXInput.text);
            _sizeY = int.Parse(_sizeYInput.text);
            _cellCount = int.Parse(_cellCountInput.text);
        }
    }
}