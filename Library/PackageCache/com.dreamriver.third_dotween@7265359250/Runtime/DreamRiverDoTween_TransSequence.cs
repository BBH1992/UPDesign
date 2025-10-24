using DG.Tweening;
using UnityEngine;

namespace SKODE
{
    public partial class DreamRiverDoTween : MonoBehaviour
    {
        /// <summary>
        /// 播放移动到trans的动画索引
        /// </summary>
        private static int _transSequenceIndex;

        /// <summary>
        /// 将 moveTarget 按 movePos 指定的点依次经过移动,且看向前方点, 总的移动时间为time
        /// 它在移动时自动柔和转弯朝向下一个点,
        /// 但移动到最后一个点时,应跟最后一个点的朝向一致
        /// </summary>
        /// <param name="moveTarget">要移动的对象</param>
        /// <param name="time">移动完全程的时间</param>
        /// <param name="movePos">要依次移动到的地方</param>
        /// <param name="callback">播放结束的回调</param>
        /// <returns>返回该序列的ID</returns>
        public static string PlayTransSequence(Transform moveTarget, float time, Transform[] movePos,
            TweenCallback callback = null)
        {
            _transSequenceIndex++;
            var id = nameof(_transSequenceIndex) + _transSequenceIndex;
            var sequence = DOTween.Sequence().SetId(id);

            //计算出每个目标点的移动时间
            var everyTime = time / movePos.Length;
            //每次转弯的时间
            var lookTurnTime = everyTime * 0.5f;

            foreach (var pos in movePos)
            {
                sequence.Append(
                    moveTarget.DOMove(pos.position, everyTime)
                        .SetEase(Ease.Linear)
                        .OnStart(() =>
                            {
                                //是否是最后一个点
                                bool isLastPos;
                                if (movePos.Length <= 1)
                                {
                                    isLastPos = true;
                                }
                                else
                                {
                                    isLastPos = pos == movePos[movePos.Length - 1];
                                }

                                //它应该移动时朝向下一个点,
                                //但移动到最后一个点时,应跟最后一个点的朝向一致
                                if (isLastPos == false)
                                {
                                    moveTarget.DOLookAt(pos.position, lookTurnTime);
                                }
                                else
                                {
                                    moveTarget.DORotateQuaternion(pos.rotation, lookTurnTime);
                                }
                            }
                        )
                );
            }

            sequence.SetAutoKill(false).PlayForward();
            sequence.OnComplete(delegate { callback?.Invoke(); });

            return id;
        }

        /// <summary>
        /// 将ID中所有的动画回滚到起始状态,支持Sequence
        /// </summary>
        public static void RewindTransSequence(string id)
        {
            DOTween.Rewind(id);
        }

        /// <summary>
        /// 重新播放为ID的动画
        /// </summary>
        /// <param name="id"></param>
        public static void RestartTransSequence(string id)
        {
            DOTween.Restart(id);
        }
    }
}