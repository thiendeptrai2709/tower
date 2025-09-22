using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject continuePanel;      // Panel hiện khi bấm Continue
    public Button[] slotButtons;          // 5 nút slot (Play)
    public TMP_Text[] slotTexts;          // 5 TMP text hiển thị trạng thái mỗi slot
    public Button togglePanelButton;      // Nút bật/tắt panel Continue

    private void Awake()
    {
        // an toàn: nếu chưa gán panel thì tránh null ref
        if (continuePanel != null)
            continuePanel.SetActive(false);
    }

    private void Start()
    {
        // gán nút bật/tắt panel (nếu có)
        if (togglePanelButton != null)
            togglePanelButton.onClick.AddListener(TogglePanel);

        // gán listener cho các nút slot (chỉ 1 lần)
        if (slotButtons != null)
        {
            for (int i = 0; i < slotButtons.Length; i++)
            {
                int index = i + 1; // Level/Slot index bắt đầu từ 1
                slotButtons[i].onClick.RemoveAllListeners(); // tránh add nhiều lần
                slotButtons[i].onClick.AddListener(() => SelectSlot(index));
            }
        }

        // cập nhật text lần đầu
        UpdateSlotTexts();
    }

    // Cập nhật text hiển thị cho từng slot
    private void UpdateSlotTexts()
    {
        if (slotTexts == null) return;

        for (int i = 0; i < slotTexts.Length; i++)
        {
            int slotIndex = i + 1;
            int unlocked = PlayerPrefs.GetInt("Slot" + slotIndex + "_UnlockedLevel", 0); // 0 = empty

            if (unlocked <= 0)
                slotTexts[i].text = $"Slot {slotIndex} - Empty";
            else
                slotTexts[i].text = $"Slot {slotIndex} - Level {unlocked}";
        }
    }

    // New Game -> reset slot về Level 1 và chuyển tới LevelMap
    public void NewGame(int slotIndex)
    {
        // lưu tiến độ cho slot
        PlayerPrefs.SetInt("Slot" + slotIndex + "_UnlockedLevel", 1);
        PlayerPrefs.SetInt("CurrentSlot", slotIndex);
        PlayerPrefs.SetInt("CurrentUnlockedLevel", 1);
        PlayerPrefs.Save();

        // cập nhật UI (nếu bạn vẫn ở menu sẽ thấy thay đổi)
        UpdateSlotTexts();

        // LƯU Ý: nếu bạn load scene ngay thì sẽ không kịp "nhìn" thấy text thay đổi
        // SceneManager.LoadScene("LevelMap");
        // Nếu bạn muốn thấy text rồi mới chuyển, comment dòng trên và gọi LoadScene sau 0.5s hoặc bằng 1 nút tiếp theo.
        SceneManager.LoadScene("LevelMap");
    }

    // Bật/tắt panel Continue
    public void TogglePanel()
    {
        if (continuePanel == null) return;
        bool newState = !continuePanel.activeSelf;
        continuePanel.SetActive(newState);
        if (newState) UpdateSlotTexts(); // refresh khi mở panel
    }

    // Khi chọn 1 slot để tiếp tục chơi
    private void SelectSlot(int slotIndex)
    {
        PlayerPrefs.SetInt("CurrentSlot", slotIndex);
        int unlocked = PlayerPrefs.GetInt("Slot" + slotIndex + "_UnlockedLevel", 1);
        PlayerPrefs.SetInt("CurrentUnlockedLevel", unlocked);
        PlayerPrefs.Save();

        // load scene chọn map
        SceneManager.LoadScene("LevelMap");
    }

    // Hàm unlock khi thắng màn
    public static void UnlockNextLevel(int currentLevel)
    {
        int slot = PlayerPrefs.GetInt("CurrentSlot", -1);
        if (slot == -1) return;

        int unlocked = PlayerPrefs.GetInt("Slot" + slot + "_UnlockedLevel", 1);
        if (currentLevel >= unlocked)
        {
            PlayerPrefs.SetInt("Slot" + slot + "_UnlockedLevel", currentLevel + 1);
            PlayerPrefs.SetInt("CurrentUnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    // helper debug (gọi từ button nếu cần)
    public void DebugPrintPrefs()
    {
        Debug.Log("CurrentSlot = " + PlayerPrefs.GetInt("CurrentSlot", -1));
        for (int i = 1; i <= (slotTexts?.Length ?? 0); i++)
        {
            Debug.Log($"Slot{i}_UnlockedLevel = " + PlayerPrefs.GetInt("Slot" + i + "_UnlockedLevel", 0));
        }
    }
}
