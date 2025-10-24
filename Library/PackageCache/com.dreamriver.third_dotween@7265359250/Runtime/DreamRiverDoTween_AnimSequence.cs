using DG.Tweening;
using UnityEngine;

namespace SKODE
{
    public partial class DreamRiverDoTween
    {
        /// <summary>
        /// 播放动画序列的索引
        /// </summary>
        private static int _animSequenceIndex;

        /// <summary>
        /// 开始按顺序播放动画
        /// 例如:Tweener move1 = animations[0].DOMoveX(1, 1).Pause();
        /// 注意:
        /// 1.传入的Tweener,必须当时生成,不可在Awake()中生成缓存,否则会出现再次播放不能播放的情况.
        /// 2.若多次调用,需先重置 RewindSequence
        /// 3.原生不支持面板上配置的DOTweenAnimation,只支持代码写的Tweener.
        /// </summary>
        /// <param name="animations"></param>
        /// <param name="callback">播放结束的回调</param>
        /// <returns>返回该序列的ID</returns>
        public static string PlayAnimSequence(Tweener[] animations, TweenCallback callback = null)
        {
            _animSequenceIndex++;

            var id = nameof(_animSequenceIndex) + _animSequenceIndex;
            var sequence = DOTween.Sequence().SetId(id);

            foreach (var anim in animations)
            {
                sequence.Append(anim);
            }

            sequence.SetAutoKill(false).PlayForward();
            sequence.OnComplete(delegate { callback?.Invoke(); });

            return id;
        }

        /// <summary>
        /// 将ID中所有的动画回滚到起始状态,支持Sequence
        /// </summary>
        public static void RewindAnimSequence(string id)
        {
            DOTween.Rewind(id);
        }

        /// <summary>
        /// 重新播放为ID的动画
        /// </summary>
        /// <param name="id"></param>
        public static void RestartAnimSequence(string id)
        {
            DOTween.Restart(id);
        }
    }
}