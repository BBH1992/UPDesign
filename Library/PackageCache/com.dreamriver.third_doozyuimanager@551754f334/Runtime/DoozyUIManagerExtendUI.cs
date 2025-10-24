using Doozy.Runtime.Reactor;
using Doozy.Runtime.Reactor.Targets.ProgressTargets;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine;

namespace SKODE
{
    public static class DoozyUIManagerExtendUI
    {
        public static UIButton UIButton(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIButton>();
            return value;
        }

        public static UIButton UIButton(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIButton>();
        }
        
        public static Progressor Progressor(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<Progressor>();
            return value;
        }

        public static Progressor Progressor(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<Progressor>();
        }
        
        public static ImageProgressTarget ImageProgressTarget(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<ImageProgressTarget>();
            return value;
        }

        public static ImageProgressTarget ImageProgressTarget(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<ImageProgressTarget>();
        }

        public static UIScrollbar UIScrollbar(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIScrollbar>();
            return value;
        }

        public static UIScrollbar UIScrollbar(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIScrollbar>();
        }

        public static UISlider UISlider(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UISlider>();
            return value;
        }

        public static UISlider UISlider(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UISlider>();
        }

        public static UIStepper UIStepper(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIStepper>();
            return value;
        }

        public static UIStepper UIStepper(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIStepper>();
        }

        public static UITab UITab(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UITab>();
            return value;
        }

        public static UITab UITab(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UITab>();
        }

        public static UIToggle UIToggle(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIToggle>();
            return value;
        }

        public static UIToggle UIToggle(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIToggle>();
        }
        
        public static UIContainer UIContainer(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIContainer>();
            return value;
        }

        public static UIContainer UIContainer(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIContainer>();
        }
        
        public static UIView UIView(this GameObject obj)
        {
            if (obj == null)
                return null;

            var value = obj.GetComponent<UIView>();
            return value;
        }

        public static UIView UIView(this Component obj)
        {
            if (obj == null)
                return null;

            return obj.GetComponent<UIView>();
        }
    }
}