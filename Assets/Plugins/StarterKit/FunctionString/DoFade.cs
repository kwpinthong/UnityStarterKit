using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace StarterKit.FunctionStringLib
{
    [System.Serializable]
    public class DoFade
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.ShowInInspector]
#endif
        public bool IsRun { get; private set; }
        
        public CanvasGroup CanvasGroup; 
        public float Alpha; 
        public float Duration; 
        public Ease Ease;
        
        public void Run(Action onComplete)
        {
            IsRun = true;
            CanvasGroup.DOFade(Alpha, Duration).SetEase(Ease).OnComplete(() =>
            {
                onComplete?.Invoke();
                IsRun = false;
            });
        }
    }
}


