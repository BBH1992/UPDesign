using SKODE;

/// <summary>
/// 跟随目标物体的提示框
/// </summary>
public class UITip_SampleTip : BaseDoozyUIManager
{
    protected UITipData Data;

    protected override void Awake()
    {
        base.Awake();

        Data = GetComponent<UITipData>();

        // 隐藏自身的Canvas,在EntityTip中会自动打开
        // 避免初始化没完成时,显示在屏幕上
        transform.Canvas().enabled = false;
    }

    protected virtual void Update()
    {
        // 更新可视化和位置
        RefreshVisableAndPos();
    }

    /// <summary>
    /// 更新可视化和位置
    /// 实现当posTrans不在屏幕内时,Tip不会显示
    /// </summary>
    private void RefreshVisableAndPos()
    {
        // 当没有跟随的目标或相机不正常
        if (Data.offsetTarget == null || CameraCurrent == null)
        {
            transform.Canvas().enabled = false;
            return;
        }

        // 当目标不在屏幕内
        if (DreamRiverEntity.GetObjCenterVisable(CameraCurrent, Data.offsetTarget) == false)
        {
            transform.Canvas().enabled = false;
            return;
        }

        transform.position = CameraCurrent.GetEntityInScreenPos(Data.offsetTarget.transform);
        transform.Canvas().enabled = true;
    }
}