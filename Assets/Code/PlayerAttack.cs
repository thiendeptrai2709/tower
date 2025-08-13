using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 5f;          // Tầm đánh
    public GameObject bulletPrefab;         // Prefab bullet
    public Transform firePoint;              // Vị trí bắn đạn
    public float bulletSpeed = 10f;          // Tốc độ đạn
    public float fireCooldown = 1f;          // Thời gian giữa các lần bắn

    private Transform targetEnemy;           // Enemy gần nhất trong tầm
    private float fireTimer = 0f;

    void Update()
    {
        fireTimer -= Time.deltaTime;

        FindNearestEnemyInRange();

        if (targetEnemy != null)
        {
            FlipTowards(targetEnemy.position);  // <-- dùng lật trái/phải thay vì xoay

            if (fireTimer <= 0f)
            {
                ShootAt(targetEnemy.position);
                fireTimer = fireCooldown;
            }
        }
    }

    void FindNearestEnemyInRange()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));

        float minDist = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestEnemy = enemy.transform;
            }
        }

        targetEnemy = nearestEnemy;
    }

    void FlipTowards(Vector2 targetPos)
    {
        if (targetPos.x > transform.position.x)
        {
            // Enemy bên phải => mặt player hướng phải
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (targetPos.x < transform.position.x)
        {
            // Enemy bên trái => mặt player hướng trái
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void ShootAt(Vector2 targetPos)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 direction = (targetPos - (Vector2)firePoint.position).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;  // <-- sửa lại thành velocity

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
