using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StarterKit.Common
{
    public class PointerClickEvent : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent PointerClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick.Invoke();
        }
    }
}
