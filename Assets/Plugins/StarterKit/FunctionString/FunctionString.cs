using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using StarterKit.AudioManagerLib;
using StarterKit.PoolManagerLib;
using UnityEngine;

namespace StarterKit.Common.FunctionStringLib
{
    public class FunctionString : MonoBehaviour
    {
        public static class Key
        {
            public const string DoScale = "doScale";
            public const string DoFade = "doFade";
            public const string PlayBGM = "playBGM";
            public const string PlaySound = "playSound";
        }
        
        private static FunctionString instance;

        public static void Run(string functionString, List<FunctionInspectorValue> inspectorValues, Action onComplete)
        {
            if (instance == null)
                CreateInstance.Create(nameof(FunctionString));
            instance.ThisRun(functionString, inspectorValues, onComplete);
        }

        [Header("Do Scale")] 
        private float doScaleSize = 1.1f;
        [SerializeField]
        private float doScaleDuration = 0.2f;
        [SerializeField]
        private Ease doScaleEase = Ease.Linear;
        [SerializeField]
        private float doFadeDuration = 0.2f;
        [SerializeField]
        private Ease doFadeEase = Ease.Linear;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void OnDestroy()
        { 
            instance = null;
        }
        
        private void ThisRun(string functionString, List<FunctionInspectorValue> inspectorValues, Action onComplete)
        {
            var function = functionString.Split(";").ToList();
            function.RemoveAll(string.IsNullOrEmpty);
            var key = function[0];
            
            switch (key)
            {
                case Key.DoFade:
                {
                    var targetKey = function[1];
                    var targetTransform = inspectorValues.Find(x => x.Key == targetKey).Value;
                    if (targetTransform == null)
                    {
                        SendWarning(Key.DoFade, $"Target transform is null. Key: {targetKey}");
                        return;
                    }
                    var canvasGroup = targetTransform.GetComponent<CanvasGroup>();
                    var alpha = function.Count >= 3 ? float.Parse(function[2]) : 0;
                    var duration = function.Count >= 4 ? float.Parse(function[3]) : doFadeDuration;
                    var ease = function.Count >= 5 ? (Ease)Enum.Parse(typeof(Ease), function[4]) : doFadeEase;
                    canvasGroup.DOFade(alpha, duration).SetEase(ease).OnComplete(() => onComplete?.Invoke());
                    break;
                }
                case Key.DoScale:
                {
                    var targetKey = function[1];
                    var targetTransform = inspectorValues.Find(x => x.Key == targetKey).Value;
                    if (targetTransform == null)
                    {
                        SendWarning(Key.DoScale, $"Target transform is null. Key: {targetKey}");
                        return;
                    }
                    var scale = function.Count >= 3 ? float.Parse(function[2]) : doScaleSize;
                    var duration = function.Count >= 4 ? float.Parse(function[3]) : doScaleDuration;
                    var ease = function.Count >= 5 ? (Ease)Enum.Parse(typeof(Ease), function[4]) : doScaleEase;
                    targetTransform.DOScale(Vector3.one * scale, duration).SetEase(ease).OnComplete(() => onComplete?.Invoke());
                    break;
                }
                case Key.PlayBGM:
                {
                    var bgmId = function[1];
                    AudioManager.PlayBGM(bgmId);
                    onComplete?.Invoke();
                    break;
                }
                case Key.PlaySound:
                {
                    var soundId = function[1];
                    AudioManager.PlaySound(soundId);
                    onComplete?.Invoke();
                    break;
                }
            }
        }
        
        private void SendWarning(string key, string message) => Debug.LogWarning($"Function: {key}, {message}");
    }
}
