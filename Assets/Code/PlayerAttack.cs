using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireCooldown = 1f; // sẽ được override bởi PlayerUpgrade

    private float fireTimer = 0f;
    private Transform targetEnemy;
    private PlayerUpgrade tower;

    void Start()
    {
        // Lấy PlayerUpgrade trên instance
        tower = GetComponent<PlayerUpgrade>();

        // đảm bảo tower đã Awake xong, giá trị fireCooldown instance đã có
        if (tower != null)
            fireCooldown = tower.FireCooldown;
        else
            fireCooldown = Mathf.Max(0.1f, fireCooldown); // fallback

        fireTimer = fireCooldown; // delay lần bắn đầu
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        FindNearestEnemy();

        if (targetEnemy != null && fireTimer <= 0f)
        {
            ShootAt(targetEnemy.position);
            fireTimer = fireCooldown; // dùng fireCooldown hiện tại
        }
    }

    void FindNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = e.transform;
            }
        }

        targetEnemy = nearest;
    }

    void ShootAt(Vector2 targetPos)
    {
        if (bulletPrefab == null || tower == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector2 dir = (targetPos - (Vector2)firePoint.position).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * bulletSpeed;

        Bullet bScript = bullet.GetComponent<Bullet>();
        if (bScript != null)
            bScript.damage = tower.bulletDamage;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void ResetFireTimer()
    {
        fireTimer = fireCooldown;
    }
}
