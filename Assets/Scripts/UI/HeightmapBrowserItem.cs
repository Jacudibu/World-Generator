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

        public bool IsVisible { get; private set; }
        
        [SerializeField] private Image _image;
        [SerializeField] private InputField _name;
        [SerializeField] private Slider _strengthSlider;
        [SerializeField] private InputField _strengthSliderInput;
        [SerializeField] private Text _toggleVisibilityButtonText;

        private HeightmapBrowser _associatedBrowser;

        public enum Mode
        {
            Add = 0,
            Substract = 1,
        }
        public Mode CurrentMode { get; private set; }

        public void Init(Heightmap map, String name, HeightmapBrowser associatedBrowser)
        {
            _associatedBrowser = associatedBrowser;
            _heightmapOriginal = map;

            IsVisible = true;
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
        
        public void Button_Remove()
        {
            _associatedBrowser.RemoveItem(this);
            Destroy(gameObject);
        }

        public void Button_RotateClockwise()
        {
            _heightmapOriginal.RotateClockwise();
            UpdateHeightmap();
        }

        public void Button_RotateCounterClockwise()
        {
            _heightmapOriginal.RotateCounterClockwise();
            UpdateHeightmap();
        }
        
        public void Button_Smooth()
        {
            _heightmapOriginal.Smooth();
            UpdateHeightmap();
        }
       
        public void Button_ToggleVisibility()
        {
            IsVisible = !IsVisible;
            _associatedBrowser.UpdateVisualization();

            _toggleVisibilityButtonText.text = IsVisible ? "<o>" : "<ø>";
        }

        public void Dropdown_ToggleMode(int value)
        {
            CurrentMode = (Mode) value;
            _associatedBrowser.UpdateVisualization();
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
            System.Diagnostics.Debug.Assert(HeightmapModified != null, "HeightmapModified == null in UpdateClone");

            HeightmapModified.AddToAllValues(_strengthSlider.value);
        }
    }
}