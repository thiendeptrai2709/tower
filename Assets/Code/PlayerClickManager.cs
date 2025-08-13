using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerClickManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask positionSpotLayer;

    public PlayerSpawner playerSpawner;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Tránh click vào UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            RaycastHit2D hit = Physics2D.Raycast(worldPos2D, Vector2.zero, 0f, positionSpotLayer);
            if (hit.collider != null)
            {
                PositionSpot spot = hit.collider.GetComponent<PositionSpot>();
                if (spot != null)
                {
                    spot.OnClicked();
                }
            }
        }
    }
}
