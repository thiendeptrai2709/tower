using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;       // Gán các nút Level trong Inspector
    public GameObject lockIconPrefab;   // Prefab hình khóa (UI Image nhỏ)

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            int capturedIndex = levelIndex;

            if (levelIndex <= unlockedLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;

                if (lockIconPrefab != null)
                {
                    GameObject lockObj = Instantiate(lockIconPrefab, levelButtons[i].transform);
                    lockObj.transform.SetAsLastSibling();
                }
            }

            levelButtons[i].onClick.AddListener(() => LoadLevel(capturedIndex));
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    // Gọi khi thắng màn
    public static void UnlockNextLevel(int currentLevel)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
