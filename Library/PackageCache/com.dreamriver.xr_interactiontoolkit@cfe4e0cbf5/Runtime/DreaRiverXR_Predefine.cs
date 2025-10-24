using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace SKODE
{
    public class DreaRiverXR_Predefine
    {
        //提供状态字典独立记录各个feature的状态
        private static Dictionary<string, bool> _stateDic = new();

        /// <summary>
        /// 按钮事件源触发
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="usage">功能特征</param>
        /// <param name="btnEnter">开始按下按钮事件</param>
        /// <param name="btnDown">按下按钮事件</param>
        /// <param name="btnUp">抬起按钮事件</param>
        public static void ButtonDispatchModel(InputDevice device, InputFeatureUsage<bool> usage, Action btnEnter,
            Action btnDown,
            Action btnUp)
        {
            //Debug.Log("usage:" + usage.name);
            //为首次执行的feature添加bool状态 -- 用以判断Enter和Up状态
            string featureKey = device.characteristics + usage.name;
            if (!_stateDic.ContainsKey(featureKey))
            {
                _stateDic.Add(featureKey, false);
            }

            bool isDown;
            if (device.TryGetFeatureValue(usage, out isDown) && isDown)
            {
                //Debug.Log("device:" + device.characteristics + "usage:" + usage.name);
                if (!_stateDic[featureKey])
                {
                    _stateDic[featureKey] = true;
                    btnEnter?.Invoke();
                }

                btnDown?.Invoke();
            }
            else
            {
                if (_stateDic[featureKey])
                {
                    btnUp?.Invoke();
                    _stateDic[featureKey] = false;
                }
            }
        }
    }

    public enum XRAction
    {
        /// <summary>
        /// 传送
        /// 表现为:摇杆向前推动
        /// </summary>
        Teleport,

        /// <summary>
        /// 缓慢转向
        /// </summary>
        Turn,

        /// <summary>
        /// 迅速转向
        /// </summary>
        SnapTurn,

        /// <summary>
        /// 移动
        /// </summary>
        Move,
    }

    public class RockerInfo
    {
        /// <summary>
        /// 向前向后的值
        /// 取值范围 [1,-1]
        /// 摇杆推到最上面时返回1，推到最下面时返回-1
        /// 不动中间位置时是0
        /// </summary>
        public float UpDownVaule;

        /// <summary>
        /// 向前向后的值
        /// 取值范围 [1,-1]
        /// 摇杆推到最左面时返回1，推到右下面时返回-1
        /// 不动中间位置时是0
        /// </summary>
        public float LeftRightValue;
    }
}