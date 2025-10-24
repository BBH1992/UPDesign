using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace SKODE
{
    //在线查看调用失败原因：监控报表，选中失败数
    //https://console.bce.baidu.com/ai/#/ai/speech/report
    public class BaiDuVoiceManager : SingleMonoBehaviour<BaiDuVoiceManager>
    {
        /// <summary>
        /// 唤醒是否成功，唤醒词/报错值
        /// </summary>
        public Action<WakeResult> OnWalkUpResult;

        /// <summary>
        /// 语音识别结果
        /// </summary>
        public Action<RecognitionResult> OnRecResult;

        [Header("是否打印安卓日志"), SerializeField]
        private bool openNativeDebug;

        public const string Packeganame = "com.skode.baiduvoice";
        private AndroidJavaObject _baiDuVoiceManager;
        public static Queue<BaiDuResult> onWakeUpResultQueue = new();
        public static Queue<BaiDuResult> onRecognResultQueue = new();

        /// <summary>
        /// 唤醒后自动识别功能，该识别语音功能用的pid模型值
        /// -1时表示不开启自动识别功能
        /// </summary>
        private int _wakeUpAutoRecPidMode = -1;

        protected override void Start()
        {
            base.Start();

            OnWalkUpResult += OnWakeUpHandle;
        }

        private void Update()
        {
            if (onRecognResultQueue.Count > 0)
            {
                ParseRecognResult(onRecognResultQueue.Dequeue());
            }

            if (onWakeUpResultQueue.Count > 0)
            {
                ParseWakeupResult(onWakeUpResultQueue.Dequeue());
            }
        }

        /// <summary>
        /// 初始化百度语音
        /// https://console.bce.baidu.com/ai/?fromai=1#/ai/speech/app/list
        /// </summary>
        public void Init(string appID, string apiKey, string secretKey)
        {
            AndroidJavaClass jc = new AndroidJavaClass(Packeganame + ".BaiDuVoiceManager");
            _baiDuVoiceManager = jc.CallStatic<AndroidJavaObject>("getInstance");

            var mCallbackListener = new BaiDuVoiceCallBackInterface();
            _baiDuVoiceManager.Call("Init", DreamRiverComAndroid.currentActivity,
                mCallbackListener, appID, apiKey, secretKey);
        }

        /// <summary>
        /// 开启功能：语音唤醒
        /// </summary>
        public void StartWakeUp()
        {
            _baiDuVoiceManager.Call("StartWakeUp");
        }

        /// <summary>
        /// 停止功能：语音唤醒
        /// </summary>
        public void StopWakeUp()
        {
            _baiDuVoiceManager.Call("StopWakeUp");
        }

        /// <summary>
        /// 开启功能：语音唤醒 + 唤醒后立刻识别语音并返回结果
        /// 返回的结果在OnRecResult
        /// 唤醒后识别语音的时长为一句话时长
        /// 英文等需要在官网购买开通识别服务：语音技术-概览-服务列表-语音识别，领取后需在服务列表中找到它，进行开通。
        /// 1537,普通话(纯中文识别),有标点
        /// 1737,英语,无标点
        /// 1637,粤语,有标点
        /// 1837,四川话,有标点
        /// 1936,普通话远场，有标点
        /// </summary>
        public void StartWakeUpAutoRec(int pidValue = 1537)
        {
            StartWakeUp();
            _wakeUpAutoRecPidMode = pidValue;
        }

        /// <summary>
        /// 停止功能：语音唤醒 + 唤醒后立刻识别语音并返回结果
        /// </summary>
        public void StopWakeUpAutoRec()
        {
            StopWakeUp();
            _wakeUpAutoRecPidMode = -1;
        }

        /// <summary>
        /// 开始语音识别，时长为一句话时长
        /// 1537,普通话(纯中文识别),有标点
        /// 1737,英语,无标点
        /// 1637,粤语,有标点
        /// 1837,四川话,有标点
        /// 1936,普通话远场，有标点
        /// </summary>
        public void StartRecogn(int model = 1537)
        {
            _baiDuVoiceManager.Call("StartRecogn", model);
        }

        /// <summary>
        /// 停止语音识别
        /// </summary>
        public void StopRecogn()
        {
            _baiDuVoiceManager.Call("StopRecogn");
        }

        private void ParseWakeupResult(BaiDuResult value)
        {
            if (openNativeDebug)
            {
                Debug.Log(JsonConvert.SerializeObject(value));
            }

            var result = JsonConvert.DeserializeObject<WakeResult>(value.ParamsValue);
            if (value.NameValue == "wp.data")
            {
                OnWalkUpResult?.Invoke(result);
                Debug.Log(JsonConvert.SerializeObject(result));
            }
            else if (value.NameValue == "wp.error")
            {
                Debug.LogError(JsonConvert.SerializeObject(value));
            }
            else if (value.NameValue == "wp.audio")
            {
            }
        }

        private void ParseRecognResult(BaiDuResult value)
        {
            if (openNativeDebug)
            {
                Debug.Log(JsonConvert.SerializeObject(value));
            }

            //语音识别结果会把状态、识别结果等等信息都返回
            //这里只处理识别结果
            if (value.ParamsValue.Contains("results_recognition"))
            {
                var result = JsonConvert.DeserializeObject<RecognitionResult>(value.ParamsValue);
                OnRecResult?.Invoke(result);
                Debug.Log("ParseRecognResult" + result.error);
            }
        }

        private void OnWakeUpHandle(WakeResult obj)
        {
            //实现功能：
            //语音唤醒后，若要求唤醒后立刻识别语音，则开始识别语音
            if (_wakeUpAutoRecPidMode != -1)
            {
                StartRecogn(_wakeUpAutoRecPidMode);
            }
        }
    }

    [Serializable]
    public class WakeResult
    {
        public string errorDesc;
        public int errorCode;
        public string word;
    }

    [Serializable]
    public class RecognitionResult
    {
        public string[] results_recognition { get; set; }

        /// <summary>
        /// 识别结果类型
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultType result_type { get; set; }

        /// <summary>
        /// 本次识别的最佳结果
        /// </summary>
        public string best_result { get; set; }

        public OriginResult origin_result { get; set; }
        public int error { get; set; }
    }

    [Serializable]
    public class OriginResult
    {
        public int err_no { get; set; }
        public Result result { get; set; }
        public long corpus_no { get; set; }
        public string sn { get; set; }
        public int product_id { get; set; }
        public string product_line { get; set; }
        public string result_type { get; set; }
    }

    [Serializable]
    public class Result
    {
        public string[] word { get; set; }
        public int[] confident { get; set; }
    }

    /// <summary>
    /// 识别结果类型
    /// </summary>
    [Serializable]
    public enum ResultType
    {
        /// <summary>
        /// 临时识别结果
        /// </summary>
        partial_result,

        /// <summary>
        /// 最终识别结果
        /// </summary>
        final_result
    }
}