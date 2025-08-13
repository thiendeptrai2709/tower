using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Sát thương, chỉnh trong prefab

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject); // Hủy viên đạn
        }
    }
}
