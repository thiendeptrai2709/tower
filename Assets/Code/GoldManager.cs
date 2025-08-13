using UnityEngine;
using TMPro; // Quan trọng: dùng TextMeshPro

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    public int gold = 0;                 // Số vàng hiện tại
    public TextMeshProUGUI goldText;     // UI TMP hiển thị vàng

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateUI();
            return true;
        }
        return false; // Không đủ vàng
    }

    void UpdateUI()
    {
        if (goldText != null)
            goldText.text = "Gold: " + gold;
    }
}
