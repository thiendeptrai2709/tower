using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // nếu nút Play dùng TMP_Text

public class MainMenu : MonoBehaviour
{
    public Button playButton;     // Nút Play duy nhất
    public TMP_Text playButtonText; // Text trên nút Play

    private void Start()
    {
        UpdatePlayButtonText();

        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
    }

    private void UpdatePlayButtonText()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 0);

        if (unlocked <= 0)
        {
            if (playButtonText != null)
                playButtonText.text = "New Game";
        }
        else
        {
            if (playButtonText != null)
                playButtonText.text = "Continue";
        }
    }

    private void OnPlayClicked()
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 0);

        if (unlocked <= 0)
        {
            // Chưa có save -> bắt đầu New Game từ Level 1
            PlayerPrefs.SetInt("UnlockedLevel", 1);
            PlayerPrefs.Save();
            Debug.Log("Bắt đầu New Game từ Level 1");
        }
        else
        {
            Debug.Log("Continue game với UnlockedLevel = " + unlocked);
        }

        SceneManager.LoadScene("LevelMap"); // luôn chuyển sang chọn Map
    }
}
