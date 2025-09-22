using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;       // Gán các nút Level trong Inspector
    public GameObject lockIconPrefab;   // Prefab hình khóa (UI Image nhỏ)

    private void Start()
    {
        // Lấy tiến độ mở khóa từ slot hiện tại
        int currentSlot = PlayerPrefs.GetInt("CurrentSlot", -1);
        int unlockedLevel = PlayerPrefs.GetInt("Slot" + currentSlot + "_UnlockedLevel", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1; // Level 1, 2, 3...
            int capturedIndex = levelIndex; // tránh bug lambda

            if (levelIndex <= unlockedLevel)
            {
                // Mở khóa => cho phép bấm
                levelButtons[i].interactable = true;
            }
            else
            {
                // Khóa => disable nút
                levelButtons[i].interactable = false;

                // Thêm hình khóa vào nút
                if (lockIconPrefab != null)
                {
                    GameObject lockObj = Instantiate(lockIconPrefab, levelButtons[i].transform);
                    lockObj.transform.SetAsLastSibling(); // cho icon nằm trên cùng
                }
            }

            // Gán sự kiện click cho nút
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
        int currentSlot = PlayerPrefs.GetInt("CurrentSlot", -1);
        int unlockedLevel = PlayerPrefs.GetInt("Slot" + currentSlot + "_UnlockedLevel", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("Slot" + currentSlot + "_UnlockedLevel", currentLevel + 1);
            PlayerPrefs.SetInt("CurrentUnlockedLevel", currentLevel + 1); // update luôn biến tạm
            PlayerPrefs.Save();
        }
    }
}
