using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using StarterKit.AudioManagerLib;
using StarterKit.Common;
using StarterKit.FunctionStringLib;
using StarterKit.FunctionStringLib.Function;
using UnityEngine;

namespace StarterKit.FunctionStringLib
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
        
        [SerializeField]
        private bool dontDestroyOnLoad = true;
        
        [Header("Do Scale")] 
#if ODIN_INSPECTOR
        [LabelText("Scale")]
#endif
        private float doScaleSize = 1.1f;
#if ODIN_INSPECTOR
        [LabelText("Duration")]
#endif
        [SerializeField]
        private float doScaleDuration = 0.2f;
#if ODIN_INSPECTOR
        [LabelText("Ease")]
#endif
        [SerializeField]
        private Ease doScaleEase = Ease.Linear;
#if ODIN_INSPECTOR
        [LabelText("List")]
#endif
        [SerializeField] 
        private List<DoScale> doScaleList = new();

        [Header("Do Fade")]
#if ODIN_INSPECTOR
        [LabelText("Duration")]
#endif
        [SerializeField]
        private float doFadeDuration = 0.2f;
#if ODIN_INSPECTOR
        [LabelText("Ease")]
#endif
        [SerializeField]
        private Ease doFadeEase = Ease.Linear;
#if ODIN_INSPECTOR
        [LabelText("List")]
#endif
        [SerializeField] 
        private List<DoFade> doFadeList = new();
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        
        private void OnDestroy()
        { 
            instance = null;
        }
        
        private void ThisRun(string functionString, List<FunctionInspectorValue> inspectorValues, Action onComplete)
        {
            var functions = functionString.Split(";").ToList();
            functions.RemoveAll(string.IsNullOrEmpty);
            var key = functions[0];
            
            switch (key)
            {
                case Key.DoFade:
                {
                    RunDoFade(functions, inspectorValues, onComplete);
                    break;
                }
                case Key.DoScale:
                {
                    RunDoScale(functions, inspectorValues, onComplete);
                    break;
                }
                case Key.PlayBGM:
                {
                    var bgmId = functions[1];
                    AudioManager.PlayBGM(bgmId);
                    onComplete?.Invoke();
                    break;
                }
                case Key.PlaySound:
                {
                    var soundId = functions[1];
                    AudioManager.PlaySound(soundId);
                    onComplete?.Invoke();
                    break;
                }
            }
        }
        
        private void RunDoFade(List<string> functions, List<FunctionInspectorValue> inspectorValues, Action onComplete)
        {
            var key = functions[1];
            var targetKey = inspectorValues.Find(x => x.Key == key).Value;
            if (targetKey == null)
            {
                SendWarning(Key.DoFade, $"Target transform is null. Key: {key}");
                return;
            }
            var doFade = doFadeList.Find(x => !x.IsRun);
            if (doFade == null)
            {
                doFade = new DoFade();
                doFadeList.Add(doFade);
            }
            doFade.CanvasGroup = targetKey.GetComponent<CanvasGroup>();
            doFade.Alpha = functions.Count >= 3 ? float.Parse(functions[2]) : 1;
            doFade.Duration = functions.Count >= 4 ? float.Parse(functions[3]) : doFadeDuration;
            doFade.Ease = functions.Count >= 5 ? (Ease)Enum.Parse(typeof(Ease), functions[4]) : doFadeEase;
            doFade.Run(onComplete);
        }
        
        private void RunDoScale(List<string> functions, List<FunctionInspectorValue> inspectorValues, Action onComplete)
        {
            var key = functions[1];
            var targetKey = inspectorValues.Find(x => x.Key == key).Value;
            if (targetKey == null)
            {
                SendWarning(Key.DoScale, $"Target transform is null. Key: {key}");
                return;
            }
            var doScale = doScaleList.Find(x => !x.IsRun);
            if (doScale == null)
            {
                doScale = new DoScale();
                doScaleList.Add(doScale);
            }
            doScale.Transform = targetKey;
            doScale.Scale = functions.Count >= 3 ? float.Parse(functions[2]) : doScaleSize;
            doScale.Duration = functions.Count >= 4 ? float.Parse(functions[3]) : doScaleDuration;
            doScale.Ease = functions.Count >= 5 ? (Ease)Enum.Parse(typeof(Ease), functions[4]) : doScaleEase;
            doScale.Run(onComplete);
        }
        
        private void SendWarning(string key, string message) => CommonDebug.LogWarning($"Function: {key}, {message}");
    }
}
