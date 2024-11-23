using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

namespace StarterKit.FunctionStringLib
{
    [System.Serializable]
    public class DoScale
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        public bool IsRun { get; private set; }
        
        public Transform Transform; 
        public float Scale; 
        public float Duration; 
        public Ease Ease;
        
        public void Run(Action onComplete)
        {
            IsRun = true;
            Transform.DOScale(Vector3.one * Scale, Duration).SetEase(Ease).OnComplete(() =>
            {
                onComplete?.Invoke();
                IsRun = false;
            });
        }
    }
}
