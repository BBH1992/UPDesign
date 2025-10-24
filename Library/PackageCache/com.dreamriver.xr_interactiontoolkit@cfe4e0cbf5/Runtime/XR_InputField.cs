using TMPro;
using UnityEngine;

namespace SKODE
{
    /// <summary>
    /// Pico会在显示输入法时再实例化一双手,因此需要在输入框被选中时隐藏原先的手
    /// </summary>
    public class XR_InputField : MonoBehaviour
    {
        private TMP_InputField _tmpInputField;

        private void Awake()
        {
            _tmpInputField = GetComponent<TMP_InputField>();
        }

        private void Start()
        {
            if (_tmpInputField != null)
            {
                _tmpInputField.onSelect.AddListener(OnSelect);
                _tmpInputField.onDeselect.AddListener(OnDeselect);
            }
        }

        private void OnDeselect(string arg0)
        {
            Entity_LeftHand.Instance.Show();
            Entity_RightHand.Instance.Show();
        }

        private void OnSelect(string arg0)
        {
            Entity_LeftHand.Instance.Hide();
            Entity_RightHand.Instance.Hide();
        }
    }
}