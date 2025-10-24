# 一、项目配置方法
1.将app包名配置到百度语音官网,并得到appID、apiKey、secretKey
https://console.bce.baidu.com/ai/?fromai=1#/ai/speech/app/list

2.替换唤醒词
替换位置：打开本插件-Plugins\Android\baiduvoice-debug.aar
将assets中的bin替换为你在官网导出的唤醒词bin

3.将 BaiDuVoiceManager 脚本添加到场景。


# 二、使用示例
以下功能实现：
语音唤醒 + 唤醒后立刻识别语音并返回结果。
类似小爱同学，唤醒后立刻识别讲的语音并返回结果。

    private void Start()
    {
        BaiDuVoiceManager.Instance.OnRecResult += OnRec;

        //配置appID、apiKey、secretKey
        BaiDuVoiceManager.Instance.Init("xxx", "xxx","xxx");
        BaiDuVoiceManager.Instance.StartWakeUpAutoRec();
    }

    private void OnRec(RecognitionResult obj)
    {
        GameObject.Find("Text (Legacy)111").GetComponent<Text>().text += "\n" + obj.best_result;
    }