using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace StarterKit.Common
{
    public class PointerClickEvent : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
    {
        public UnityEvent PointerClick;
        public UnityEvent PointerUp;
        public UnityEvent PointerDown;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown.Invoke();
        }
    }
}
