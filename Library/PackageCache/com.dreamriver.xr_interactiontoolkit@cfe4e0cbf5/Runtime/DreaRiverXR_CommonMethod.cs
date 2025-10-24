using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace SKODE
{
    public class DreaRiverXR_CommonMethod
    {
        /// <summary>
        /// 获得当前射线下击中的UI
        /// </summary>
        [CanBeNull]
        public static GameObject IsRayInteractorOverUI(XRRayInteractor interactor)
        {
            RaycastResult uiRaycastResult;
            if (interactor.TryGetCurrentUIRaycastResult(out uiRaycastResult))
            {
                // 检查是否有UI元素被击中
                if (uiRaycastResult.gameObject != null)
                {
                    // 获得被击中的UI元素
                    GameObject hitUIElement = uiRaycastResult.gameObject;
                    return hitUIElement;
                }
            }

            return null;
        }
        
        /// <summary>
        /// 获得当前射线下击中的物体
        /// </summary>
        [CanBeNull]
        public static GameObject IsRayInteractorOver3DCollider(XRRayInteractor interactor)
        {
            RaycastHit raycastHit;
            if (interactor.TryGetCurrent3DRaycastHit(out raycastHit))
            {
                // 检查是否有3D物体被击中
                if (raycastHit.collider != null)
                {
                    // 获得被击中的3D物体
                    GameObject hitObject = raycastHit.collider.gameObject;
                    return hitObject;
                }
            }

            return null;
        }
    }
}