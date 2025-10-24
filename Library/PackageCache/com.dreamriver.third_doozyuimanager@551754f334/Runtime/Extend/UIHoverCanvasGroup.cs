using DG.Tweening;
using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace SKODE
{
    public class UIHoverCanvasGroup : MonoBehaviour
    {
        [SerializeField] private float normalAlpha = 0.8f;
        [SerializeField] private float hoverAlpha = 1;
        [SerializeField] private float animDuration = 0.2f;

        private CanvasGroup _canvasGroup;
        private UISelectable _uiSelectable;

        private void Awake()
        {
            _canvasGroup = transform.CanvasGroup();

            if (!gameObject.GetComponent<UISelectable>())
            {
                _uiSelectable = gameObject.AddComponent<UISelectable>();
            }
            else
            {
                _uiSelectable = gameObject.GetComponent<UISelectable>();
            }
        }

        private void Start()
        {
            _uiSelectable.normalState.stateEvent.Event.AddListener(() => { ToAlpha(normalAlpha); });
            _uiSelectable.highlightedState.stateEvent.Event.AddListener(() => { ToAlpha(hoverAlpha); });
        }

        private void ToAlpha(float alpha)
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(alpha, animDuration);
        }
    }
}