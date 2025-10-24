using System;
using Unity.Mathematics;
using Unity.XR.CoreUtils.Bindings;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Tweenables.Primitives;
using System.Collections;

namespace SKODE
{
    public class DreamRiver_LazyFollow : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// true,更新位置时中心点高度自动调整高度到眼睛高度；false，保持原高度
        /// </summary>
        private bool _adjustHeight;

        private Transform _target;

        /// <summary>
        /// 默认Camera.Main，也可代码赋值
        /// </summary>
        private Transform Target
        {
            set { _target = value; }
            get
            {
                if (_target == null)
                    _target = Camera.main.transform;

                return _target;
            }
        }

        [SerializeField, Tooltip("与target的偏移距离")]
        private Vector3 targetOffset = Vector3.forward;

        [SerializeField, Tooltip("是否在Enable时，将自身位置设置为目标位置")]
        private bool snapOnEnable = true;

        private float _movementSpeed = 7f;
        private float _minDistanceAllowed = 0.01f;
        private float _mMaxDistanceAllowed = 0.3f;
        private float _minAngleAllowed = 0.01f;
        private float _maxAngleAllowed = 0.3f;

        private float _mTimeUntilThresholdReachesMaxDistance = 3f;
        private float _mTimeUntilThresholdReachesMaxAngle = 3f;

        private Vector3 MTargetPosition => Target.position + Target.TransformVector(targetOffset);

        private readonly BindingsGroup _mBindingsGroup = new();

        private Vector3 _mLastTargetPosition = Vector3.zero;

        private Vector3TweenableVariable _mVector3TweenableVariable;
        private QuaternionTweenableVariable _mQuaternionTweenableVariable;

        private float _mLastUpdateTime;
        private bool _isFlowing;

        #endregion

        protected void Awake()
        {
            _mVector3TweenableVariable = new Vector3TweenableVariable();
            _mQuaternionTweenableVariable = new QuaternionTweenableVariable();
        }

        protected void OnEnable()
        {
            var thisTransform = transform;
            
            //若启用snapOnEnable，
            //每次Enable时，将自身位置、角度设置为目标位置
            if (snapOnEnable && Target != null)
            {
                thisTransform.position = MTargetPosition;

                TryGetThresholdTargetRotation(out var newRotation);
                thisTransform.rotation = newRotation;
            }

            var currentPosition = thisTransform.position;
            var currentRotation = thisTransform.rotation;

            _mVector3TweenableVariable.Value = currentPosition;
            _mVector3TweenableVariable.target = currentPosition;

            _mQuaternionTweenableVariable.Value = currentRotation;
            _mQuaternionTweenableVariable.target = currentRotation;

            _mBindingsGroup.AddBinding(_mVector3TweenableVariable.SubscribeAndUpdate(UpdatePosition));
            _mBindingsGroup.AddBinding(_mQuaternionTweenableVariable.SubscribeAndUpdate(UpdateRotation));
        }

        protected void LateUpdate()
        {
            if (_isFlowing == false || Target == null)
                return;

            if (MTargetPosition != _mLastTargetPosition)
            {
                _mLastUpdateTime = Time.unscaledTime;
                _mLastTargetPosition = MTargetPosition;
            }

            var targetWithinUpdateThreshold = TryGetThresholdTargetPosition(out var newTarget);
            if (targetWithinUpdateThreshold)
            {
                _mVector3TweenableVariable.target = newTarget;
            }

            var rotationWithinUpdateThreshold = TryGetThresholdTargetRotation(out var newRotation);
            if (rotationWithinUpdateThreshold)
            {
                _mQuaternionTweenableVariable.target = newRotation;
            }

            var tweenTarget = Time.unscaledDeltaTime * _movementSpeed;
            _mVector3TweenableVariable.HandleTween(tweenTarget);

            //解决第一个角度旋转360问题，利用快速旋转，解决第一次问题
            float rotateSpeed = tweenTarget;

            _mQuaternionTweenableVariable.HandleTween(rotateSpeed);
        }

        private void UpdatePosition(float3 position)
        {
            transform.position = position;
        }

        private void UpdateRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        protected virtual bool TryGetThresholdTargetPosition(out Vector3 newTarget)
        {
            var startHeight = transform.position.y;
            var camHeight = Target.position.y;

            var staryYPosition = new Vector3(MTargetPosition.x, startHeight, MTargetPosition.z);
            var adjustYPosition = new Vector3(MTargetPosition.x, camHeight, MTargetPosition.z);

            //判断是否需要调整高度
            newTarget = _adjustHeight ? adjustYPosition : staryYPosition;

            //调整距离
            float currDistance = Vector3.Distance(newTarget, Target.position);
            float poor = targetOffset.z - currDistance;
            newTarget += new Vector3(0, 0, poor);

            // newTarget = m_TargetPosition;

            var newSqrTargetOffset = Vector3.Distance(_mVector3TweenableVariable.target, newTarget);
            var timeSinceLastUpdate = Time.unscaledTime - _mLastUpdateTime;

            var allowedTargetDistanceOffset = Mathf.Lerp(_minDistanceAllowed, _mMaxDistanceAllowed,
                Mathf.Clamp01(timeSinceLastUpdate / _mTimeUntilThresholdReachesMaxDistance));
            return newSqrTargetOffset > (allowedTargetDistanceOffset * allowedTargetDistanceOffset);
        }

        protected virtual bool TryGetThresholdTargetRotation(out Quaternion newTarget)
        {
            var forward = gameObject.transform.position - Target.position;
            var right = Vector3.Cross(forward, Vector3.up);
            var up = Vector3.Cross(forward, right);
            newTarget = Quaternion.LookRotation(forward, up);

            //解决角度旋转360问题
            Vector3 eulerAngles = newTarget.eulerAngles;
            eulerAngles.z = 0;
            newTarget = Quaternion.Euler(eulerAngles);

            var angle = Quaternion.Angle(_mQuaternionTweenableVariable.target, newTarget);
            var timeSinceLastUpdate = Time.unscaledTime - _mLastUpdateTime;

            var allowedTargetAngleOffset = Mathf.Lerp(_minAngleAllowed, _maxAngleAllowed,
                Mathf.Clamp01(timeSinceLastUpdate / _mTimeUntilThresholdReachesMaxAngle));

            return angle > (allowedTargetAngleOffset * allowedTargetAngleOffset);
        }

        /// <summary>
        /// 设置跟随目标
        /// </summary>
        public void SetFollowAim(Transform targetValue)
        {
            Target = targetValue;
        }

        /// <summary>
        /// 在duration时间内跟随到目标点
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        /// <param name="adjustHeightValue">true,更新位置时中心点高度自动调整高度到眼睛高度；false，保持原高度</param>
        public void StartFollowing(float duration, bool adjustHeightValue = false, Action onComplete = null)
        {
            StartCoroutine(FollowCoroutine(duration, adjustHeightValue, onComplete));
        }

        private IEnumerator FollowCoroutine(float duration, bool adjustHeightValue, Action onComplete)
        {
            var startTime = Time.time;
            _adjustHeight = adjustHeightValue;
            _isFlowing = true;

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = MTargetPosition;
            float distance = Vector3.Distance(startPosition, targetPosition);
            _movementSpeed = (distance / duration);

            while (Time.time - startTime < duration)
            {
                yield return null;
            }

            _isFlowing = false;
            onComplete?.Invoke();

            foreach (var variable in GetComponentsInChildren<DreamRiver_LazyFollowItem>())
            {
                variable.DoLookAt(Target.position, 0.2f);
            }
        }

        protected void OnDisable()
        {
            _mBindingsGroup.Clear();
        }

        protected void OnDestroy()
        {
            _mVector3TweenableVariable?.Dispose();
        }
    }
}