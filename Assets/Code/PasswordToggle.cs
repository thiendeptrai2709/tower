using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    public TMP_InputField passwordInput;
    private bool isHidden = true;

    public void TogglePassword()
    {
        isHidden = !isHidden;
        if (isHidden)
        {
            // Ẩn mật khẩu
            passwordInput.contentType = TMP_InputField.ContentType.Password;
        }
        else
        {
            // Hiện mật khẩu
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
        }
        // Refresh lại field để áp dụng thay đổi
        passwordInput.ForceLabelUpdate();
    }
}
