using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelLogin;
    public GameObject panelRegister;

    [Header("Login UI")]
    public TMP_InputField loginUsernameInput;
    public TMP_InputField loginPasswordInput;

    [Header("Register UI")]
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerPasswordInput;

    [Header("Notification")]
    public TextMeshProUGUI notificationText;
    public float messageDuration = 2f;

    private AccountList accountList = new AccountList();

    void Start()
    {
        panelLogin.SetActive(false);
        panelRegister.SetActive(false);
        notificationText.text = "";
        LoadAccounts();
    }

    void LoadAccounts()
    {
        if (PlayerPrefs.HasKey("Accounts"))
        {
            string json = PlayerPrefs.GetString("Accounts");
            accountList = JsonUtility.FromJson<AccountList>(json);
        }
        else
        {
            accountList = new AccountList();
        }
    }

    void SaveAccounts()
    {
        string json = JsonUtility.ToJson(accountList);
        PlayerPrefs.SetString("Accounts", json);
        PlayerPrefs.Save();
    }

    public void OpenLoginPanel()
    {
        panelLogin.SetActive(true);
        panelRegister.SetActive(false);
    }

    public void OpenRegisterPanel()
    {
        panelRegister.SetActive(true);
        panelLogin.SetActive(false);
    }

    public void ClosePanels()
    {
        panelLogin.SetActive(false);
        panelRegister.SetActive(false);
    }

    // ===== LOGIN =====
    public void ConfirmLogin()
    {
        string username = loginUsernameInput.text.Trim();
        string password = loginPasswordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Username or password cannot be empty!");
            return;
        }

        foreach (var acc in accountList.accounts)
        {
            if (acc.username == username)
            {
                if (acc.password == password)
                {
                    ShowMessage("Login successful!");
                    PlayerPrefs.SetString("CurrentUser", username);

                    // 👉 Chuyển sang LoadingScene, rồi nó sẽ tự load sang MenuScene
                    LoadingScreen.nextScene = "Menu"; // nhớ đổi tên theo scene thật
                    SceneManager.LoadScene("LoadingScene");
                    return;
                }
                else
                {
                    ShowMessage("Wrong password!");
                    return;
                }
            }
        }

        ShowMessage("Account does not exist!");
    }

    // ===== REGISTER =====
    public void ConfirmRegister()
    {
        string username = registerUsernameInput.text.Trim();
        string password = registerPasswordInput.text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowMessage("Username or password cannot be empty!");
            return;
        }

        foreach (var acc in accountList.accounts)
        {
            if (acc.username == username)
            {
                ShowMessage("Account already exists!");
                return;
            }
        }

        Account newAcc = new Account();
        newAcc.username = username;
        newAcc.password = password;
        accountList.accounts.Add(newAcc);

        SaveAccounts();

        ShowMessage("Registration successful!");
    }

    // ===== NOTIFICATION =====
    void ShowMessage(string msg)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayMessage(msg));
    }

    IEnumerator DisplayMessage(string msg)
    {
        notificationText.text = msg;
        notificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        notificationText.text = "";
        notificationText.gameObject.SetActive(false);
    }
}
