using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldGeneration;

namespace UI
{
    public class HeightmapBrowserItem : MonoBehaviour
    {
        public Heightmap Heightmap { get; private set; }

        [SerializeField] private Image _image;
        [SerializeField] private InputField _name;
        [SerializeField] private Slider _strengthSlider;
        [SerializeField] private InputField _strengthSliderInput;

        public void Init(Heightmap map, String name)
        {
            // TODO: Add to List
            
            Heightmap = map;
            _name.text = name;
            UpdateSprite();
        }
        
        public void OnSliderValueChanged()
        {
            _strengthSliderInput.text = _strengthSlider.value.ToString("F");
        }

        public void OnInputValueChanged()
        {
            float value;
            bool parsed = float.TryParse(_strengthSliderInput.text, out value);

            if (parsed)
            {
                _strengthSlider.value = value;
            }
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
        }

        public void Button_Invert()
        {
            Heightmap.Invert();
            UpdateSprite();
        }

        public void Button_Smooth()
        {
            Heightmap.Smooth();
            UpdateSprite();
        }

        public void Button_Remove()
        {
            // TODO: Remove from list
            Destroy(gameObject);
        }

        private void UpdateSprite()
        {
            _image.sprite = HeightmapConverter.ToSprite(Heightmap);
        }
    }
}