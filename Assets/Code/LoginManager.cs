using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public GameObject panelLogin;
    public GameObject panelRegister;

    void Start()
    {
        panelLogin.SetActive(false);
        panelRegister.SetActive(false);
    }

    public void OnClickLogin()
    {
        panelLogin.SetActive(true);
        panelRegister.SetActive(false);
    }

    public void OnClickRegister()
    {
        panelRegister.SetActive(true);
        panelLogin.SetActive(false);
    }

    public void OnClickBack()
    {
        panelLogin.SetActive(false);
        panelRegister.SetActive(false);
    }
}
