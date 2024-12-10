using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StarterKit.UIFarmworkLib
{
    public class SettingOption : MonoBehaviour
    {
        public enum OptionType
        {
            Slider,
            Dropdown,
        }
        
        public struct SettingData
        {
            public float sliderValue;
            public int dropdownValue;
        }

        [SerializeField] private OptionType optionType = OptionType.Slider;

        [Header("Option Type GameObject")]
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Dropdown dropdown;
        
        public Action<SettingData> OnOptionChanged;

        private void Start()
        {
            switch (optionType)
            {
                case OptionType.Slider:
                    slider.onValueChanged.AddListener(OnSliderChanged);
                    break;
                case OptionType.Dropdown:
                    dropdown.onValueChanged.AddListener(OnDropdownChanged);
                    break;
            }
        }

        public void SetValue(float value)
        {
            if (optionType == OptionType.Slider)
            {
                slider.SetValueWithoutNotify(value * 100f);
                slider.GetComponent<SliderHandler>().OnSliderChanged(value * 100f);
            }
        }
        
        private void OnSliderChanged(float value)
        {
            OnOptionChanged?.Invoke(new SettingData
            {
                sliderValue = value / 100f,
            });
        }
        
        private void OnDropdownChanged(int value)
        {
            OnOptionChanged?.Invoke(new SettingData
            {
                dropdownValue = value,
            });
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            slider?.gameObject.SetActive(optionType == OptionType.Slider);
            dropdown?.gameObject.SetActive(optionType == OptionType.Dropdown);
        }
#endif
    }
}
