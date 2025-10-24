using System.Collections.Generic;
using UnityEngine;

namespace SKODE
{
    public static class EntityTipManager
    {
        public class TipInfo
        {
            /// <summary>
            /// tip内容
            /// 当输入为null或空时,该项不参与运算.[即原先你的UI可能预输入了内容,现在还是显示原先内容]
            /// </summary>
            public string Content;

            /// <summary>
            /// tip跟随的transform
            /// 当为null时,若之前有跟随的trans,则还是跟随之前的trans
            /// </summary>
            public Transform OffsetTrans;

            /// <summary>
            /// 要实例化的Tip类
            /// 默认使用UITip_SampleTip
            /// </summary>
            public string UITipName = nameof(UITip_SampleTip);

            /// <summary>
            /// 交互触发的类型
            /// 该项只在 AddAndRefresh 时调用,其他方法用不到.
            /// </summary>
            public EntityTip.TriggerType TriggerType = EntityTip.TriggerType.Pointer;

            /// <summary>
            /// 触发Show的外部调用开关
            /// true:当前可触发Show
            /// </summary>
            public bool ShowSwitch = true;
        }

        public class TipRootInfo
        {
            /// <summary>
            /// tip内容
            /// 当输入为null或空时,该项不参与运算.[即原先你的UI可能预输入了内容,现在还是显示原先内容]
            /// </summary>
            public string Content;

            /// <summary>
            /// tip跟随的transform
            /// 当为null时,若之前有跟随的trans,则还是跟随之前的trans
            /// </summary>
            public Transform OffsetTrans;

            /// <summary>
            /// 要实例化的Tip类
            /// 默认使用UITip_SampleTip
            /// </summary>
            public string UITipName = nameof(UITip_SampleTip);

            /// <summary>
            /// 触发Show的外部调用开关
            /// true:当前可触发Show
            /// </summary>
            public bool ShowSwitch = true;
        }

        /// <summary>
        /// 给当前实体添加或更新Tip.注意:当posTrans不在屏幕内时,Tip不会显示
        /// </summary>
        public static EntityTip AddRefreshTip(this Component component, TipInfo info)
        {
            if (component.GetComponent<Collider>() == null)
            {
                var collider = component.gameObject.AddComponent<MeshCollider>();
                collider.convex = true;
            }

            var entityTip = component.GetComponent<EntityTip>();

            if (entityTip != null)
            {
                info.OffsetTrans = entityTip.TipInfo.OffsetTrans;
            }
            else
            {
                // 默认跟随当前实体的transform
                info.OffsetTrans ??= component.transform;
                entityTip = component.gameObject.AddComponent<EntityTip>();
            }

            entityTip.Init(component.transform, info);

            return entityTip;
        }

        /// <summary>
        /// 给当前实体根节点添加或更新Tip.注意:当posTrans不在屏幕内时,Tip不会显示
        /// 它会自动遍历所有子节点,并添加 meshcolloder和 uiTip
        /// </summary>
        public static List<EntityTip> AddRefreshTipRoot(this Component component, TipInfo info)
        {
            List<EntityTip> value = new List<EntityTip>();
            foreach (var variable in component.GetComponentsInChildren<Renderer>())
            {
                var entity = AddRefreshTip(variable, info);
                value.Add(entity);
            }

            return value;
        }

        /// <summary>
        /// 显示当前实体的Tip.(不会自动隐藏,需要手动调用HideEntityTip(),或该tip的hide)
        /// </summary>
        public static EntityTip ShowTip(this Component component, TipRootInfo rootInfo)
        {
            TipInfo tipInfo = new TipInfo()
            {
                Content = rootInfo.Content,
                OffsetTrans = rootInfo.OffsetTrans,
                UITipName = rootInfo.UITipName,
                TriggerType = EntityTip.TriggerType.ManualTrigger,
                ShowSwitch = rootInfo.ShowSwitch
            };

            return AddRefreshTip(component, tipInfo);
        }

        /// <summary>
        /// 显示当前实体的Tip.(不会自动隐藏,需要手动调用HideEntityTip(),或该tip的hide)
        /// </summary>
        public static List<EntityTip> ShowTipRoot(this Component component, TipRootInfo rootInfo)
        {
            TipInfo tipInfo = new TipInfo
            {
                Content = rootInfo.Content,
                OffsetTrans = rootInfo.OffsetTrans,
                UITipName = rootInfo.UITipName,
                TriggerType = EntityTip.TriggerType.ManualTrigger,
                ShowSwitch = rootInfo.ShowSwitch
            };

            return AddRefreshTipRoot(component, tipInfo);
        }

        /// <summary>
        /// 隐藏当前实体的Tip
        /// </summary>
        public static EntityTip HideTip(this Component component)
        {
            var value = component.GetComponent<EntityTip>();

            if (value != null)
                value.Hide();

            return value;
        }

        /// <summary>
        /// 隐藏当前实体的Tip
        /// </summary>
        public static List<EntityTip> HideTipRoot(this Component component)
        {
            List<EntityTip> value = new List<EntityTip>();
            foreach (var variable in component.GetComponentsInChildren<EntityTip>())
            {
                var tip = variable.GetComponent<EntityTip>();

                if (tip != null)
                    tip.Hide();

                value.Add(tip);
            }

            return value;
        }
    }
}