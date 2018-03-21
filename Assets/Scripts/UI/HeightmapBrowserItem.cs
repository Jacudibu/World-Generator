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

        public void Init(Heightmap map, String name)
        {
            // TODO: Add to List
            
            _heightmapOriginal = map;
            UpdateSprite();
        }
        
        public void OnSliderValueChanged()
        {
            _strengthSliderInput.text = _strengthSlider.value.ToString("F");
            
            UpdateSprite();
        }

        public void OnInputValueChanged()
        {
            float value;
            bool parsed = float.TryParse(_strengthSliderInput.text, out value);

            if (parsed)
            {
                _strengthSlider.value = value;
            }
            
            UpdateSprite();
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
            
            UpdateSprite();
        }

        public void Button_Invert()
        {
            _heightmapOriginal.Invert();
            UpdateSprite();
        }

        public void Button_Smooth()
        {
            _heightmapOriginal.Smooth();
            UpdateSprite();
        }

        public void Button_Remove()
        {
            // TODO: Remove from list
            Destroy(gameObject);
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