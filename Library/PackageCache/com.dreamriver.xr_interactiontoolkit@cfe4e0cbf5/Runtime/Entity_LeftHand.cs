using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using CommonUsages = UnityEngine.XR.CommonUsages;
using InputDevice = UnityEngine.XR.InputDevice;

namespace SKODE
{
    public class Entity_LeftHand : SingleMonoBehaviour<Entity_LeftHand>
    {
        /// <summary>
        /// 控制器是否连接
        /// 仅在真实设备上可信（Pico Live Preview时总显示True）
        /// </summary>
        public bool IsConnected { private set; get; }

        // 以下是各个按键的回调
        public Action OnTriggerEnter;
        public Action OnTriggerDown;
        public Action OnTriggerUp;

        public Action OnGripEnter;
        public Action OnGripDown;
        public Action OnGripUp;

        public Action OnAppEnter;
        public Action OnAppDown;
        public Action OnAppUp;

        public Action OnJoyStickEnter;
        public Action OnJoyStickDown;
        public Action OnJoyStickUp;

        public Action OnXEnter;
        public Action OnXDown;
        public Action OnXUp;

        public Action OnYEnter;
        public Action OnYDown;
        public Action OnYButonUp;

        private InputDevice _inputDevice;
        private ActionBasedController _actionBasedController;
        private ActionBasedControllerManager _actionBasedControllerManager;
        private XRPokeInteractor _xrPokeInteractor;
        private XRRayInteractor _xrRayInteractor;

        private RockerInfo _rockerInfo = new();

        // 以下通过反射得到
        private InputActionReference m_TeleportModeActivate;
        private InputActionReference m_TeleportModeCancel;
        private InputActionReference m_Turn;
        private InputActionReference m_SnapTurn;
        private InputActionReference m_Move;

        private MethodInfo _enableActionMethodInfo;
        private MethodInfo _disableActionMethodInfo;

        private void Awake()
        {
            _actionBasedController = GetComponent<ActionBasedController>();
            _actionBasedControllerManager = GetComponent<ActionBasedControllerManager>();
            _xrPokeInteractor = GetComponentInChildren<XRPokeInteractor>();
            _xrRayInteractor = GetComponentInChildren<XRRayInteractor>();

            InitPrivateField();
        }

        protected override void Start()
        {
            base.Start();

            _inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        }

        private void Update()
        {
            CheckControllerConnection();

            if (_inputDevice.isValid)
            {
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.triggerButton,
                    OnTriggerEnter, OnTriggerDown, OnTriggerUp);
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.gripButton, OnGripEnter,
                    OnGripDown, OnGripUp);
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.primaryButton, OnXEnter,
                    OnXDown, OnXUp);
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.secondaryButton,
                    OnYEnter, OnYDown, OnYButonUp);
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.primary2DAxisClick,
                    OnJoyStickEnter, OnJoyStickDown, OnJoyStickUp);
                DreaRiverXR_Predefine.ButtonDispatchModel(_inputDevice, CommonUsages.menuButton, OnAppEnter,
                    OnAppDown, OnAppUp);
            }
        }

        /// <summary>
        /// 获得当前射线下击中的UI
        /// </summary>
        [CanBeNull]
        public GameObject GetRayUI()
        {
            return DreaRiverXR_CommonMethod.IsRayInteractorOverUI(_xrRayInteractor);
        }

        /// <summary>
        /// 获得当前射线下击中的物体
        /// </summary>
        [CanBeNull]
        public GameObject GetRay3DCollider()
        {
            return DreaRiverXR_CommonMethod.IsRayInteractorOver3DCollider(_xrRayInteractor);
        }

        /// <summary>
        /// 开启某个功能
        /// </summary>
        public void EnableAction(XRAction value)
        {
            _enableActionMethodInfo ??= _actionBasedControllerManager.GetType().GetMethod("EnableAction",
                BindingFlags.Static | BindingFlags.NonPublic);

            InputActionReference actionReference = null;

            switch (value)
            {
                case XRAction.Teleport:
                    actionReference = m_TeleportModeActivate;
                    break;
                case XRAction.Turn:
                    actionReference = m_Turn;
                    break;
                case XRAction.SnapTurn:
                    actionReference = m_SnapTurn;
                    break;
                case XRAction.Move:
                    actionReference = m_Move;
                    break;
            }

            if (_enableActionMethodInfo != null)
            {
                _enableActionMethodInfo.Invoke(null, new object[] { actionReference });

                if (value == XRAction.Teleport)
                    _enableActionMethodInfo.Invoke(null, new object[] { m_TeleportModeCancel });
            }
        }

        /// <summary>
        /// 关闭某个功能
        /// </summary>
        public void DisableAction(XRAction value)
        {
            _disableActionMethodInfo ??= _actionBasedControllerManager.GetType().GetMethod("DisableAction",
                BindingFlags.Static | BindingFlags.NonPublic);

            InputActionReference actionReference = null;

            switch (value)
            {
                case XRAction.Teleport:
                    actionReference = m_TeleportModeActivate;
                    break;
                case XRAction.Turn:
                    actionReference = m_Turn;
                    break;
                case XRAction.SnapTurn:
                    actionReference = m_SnapTurn;
                    break;
                case XRAction.Move:
                    actionReference = m_Move;
                    break;
            }

            if (_disableActionMethodInfo != null)
            {
                _disableActionMethodInfo.Invoke(null, new object[] { actionReference });

                if (value == XRAction.Teleport)
                    _disableActionMethodInfo.Invoke(null, new object[] { m_TeleportModeCancel });
            }
        }

        /// <summary>
        /// 是否按下了输入的键
        /// 如果一直按住，将持续返回true
        /// 例如: Button.Trigger
        /// 注: 经测试：Button.Trigger、Button.TriggerButton、Button.TriggerPressed 3种效果等同
        /// </summary>
        /// <returns></returns>
        public bool IsPressed(InputHelpers.Button button)
        {
            bool isPressed = false;
            if (_inputDevice.isValid)
            {
                _inputDevice.IsPressed(button, out isPressed);
            }

            return isPressed;
        }

        /// <summary>
        /// 获得摇杆值
        /// </summary>
        public RockerInfo GetRockerValue()
        {
            if (_inputDevice.isValid)
            {
                float up;
                _inputDevice.TryReadSingleValue(InputHelpers.Button.PrimaryAxis2DUp, out up);
                _rockerInfo.UpDownVaule = up;

                float left;
                _inputDevice.TryReadSingleValue(InputHelpers.Button.PrimaryAxis2DLeft, out left);
                _rockerInfo.LeftRightValue = left;
            }

            return _rockerInfo;
        }

        /// <summary>
        /// 获得Trigger按下的值
        /// [0-1]
        /// </summary>
        public float GetTriggerValue()
        {
            float value = 0;
            if (_inputDevice.isValid)
            {
                _inputDevice.TryReadSingleValue(InputHelpers.Button.Trigger, out value);
            }

            return value;
        }

        /// <summary>
        /// 获得Grip按下的值
        /// [0-1]
        /// </summary>
        /// <returns></returns>
        public float GetGripValue()
        {
            float value = 0;
            if (_inputDevice.isValid)
            {
                _inputDevice.TryReadSingleValue(InputHelpers.Button.Grip, out value);
            }

            return value;
        }

        /// <summary>
        /// 获取Trigger状态
        /// WasPressedThisFrame(),在触发按下动作帧中返回true
        /// WasPerformedThisFrame(),在保持按下状态帧中返回true
        /// WasReleasedThisFrame(),在触发释放动作帧中返回true
        /// IsPressed(),只要超过按钮按下阈值就返回true
        /// </summary>
        public InputAction GetTriggerStateThisFrame()
        {
            return _actionBasedController.activateAction.action;
        }

        /// <summary>
        /// 获取Grip状态
        /// triggered,只会在按下那一刻返回true
        /// IsPressed(),如果Grip处于按下状态，会一直返回true
        /// </summary>
        public InputAction GetGrabStateThisFrame()
        {
            return _actionBasedController.selectAction.action;
        }

        /// <summary>
        /// 反射得到私有字段
        /// </summary>
        private void InitPrivateField()
        {
            m_TeleportModeActivate = GetReference("m_TeleportModeActivate");
            m_TeleportModeActivate = GetReference("m_TeleportModeActivate");
            m_Turn = GetReference("m_Turn");
            m_SnapTurn = GetReference("m_SnapTurn");
            m_Move = GetReference("m_Move");
        }

        private InputActionReference GetReference(string nameValue)
        {
            FieldInfo fieldInfo = _actionBasedControllerManager.GetType().GetField(nameValue,
                BindingFlags.NonPublic | BindingFlags.Instance);
            return (InputActionReference)fieldInfo.GetValue(_actionBasedControllerManager);
        }

        /// <summary>
        /// 检查是否连接了控制器，并自动开关显示手柄、 Poke Interactor 的 Poke Point 物体
        /// 仅在真实设备上可信（ico Live Preview时总显示True）
        /// </summary>
        private void CheckControllerConnection()
        {
            List<InputDevice> devices = new List<InputDevice>();
            InputDevices.GetDevices(devices);

            IsConnected = false;
            foreach (var device in devices)
            {
                if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left) &&
                    device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
                {
                    IsConnected = true;
                }
            }

            //根据是否连接，自动开关手柄及其下所有内容
            _actionBasedController.hideControllerModel = !IsConnected;
            _xrPokeInteractor.gameObject.SetActive(IsConnected);
            _xrRayInteractor.gameObject.SetActive(IsConnected);
        }
    }
}