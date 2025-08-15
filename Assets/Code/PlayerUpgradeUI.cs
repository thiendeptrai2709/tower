using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpgradeUI : MonoBehaviour
{
    public GameObject panel;
    public Button damageButton;
    public Button fireRateButton;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI fireRateText;

    private PlayerUpgrade currentTower;

    void Start()
    {
        panel.SetActive(false);

        damageButton.onClick.AddListener(() =>
        {
            if (currentTower != null)
            {
                currentTower.UpgradeDamage();
                UpdateUI();
                Hide();
            }
        });

        fireRateButton.onClick.AddListener(() =>
        {
            if (currentTower != null)
            {
                currentTower.UpgradeFireRate();
                UpdateUI();
                Hide();
            }
        });
    }

    public void Show(PlayerUpgrade tower)
    {
        currentTower = tower;
        panel.SetActive(true);
        UpdateUI();
    }

    public void Hide()
    {
        panel.SetActive(false);
        currentTower = null;
    }

    private void UpdateUI()
    {
        if (currentTower != null && currentTower.playerAttack != null)
        {
            damageText.text = "Damage: " + currentTower.bulletDamage;
            fireRateText.text = "Fire Rate: " + currentTower.playerAttack.fireCooldown.ToString("F2");
        }
        else
        {
            damageText.text = "Damage: -";
            fireRateText.text = "Fire Rate: -";
        }
    }
}
