using UnityEngine;
using UnityEngine.Events;

namespace Bamboo.UI
{
    public class Menu : MonoBehaviour
    {
        public bool ignoreOpenOnlyOneCall = false;
        public bool ignoreCloseAllCall = false;
        public string MenuName { get; private set; }

        public UnityEvent OnMenuOpen = new UnityEvent();
        public UnityEvent OnMenuClose = new UnityEvent();
        [HideInInspector] public RectTransform rectTransform;

        public virtual void OnAwake()
        {
            MenuName = gameObject.name;
            rectTransform = GetComponent<RectTransform>();
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
            OnMenuOpen?.Invoke();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
            OnMenuClose?.Invoke();
        }

        public GameObject GetObject() => gameObject;
    }
}
