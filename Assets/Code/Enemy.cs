using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;     // Máu tối đa
    public int goldReward = 10;     // Vàng nhận được khi tiêu diệt
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Cộng vàng cho người chơi
        GoldManager.Instance.AddGold(goldReward);

        // Báo cho hệ thống spawn quái biết quái đã chết
        WaveSpawner.EnemyKilled();

        // Xóa enemy khỏi scene
        Destroy(gameObject);
    }
}
