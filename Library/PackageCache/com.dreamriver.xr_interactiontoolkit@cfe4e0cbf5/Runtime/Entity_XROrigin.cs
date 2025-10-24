using System.Reflection;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

namespace SKODE
{
    public class Entity_XROrigin : SingleMonoBehaviour<Entity_XROrigin>
    {
        private XROrigin _xrOrigin;

        private void Awake()
        {
            _xrOrigin = GetComponent<XROrigin>();
            
            FieldInfo trackingOriginMode = typeof(XROrigin).GetField("m_RequestedTrackingOriginMode",
                BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo currentTrackingOriginMode = typeof(XROrigin).GetProperty("CurrentTrackingOriginMode",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.OSXEditor)
            {
                trackingOriginMode.SetValue(_xrOrigin, XROrigin.TrackingOriginMode.Floor);
                currentTrackingOriginMode.SetValue(_xrOrigin, TrackingOriginModeFlags.Floor, null);
            }
            else
            {
                trackingOriginMode.SetValue(_xrOrigin, XROrigin.TrackingOriginMode.Device);
                currentTrackingOriginMode.SetValue(_xrOrigin, TrackingOriginModeFlags.Device, null);
            }
        }
    }
}