namespace SKODE
{
    /// <summary>
    /// 解决Tab下的UIContainer显示不正常问题
    /// 具体表现在:本应在最上层的UI却被下层Tab的UIContainer遮挡
    /// 使用方法:将本脚本挂载到UIContainer上
    /// </summary>
    public class FixUIContainer : BaseDoozyUIManager
    {
        protected override void Awake()
        {
            base.Awake();

            uiContainer.OnShowCallback.Event.AddListener(delegate
            {
                transform.Canvas().overrideSorting = true;
                transform.Canvas().overrideSorting = false;
            });
        }
    }
}