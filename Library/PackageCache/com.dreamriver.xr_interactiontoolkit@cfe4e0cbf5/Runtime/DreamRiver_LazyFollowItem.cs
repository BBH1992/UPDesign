using DG.Tweening;
using UnityEngine;

namespace SKODE
{
    /// <summary>
    /// 受DreamRiver_LazyFollow驱动，在跟随时会自动朝向目标
    /// </summary>
    public class DreamRiver_LazyFollowItem : MonoBehaviour
    {
        public void DoLookAt(Vector3 targetPos, float duration)
        {
            // 计算从目标位置到UI元素的方向向量
            Vector3 directionToUIElement = transform.position - targetPos;

            // 计算反向的目标位置
            Vector3 targetPositionForNegativeZ = transform.position + directionToUIElement;

            // 使用计算出的反向目标位置调用 DOLookAt
            transform.DOLookAt(targetPositionForNegativeZ, duration);
        }
    }
}