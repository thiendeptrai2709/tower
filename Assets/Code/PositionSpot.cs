using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem; // input system mới

public class PositionSpot : MonoBehaviour
{
    public bool hasPlayer = false;
    public Vector3 spawnPosition;

    public UnityEvent<PositionSpot> onSpotClicked;

    private void Awake()
    {
        spawnPosition = transform.position;
    }

    // Hàm này sẽ do PlayerClickManager gọi khi nhấp chuột ở vị trí này
    public void OnClicked()
    {
        if (onSpotClicked != null)
            onSpotClicked.Invoke(this);
    }
}
