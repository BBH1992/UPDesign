using SKODE;
using UnityEngine;
public class VoiceManager : MonoBehaviour
{
    public bool IsDontDestroyOnLoad;
    private void Awake()
    {

        if (IsDontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

        Invoke("Init", 1f);

    }
    void Init()
    {
#if !UNITY_EDITOR
        BaiDuVoiceManager.Instance.Init("72766871", "fMFjXwGrQhVRtE7wpQXvFWdW",
        "eFV43MkpjoZ6rsIrIFx7Ze0G1OwTcEkH");
        BaiDuVoiceManager.Instance.StartWakeUpAutoRec(1737);
#endif
    }

}
