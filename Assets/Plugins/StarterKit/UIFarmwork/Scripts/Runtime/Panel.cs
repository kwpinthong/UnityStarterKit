using System.Collections;
using System.Collections.Generic;
using StarterKit.Common;
using StarterKit.FunctionStringLib;
using UnityEngine;

namespace StarterKit.UIFarmworkLib
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup container;
        [SerializeField] private FunctionKeeper functionKeeper;
        [SerializeField] private string fadeInFunctionKey = "fadeIn";
        [SerializeField] private string fadeOutFunctionKey = "fadeOut";
        
        public bool IsOpen => container.gameObject.activeSelf;

        protected virtual void PreOpen()
        {
            //CommonDebug.Log("PreOpen");
        }
        
        protected virtual void PostOpen()
        {
            //CommonDebug.Log("PostOpen");
        }
        
        protected virtual void PreClose()
        {
            //CommonDebug.Log("PerClose");
        }
        
        protected virtual void PostClose()
        {
            //CommonDebug.Log("PostClose");
        }
        
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#else
        [ContextMenu("Open")]
#endif
        public virtual void Open()
        {
            if (Application.isPlaying)
            {
                PreOpen();
                container.gameObject.SetActive(true);
                container.blocksRaycasts = true;
                container.interactable = true;
                functionKeeper.Run(fadeInFunctionKey, PostOpen);
            }
            else
            {
                container.gameObject.SetActive(true);
                container.blocksRaycasts = true;
                container.interactable = true;
                container.alpha = 1;
            }
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#else
        [ContextMenu("Close")]
#endif
        public virtual void Close()
        {
            if (Application.isPlaying)
            {
                PreClose();
                container.blocksRaycasts = false;
                container.interactable = false;
                functionKeeper.Run(fadeOutFunctionKey, () =>
                {
                    container.gameObject.SetActive(false);
                    PostClose();
                });
            }
            else
            {
                container.gameObject.SetActive(false);
                container.blocksRaycasts = false;
                container.interactable = false;
                container.alpha = 0f;
            }
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            functionKeeper = GetComponent<FunctionKeeper>();
            container = transform.Find("Container").GetComponent<CanvasGroup>();
        }
#endif
    }
}
