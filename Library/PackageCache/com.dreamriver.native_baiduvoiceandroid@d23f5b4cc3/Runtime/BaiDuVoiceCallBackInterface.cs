using System;
using UnityEngine;

namespace SKODE
{
    public class BaiDuVoiceCallBackInterface : AndroidJavaProxy
    {
        public BaiDuVoiceCallBackInterface() : base(BaiDuVoiceManager.Packeganame + ".BaiDuVoiceCallBackInterface")
        {
        }

        public void OnRecognResult(string nameValue, string paramsValue, AndroidJavaObject data, int i,
            int i1)
        {
            BaiDuResult result = new();
            result.NameValue = nameValue;
            result.ParamsValue = paramsValue;
            result.Data = data;
            result.I = i;
            result.I1 = i1;

            BaiDuVoiceManager.onRecognResultQueue.Enqueue(result);
        }

        public void OnWakeUpResult(string nameValue, string paramsValue, AndroidJavaObject data, int i,
            int i1)
        {
            BaiDuResult result = new();
            result.NameValue = nameValue;
            result.ParamsValue = paramsValue;
            result.Data = data;
            result.I = i;
            result.I1 = i1;
            BaiDuVoiceManager.onWakeUpResultQueue.Enqueue(result);
        }
    }

    public class BaiDuResult
    {
        public string NameValue { get; set; }
        public string ParamsValue { get; set; }

        /// <summary>
        /// 子线程中不可解析AndroidJavaObject
        /// 需在主线程转化为byte[]
        /// </summary>
        public AndroidJavaObject Data { get; set; }

        public int I { get; set; }
        public int I1 { get; set; }
    }

    [Serializable]
    public class WakeUpResult
    {
        public string name;
        public string origalJson;
        public string word;
        public string desc;
        public int errorCode;
        public static int ERROR_NONE = 0;
    }
}