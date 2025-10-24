using System;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace SKODE
{
    public class EntityTip : BaseDreamRiver
    {
        /// <summary>
        /// 显示回调
        /// </summary>
        public Action OnShowCallback;

        /// <summary>
        /// 隐藏回调
        /// </summary>
        public Action OnHideCallback;

        /// <summary>
        /// 跟随的目标
        /// </summary>
        public Transform followTrans;

        /// <summary>
        /// 实例化出来的UIPopup
        /// </summary>
        private UIPopup _uiPopup;

        /// <summary>
        /// 传入的信息
        /// </summary>
        public EntityTipManager.TipInfo TipInfo;

        public void Init(Transform followTransInput, EntityTipManager.TipInfo info)
        {
            TipInfo = info;
            followTrans = followTransInput;

            CheckPermission();
        }

        private void OnMouseDown()
        {
            if (transform.GetOverUI() != null)
                return;

            if (TipInfo.TriggerType == TriggerType.Click)
            {
                Show();
            }
        }

        private void OnMouseEnter()
        {
            if (transform.GetOverUI() != null)
                return;

            if (TipInfo.TriggerType == TriggerType.Pointer)
            {
                Show();
            }
        }

        private void OnMouseExit()
        {
            if (TipInfo.TriggerType == TriggerType.Pointer)
            {
                Hide();
            }
        }

        public void Hide()
        {
            if (_uiPopup != null)
            {
                _uiPopup.Hide();
                OnHideCallback?.Invoke();

                _uiPopup = null;
            }
        }

        public void Show()
        {
            // 能否触发Show的开关
            if (TipInfo.ShowSwitch == false)
                return;

            // 若已经显示,则不再执行Show
            if (_uiPopup != null)
                return;

            _uiPopup = UIPopup.Get(TipInfo.UITipName);

            if (TipInfo.Content.IsNullOrEmpty() == false)
                _uiPopup.SetTexts(TipInfo.Content);

            _uiPopup.OnShowCallback.Event.AddListener(delegate
            {
                _uiPopup.GetComponent<UITipData>().followTarget = followTrans;
                _uiPopup.GetComponent<UITipData>().offsetTarget = TipInfo.OffsetTrans;
            });

            _uiPopup.Show();
            OnShowCallback?.Invoke();
        }

        /// <summary>
        /// 本功能需模型的读写权限,检查读写权限
        /// </summary>
        private void CheckPermission()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                Mesh mesh = meshFilter.sharedMesh;
                // 检查网格是否可读
                if (mesh.isReadable == false)
                {
                    Debug.LogError("请打开本模型FBX的读写属性", gameObject);
                }
            }
        }

        /// <summary>
        /// 交互触发类型
        /// </summary>
        public enum TriggerType
        {
            /// <summary>
            /// 进入触发,移出隐藏
            /// </summary>
            Pointer,

            /// <summary>
            /// 点击触发,需自行隐藏
            /// </summary>
            Click,

            /// <summary>
            /// 自行触发,自行隐藏
            /// 触发使用Show方法
            /// 隐藏使用Hide方法
            /// </summary>
            ManualTrigger
        }
    }
}