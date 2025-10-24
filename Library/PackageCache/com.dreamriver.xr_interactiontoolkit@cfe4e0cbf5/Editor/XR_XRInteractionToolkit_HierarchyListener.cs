using TMPro;
using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace SKODE
{
    /// <summary>
    /// 创建物体监听器
    /// </summary>
    [InitializeOnLoad]
    public class XR_XRInteractionToolkit_HierarchyListener
    {
        private static readonly InteractionLayerMask EverythingLayer = -1;

        static XR_XRInteractionToolkit_HierarchyListener()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            AutoFixTeleportation();

            AutoAddComponent();
            
            AutoAddTrackedDeviceGraphicRaycaster();
        }

        /// <summary>
        /// 自动添加TrackedDeviceGraphicRaycaster
        /// </summary>
        private static void AutoAddTrackedDeviceGraphicRaycaster()
        {
            var follows = Object.FindObjectsOfType<GraphicRaycaster>();
            foreach (var item in follows)
            {
                if (item.GetComponent<TrackedDeviceGraphicRaycaster>() == null)
                {
                    item.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
                }
            }
        }

        private static void AutoAddComponent()
        {
            //增加 Entity_LeftHand 和 Entity_RightHand 组件
            var hands = Object.FindObjectsOfType<ActionBasedControllerManager>();
            foreach (var variable in hands)
            {
                if (variable.gameObject.name.Contains("Left"))
                {
                    if (variable.GetComponent<Entity_LeftHand>() == null)
                        variable.gameObject.AddComponent<Entity_LeftHand>();
                }
                else
                {
                    if (variable.GetComponent<Entity_RightHand>() == null)
                        variable.gameObject.AddComponent<Entity_RightHand>();
                }
            }

            var xrOrigin = Object.FindObjectOfType<XROrigin>();
            if (xrOrigin != null && xrOrigin.GetComponent<Entity_XROrigin>() == null)
                xrOrigin.gameObject.AddComponent<Entity_XROrigin>();

            //增加 XR_InputField 组件
            var unityEngineInputFields = Object.FindObjectsOfType<UnityEngine.UI.InputField>();
            foreach (var variable in unityEngineInputFields)
            {
                if (variable.GetComponent<XR_InputField>() == null)
                    variable.gameObject.AddComponent<XR_InputField>();
            }

            var tmpInputFields = Object.FindObjectsOfType<TMP_InputField>();
            foreach (var variable in tmpInputFields)
            {
                if (variable.GetComponent<XR_InputField>() == null)
                    variable.gameObject.AddComponent<XR_InputField>();
            }
        }

        private static void AutoFixTeleportation()
        {
            // 自动更正 TeleportationArea
            var teleportationAreas = Object.FindObjectsOfType<TeleportationArea>();
            foreach (var item in teleportationAreas)
            {
                if (item.interactionLayers.value == 1)
                {
                    item.interactionLayers = EverythingLayer;
                    item.matchDirectionalInput = true;
                }
            }

            // 自动更正 TeleportationAnchor
            var teleportationAnchor = Object.FindObjectsOfType<TeleportationAnchor>();
            foreach (var item in teleportationAnchor)
            {
                if (item.interactionLayers.value == 1)
                {
                    item.interactionLayers = EverythingLayer;
                    item.matchDirectionalInput = true;
                }
            }
        }
    }
}