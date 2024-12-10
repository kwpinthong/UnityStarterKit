using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StarterKit.UIFarmworkLib
{
    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Slider slider;
        
        public void OnSliderChanged(float value)
        {
            if (valueText != null)
                valueText.text = $"{value}";
        }
        
    }
}
