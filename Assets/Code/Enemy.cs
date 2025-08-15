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

    // Hàm để tăng máu khi spawn
    public void SetupHP(int bonusHP)
    {
        maxHealth += bonusHP;
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
        GoldManager.Instance.AddGold(goldReward);
        Debug.Log("Enemy chết, báo WaveSpawner");
        WaveSpawner.EnemyKilled();
        Destroy(gameObject);
    }
}
