using UnityEngine;

namespace StarterKit.Common
{
    public class SpriteAutoSortOnY : MonoBehaviour
    {
        [SerializeField] private Transform root;
        [SerializeField] private int offsetSortingOrder;
        private SpriteRenderer spriteRenderer;
        private float lastY;

        private void Awake()
        {
            if (!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            lastY = GetY();
        }

        private void Update()
        {
            if (spriteRenderer && lastY != GetY())
            {
                lastY = GetY();
                UpdateSortingOrder();
            }
        }

        private void UpdateSortingOrder()
        {
            spriteRenderer.sortingOrder = Mathf.CeilToInt(lastY);
            spriteRenderer.sortingOrder += offsetSortingOrder;
        }
        private float GetY()
        {
            return root ? root.position.y : transform.position.y;
        }
    }
}
