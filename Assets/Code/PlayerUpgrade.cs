using UnityEngine;

public class PlayerUpgrade : MonoBehaviour
{
    public int bulletDamage = 10;               // damage mặc định
    public int upgradeDamageCost = 20;
    public int upgradeFireRateCost = 30;
    public int damageIncrease = 5;
    public float fireRateDecrease = 0.1f;

    public PlayerUpgradeUI upgradeUI;           // gán panel UI trong prefab

    [HideInInspector] public PlayerAttack playerAttack; // instance PlayerAttack

    private int bulletDamageOriginal;
    private float fireCooldownOriginal;
    private float currentFireCooldown;          // fireCooldown riêng cho instance

    void Awake()
    {
        // Lấy component PlayerAttack trên chính instance
        playerAttack = GetComponent<PlayerAttack>();

        // Lấy damage từ bulletPrefab nếu có
        if (playerAttack != null && playerAttack.bulletPrefab != null)
        {
            Bullet bulletPrefabScript = playerAttack.bulletPrefab.GetComponent<Bullet>();
            if (bulletPrefabScript != null)
                bulletDamage = bulletPrefabScript.damage;
        }

        bulletDamageOriginal = bulletDamage;

        if (playerAttack != null)
        {
            fireCooldownOriginal = playerAttack.fireCooldown; // giá trị gốc prefab
            currentFireCooldown = fireCooldownOriginal;       // lưu riêng instance
            playerAttack.fireCooldown = currentFireCooldown;  // sync lần đầu
        }
    }

    // public getter để PlayerAttack truy cập fireCooldown của instance
    public float FireCooldown => currentFireCooldown;

    private void OnMouseDown()
    {
        if (upgradeUI != null)
            upgradeUI.Show(this);
    }

    public void UpgradeDamage()
    {
        if (GoldManager.Instance == null) return;
        if (GoldManager.Instance.SpendGold(upgradeDamageCost))
            bulletDamage += damageIncrease;
    }

    public void UpgradeFireRate()
    {
        if (playerAttack == null || GoldManager.Instance == null) return;
        if (GoldManager.Instance.SpendGold(upgradeFireRateCost))
        {
            currentFireCooldown = Mathf.Max(0.1f, currentFireCooldown - fireRateDecrease);
            playerAttack.fireCooldown = currentFireCooldown;
            playerAttack.ResetFireTimer();
        }
    }

    public void ResetTowerStats()
    {
        bulletDamage = bulletDamageOriginal;
        currentFireCooldown = fireCooldownOriginal;
        if (playerAttack != null)
        {
            playerAttack.fireCooldown = currentFireCooldown;
            playerAttack.ResetFireTimer();
        }
    }
}
