using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVP
{
    public abstract class BaseView : MonoBehaviour, IView
    {
        [SerializeField] private bool _hideInAwake;
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnAwake() { }
        protected virtual void OnStart() { }
        protected virtual void OnDestroyInner() { }

        private void Awake()
        {
            if (_hideInAwake)
                Hide();
            OnAwake();
        }       

        void Start()
        {
            OnStart();
        }

        private void OnDestroy()
        {
            OnDestroyInner();
        }
    }
}
