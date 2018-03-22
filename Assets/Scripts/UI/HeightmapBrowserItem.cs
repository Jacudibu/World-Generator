using System;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;

namespace UI
{
    public class HeightmapBrowserItem : MonoBehaviour
    {
        private Heightmap _heightmapOriginal;
        public Heightmap HeightmapModified { get; private set; }

        [SerializeField] private Image _image;
        [SerializeField] private InputField _name;
        [SerializeField] private Slider _strengthSlider;
        [SerializeField] private InputField _strengthSliderInput;

        private HeightmapBrowser _associatedBrowser;

        public void Init(Heightmap map, String name, HeightmapBrowser associatedBrowser)
        {
            _associatedBrowser = associatedBrowser;
            _heightmapOriginal = map;
            
            UpdateHeightmap();
        }
        
        public void OnSliderValueChanged()
        {
            _strengthSliderInput.text = _strengthSlider.value.ToString("F");

            UpdateHeightmap();
        }

        public void OnInputValueChanged()
        {
            float value;
            bool parsed = float.TryParse(_strengthSliderInput.text, out value);

            if (parsed)
            {
                _strengthSlider.value = value;
            }
            
            UpdateHeightmap();
        }

        public void OnInputValueEndEdit()
        {
            float value;
            bool parsed = float.TryParse(_strengthSliderInput.text, out value);

            if (parsed)
            {
                _strengthSlider.value = value;
            }
            else
            {
                _strengthSlider.value = 1;
                _strengthSliderInput.text = "1";
            }

            UpdateHeightmap();
        }

        public void Button_Invert()
        {
            _heightmapOriginal.Invert();
            UpdateHeightmap();
        }

        public void Button_Smooth()
        {
            _heightmapOriginal.Smooth();
            UpdateHeightmap();
        }

        public void Button_Remove()
        {
            _associatedBrowser.RemoveItem(this);
            Destroy(gameObject);
        }

        private void UpdateHeightmap()
        {
            UpdateClone();
            UpdateSprite();
            _associatedBrowser.UpdateVisualization();
        }
        
        private void UpdateSprite()
        {
            UpdateClone();
            _image.sprite = HeightmapConverter.ToSprite(HeightmapModified);
        }

        private void UpdateClone()
        {
            HeightmapModified = _heightmapOriginal.Clone() as Heightmap;
            System.Diagnostics.Debug.Assert(HeightmapModified != null, "HeightmapModified != null");

            Debug.Log(_strengthSlider.value);
            HeightmapModified.AddToAllValues(_strengthSlider.value);
        }
    }
}