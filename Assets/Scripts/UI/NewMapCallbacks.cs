using System;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;
using WorldGeneration.HeightmapGenerators;

namespace UI
{
    public class NewMapCallbacks : MonoBehaviour
    {     
        [Header("Diamond Square")]
        [SerializeField] private GameObject _diamondSquareWindow;
        [SerializeField] private InputField _diamondSquareInputSize;
        [SerializeField] private InputField _diamondSquareInputNoiseFactor;
        [SerializeField] private InputField _diamondSquareInputNoiseFalloff;

        [Header("Cell Noise")]
        [SerializeField] private GameObject _cellNoiseWindow;
        [SerializeField] private InputField _cellNoiseInputSizeX;
        [SerializeField] private InputField _cellNoiseInputSizeY;
        [SerializeField] private InputField _cellNoiseInputCellCount;
        [SerializeField] private InputField _cellNoiseInputFalloff;

        private int _diamondSquareSize;
        private float _diamondSquareNoiseFactor;
        private float _diamondSquareNoiseFalloff;
        
        private int _cellNoiseSizeX;
        private int _cellNoiseSizeY;
        private int _cellNoiseCellCount;
        private float _cellNoiseFalloff;

        private int _diamondMapsGenerated = 0;
        private int _cellMapsGenerated = 0;

        public void Button_OpenWindow_DiamondSquare()
        {
            CloseAllSubWindwos();
            _diamondSquareWindow.SetActive(true);
        }

        public void Button_OpenWindow_CellNoise()
        {
            CloseAllSubWindwos();
            _cellNoiseWindow.SetActive(true);
        }
        
        public void Button_GenerateHeightmap_DiamondSquare()
        {
            ParseInputFields();
            var heightmap = DiamondSquare.Generate(_diamondSquareSize, _diamondSquareNoiseFactor, _diamondSquareNoiseFalloff);
            CreateNewMenuItem(heightmap, "DiamondSquare_" + ++_diamondMapsGenerated);
        }

        public void Button_GenerateHeightmap_CellNoise()
        {
            ParseInputFields();
            var heightmap = CellNoise.Generate(_cellNoiseSizeX, _cellNoiseSizeY, _cellNoiseCellCount, _cellNoiseFalloff);
            CreateNewMenuItem(heightmap, "CellNoise_" + ++_cellMapsGenerated);
        }

        private void CreateNewMenuItem(Heightmap heightmap, string name)
        {
            FindObjectOfType<HeightmapBrowser>().CreateNewItem(heightmap, name);
        }
        
        private void ParseInputFields()
        {
            _diamondSquareSize = int.Parse(_diamondSquareInputSize.text);
            _diamondSquareNoiseFactor = float.Parse(_diamondSquareInputNoiseFactor.text);
            _diamondSquareNoiseFalloff= float.Parse(_diamondSquareInputNoiseFalloff.text);
            
            _cellNoiseSizeX = int.Parse(_cellNoiseInputSizeX.text);
            _cellNoiseSizeY = int.Parse(_cellNoiseInputSizeY.text);
            _cellNoiseCellCount = int.Parse(_cellNoiseInputCellCount.text);
            _cellNoiseFalloff = float.Parse(_cellNoiseInputFalloff.text);
        }

        private void CloseAllSubWindwos()
        {
            _diamondSquareWindow.SetActive(false);
            _cellNoiseWindow.SetActive(false);
        }
    }
}