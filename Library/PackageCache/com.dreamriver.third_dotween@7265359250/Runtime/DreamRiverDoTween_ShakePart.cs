using DG.Tweening;
using UnityEngine;

namespace SKODE
{
    public partial class DreamRiverDoTween
    {
        /// <summary>
        /// 物体震动效果
        /// </summary>
        /// <param name="part">震动的物体</param>
        /// <param name="duration">震动持续时间</param>
        /// <param name="strength">震动强度</param>
        /// <param name="vibrato">震动次数</param>
        /// <param name="randomness">随机度</param>
        public static Tween ShakePart(Transform part, float duration = 0.5f, float strength = 0.3f, int vibrato = 10,
            float randomness = 0f)
        {
            // 记录零件原始位置
            Vector3 originalPosition = part.localPosition;
            // 执行震动动画
            return part.DOShakePosition(duration, strength, vibrato, randomness, false, false)
                .OnComplete(() => part.localPosition = originalPosition); // 动画完成后重置位置
        }
    }
}