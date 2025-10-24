using System.Collections.Generic;
using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager;
using Doozy.Runtime.UIManager.Animators;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace SKODE
{
    public class BaseDoozyUIManager : BaseDreamRiver
    {
        #region Fields

        public bool isVisible
        {
            get
            {
                bool value = false;
                if (uiView != null)
                {
                    value = uiView.isVisible;
                }

                if (uiContainer != null)
                {
                    value = uiContainer.isVisible;
                }

                if (uiPopup != null)
                {
                    value = uiPopup.isVisible;
                }

                return value;
            }
        }

        protected CanvasGroup canvasGroup;
        protected RectTransform rectTransform;
        protected LayoutElement layoutElement;
        protected UIView uiView;

        /// <summary>
        /// uiContainer即可能是UIContainer,也可能是UIView.
        /// UIView继承自UIContainer.
        /// </summary>
        protected UIContainer uiContainer;

        protected UIPopup uiPopup;

        protected UIContainerUIAnimator uiContainerUIAnimator;
        protected AnimatorModule animatorModule;
        protected ReactorController reactorController;

        protected UIButton uiButton;
        protected UIToggle uiToggle;
        protected UIToggleGroup uiToggleGroup;
        protected UITab uiTab;
        protected UIScrollbar uiScrollbar;
        protected UISlider uiSlider;
        protected UIStepper uiStepper;

        /// <summary>
        /// 本UIForm的根节点是否是UIPopup
        /// </summary>
        protected bool rootIsUIPopup;

        #endregion

        protected virtual void Awake()
        {
            rectTransform = transform.RectTransform();
            canvasGroup = transform.CanvasGroup();
            layoutElement = GetComponent<LayoutElement>();
            uiView = GetComponent<UIView>();
            uiContainer = GetComponent<UIContainer>();
            uiPopup = GetComponent<UIPopup>();
            uiContainerUIAnimator = GetComponentInChildren<UIContainerUIAnimator>();
            animatorModule = GetComponent<AnimatorModule>();
            reactorController = GetComponent<ReactorController>();

            uiButton = GetComponent<UIButton>();
            uiToggle = GetComponent<UIToggle>();
            uiToggleGroup = GetComponent<UIToggleGroup>();
            uiTab = GetComponent<UITab>();
            uiScrollbar = GetComponent<UIScrollbar>();
            uiSlider = GetComponent<UISlider>();
            uiStepper = GetComponent<UIStepper>();

            if (uiView != null)
            {
                uiView.DisableCanvasWhenHidden = false;
            }

            //在uiContainer中
            if (uiContainer != null && uiPopup == null)
            {
                uiContainer.DisableCanvasWhenHidden = false;

                //解决UIContainer初次实例化本预制体,打开UI时闪烁的问题
                if (uiContainer.GetType().Name == nameof(UIContainer) && transform.Canvas().enabled)
                {
                    canvasGroup.alpha = 0;
                    uiContainer.OnStartBehaviour = ContainerBehaviour.Show;
                }
            }

            if (uiPopup != null)
            {
                if (uiContainerUIAnimator != null)
                {
                    //解决初次实例化本Popup,打开UI时闪烁的问题
                    uiContainerUIAnimator.canvasGroup.alpha = 0;
                }

                //将自身的Sort较上一个+1
                //解决打开多个popup显示问题
                uiPopup.OverrideSorting = false;
                uiPopup.OnShowCallback.Event.AddListener(delegate
                {
                    transform.Canvas().overrideSorting = true;
                    var childCount = transform.parent.childCount;
                    if (childCount == 1)
                    {
                        transform.Canvas().sortingOrder = transform.parent.Canvas().sortingOrder;
                    }
                    else
                    {
                        transform.Canvas().sortingOrder =
                            transform.parent.GetChild(childCount - 2).Canvas().sortingOrder + 1;
                    }
                });
            }

            //判断本UIForm的根节点是否是UIPopup
            var parCanvas = rectTransform.GetRootCanvasByDreamRiver();
            if (parCanvas.name.Contains("UIPopup") || parCanvas.GetComponent<UITag>() &&
                parCanvas.GetComponent<UITag>().Id.Category == "UIPopup")
            {
                rootIsUIPopup = true;
            }
        }

        /// <summary>
        /// 打开指定本 Container 下的指定 View,并关闭其他所有 View
        /// </summary>
        /// <typeparam name="T">打开后要显示的界面</typeparam>
        protected void Show<T>() where T : BaseDoozyUIManager
        {
            List<BaseDoozyUIManager> childPages = new();
            var container = transform.parent;
            for (int i = 0; i < container.childCount; i++)
            {
                var child = container.GetChild(i);
                if (child.GetComponent<BaseDoozyUIManager>() != null)
                {
                    childPages.Add(child.GetComponent<BaseDoozyUIManager>());
                }
            }

            //获得正在打开的View界面
            var openView = childPages.FindAll(
                x => x.isVisible);

            //获得T的实例
            BaseDoozyUIManager tInstance = null;
            foreach (var variable in childPages)
            {
                if (variable.GetType().FullName == typeof(T).FullName)
                {
                    tInstance = variable;
                    break;
                }
            }

            if (openView.Count > 0)
            {
                openView[0].Hide().OnComplete(tInstance.Show);
                openView.Remove(openView[0]);
                
                foreach (var variable in openView)
                {
                    variable.InstantHide();
                }
            }
            else
            {
                tInstance.Show();
            }
        }

        /// <summary>
        /// 显示
        /// </summary>
        public virtual void Show()
        {
            if (uiView != null)
            {
                uiView.Show(true);

                return;
            }

            if (uiContainer != null)
            {
                uiContainer.Show(true);
                return;
            }

            if (layoutElement != null)
                layoutElement.ignoreLayout = false;

            transform.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public virtual DoozyUIHideProcess Hide()
        {
            if (uiView != null)
            {
                return new DoozyUIHideProcess(
                    uiContainerUIAnimator.hideAnimation.GetStartDelay() +
                    uiContainerUIAnimator.hideAnimation.GetDuration(),
                    () => { uiView.Hide(true); });
            }

            if (uiContainer != null)
            {
                return new DoozyUIHideProcess(
                    uiContainerUIAnimator.hideAnimation.GetStartDelay() +
                    uiContainerUIAnimator.hideAnimation.GetDuration(),
                    () => { uiContainer.Hide(true); });
            }

            if (uiPopup != null)
            {
                return new DoozyUIHideProcess(
                    uiContainerUIAnimator.hideAnimation.GetStartDelay() +
                    uiContainerUIAnimator.hideAnimation.GetDuration(),
                    () => { uiPopup.Hide(true); });
            }

            if (layoutElement != null)
                layoutElement.ignoreLayout = true;

            return new DoozyUIHideProcess(
                0, () => { transform.Hide(); });
        }

        /// <summary>
        /// 立刻隐藏
        /// </summary>
        public virtual void InstantHide()
        {
            if (uiView != null)
            {
                uiView.InstantHide(true);
                return;
            }

            if (uiContainer != null)
            {
                uiContainer.InstantHide(true);
                return;
            }

            if (uiPopup != null)
            {
                uiPopup.InstantHide(true);
                return;
            }

            if (layoutElement != null)
                layoutElement.ignoreLayout = true;

            transform.Hide();
        }
    }
}