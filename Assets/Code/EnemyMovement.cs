using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    private int waypointIndex = 0;

    public int damageToBase = 1;
    void Start()
    {
        // Lấy waypoint đầu tiên
        target = Waypoints.points[0];
    }

    void Update()
    {
        // Tính hướng đi
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Đổi hướng khi gần tới waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex >= Waypoints.points.Length)
        {
            ReachBase(); // Khi đến waypoint cuối cùng
            return;
        }
        target = Waypoints.points[waypointIndex];
    }

    void ReachBase()
    {
        BaseHealth baseHealth = FindFirstObjectByType<BaseHealth>();
        if (baseHealth != null)
        {
            baseHealth.TakeDamage(damageToBase);
        }
        Destroy(gameObject); // Enemy biến mất sau khi tấn công base
    }
}
