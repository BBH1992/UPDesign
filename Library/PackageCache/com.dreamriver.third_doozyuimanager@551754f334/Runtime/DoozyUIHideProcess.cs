using System;
using System.Threading.Tasks;

namespace SKODE
{
    public class DoozyUIHideProcess
    {
        private Action _onCompleteAction;
        private bool _isHideComplete;

        /// <summary>
        /// 此刻刚开始执行Hide
        /// </summary>
        /// <param name="hideTimeInSeconds"></param>
        /// <param name="hideAction"></param>
        public DoozyUIHideProcess(float hideTimeInSeconds, Action hideAction)
        {
            hideAction?.Invoke();
            ExecuteAfterDelay(hideTimeInSeconds);
        }

        /// <summary>
        /// 调用它默认表明会打开新界面
        /// </summary>
        /// <param name="action"></param>
        public void OnComplete(Action action)
        {
            _onCompleteAction = action;

            if (_isHideComplete)
                ExecuteOnCompleteAction();
        }

        private async Task ExecuteAfterDelay(float delayInSeconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(delayInSeconds));

            ExecuteOnCompleteAction();
        }

        private void ExecuteOnCompleteAction()
        {
            _isHideComplete = true;
            _onCompleteAction?.Invoke();
            _onCompleteAction = null;
        }
    }
}