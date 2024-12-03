using System;
using UnityEngine;

namespace StarterKit.Common.ValueField
{
    [CreateAssetMenu(fileName = "LargeNumberField", menuName = "StarterKit/ValueField/LargeNumberField")]
    public class LargeNumberField : ScriptableObject
    {
        private static readonly string[] customPrefixes = { "K", "M", "B", "T" };
        private static double divider = 1000;
        
        [SerializeField] private double value;
#if UNITY_EDITOR
        [Sirenix.OdinInspector.ReadOnly]
#endif
        [SerializeField] private int exponent;

        public double Value => ToDouble();
        
        public void Set(double setValue)
        {
            this.value = setValue;
            Normalize();
        }
        
        public void Add(double addValue)
        {
            var nextValue = ToDouble() + addValue;
            Set(nextValue);
        }
        
        private void OnDisable()
        {
            Clear();
        }
        
        private void Normalize()
        {
            if (value == 0)
            {
                exponent = 0;
                return;
            }

            exponent = (int)Math.Floor(Math.Log10(Math.Abs(value)) / Math.Log10(divider));
            value /= Math.Pow(divider, exponent);
        }
        
        private double ToDouble()
        {
            return value * Math.Pow(divider, exponent);
        }
        
        public override string ToString()
        {
            if (exponent < 1)
            {
                return $"{value:F2}";
            }
    
            if (exponent < customPrefixes.Length)
            {
                return $"{value:F2}{customPrefixes[exponent - 1]}";
            }
    
            int alphaExponent = exponent - customPrefixes.Length;
            string result = "";
            while (alphaExponent >= 0)
            {
                result = (char)('A' + (alphaExponent % 26)) + result;
                alphaExponent = alphaExponent / 26 - 1;
            }

            return $"{value:F2}{result}";
        }

        private void Reset()
        {
            Clear();
        }
        
        private void Clear()
        {
            value = 0;
            exponent = 0;
        }
    }
}
