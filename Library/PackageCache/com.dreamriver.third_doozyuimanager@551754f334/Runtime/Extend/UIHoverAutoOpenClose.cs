using Doozy.Runtime.UIManager.Components;
using UnityEngine;

namespace SKODE
{
    public class UIHoverAutoOpenClose : MonoBehaviour
    {
        [SerializeField] private GameObject tar;

        [SerializeField, Header("是否使用功能：点击tar下的UIButton，立刻隐藏tar")]
        private bool useClickHide = true;

        private UISelectable _uiSelectable;

        private void Awake()
        {
            if (!gameObject.GetComponent<UISelectable>())
            {
                _uiSelectable = gameObject.AddComponent<UISelectable>();
            }
            else
            {
                _uiSelectable = gameObject.GetComponent<UISelectable>();
            }

            if (useClickHide)
            {
                //点击子按钮，自动关闭按钮组
                foreach (var button in tar.GetComponentsInChildren<UIButton>())
                {
                    button.onClickEvent.AddListener(() => { Toggle(false); });
                }
            }
        }

        private void Start()
        {
            _uiSelectable.normalState.stateEvent.Event.AddListener(() => { Toggle(false); });
            _uiSelectable.highlightedState.stateEvent.Event.AddListener(() => { Toggle(true); });
        }

        public void Toggle(bool value)
        {
            tar.SetActive(value);
        }
    }
}