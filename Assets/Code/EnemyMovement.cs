using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    private Transform target;
    private int waypointIndex = 0;

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
            Destroy(gameObject); // Tới đích thì xoá
            return;
        }
        target = Waypoints.points[waypointIndex];
    }
}
