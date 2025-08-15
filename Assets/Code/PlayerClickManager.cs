using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerClickManager : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask positionSpotLayer;
    public LayerMask playerLayer; // thêm layer cho player

    public PlayerSpawner playerSpawner;
    public PlayerUpgradeUI upgradeUI; // tham chiếu UI nâng cấp

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            Vector2 worldPos2D = new Vector2(worldPos.x, worldPos.y);

            // Check click vào Player trước
            RaycastHit2D hitPlayer = Physics2D.Raycast(worldPos2D, Vector2.zero, 0f, playerLayer);
            if (hitPlayer.collider != null)
            {
                PlayerUpgrade upgrade = hitPlayer.collider.GetComponent<PlayerUpgrade>();
                if (upgrade != null)
                {
                    upgradeUI.Show(upgrade); // Mở panel nâng cấp
                    return;
                }
            }

            // Nếu không click player thì check PositionSpot
            RaycastHit2D hitSpot = Physics2D.Raycast(worldPos2D, Vector2.zero, 0f, positionSpotLayer);
            if (hitSpot.collider != null)
            {
                PositionSpot spot = hitSpot.collider.GetComponent<PositionSpot>();
                if (spot != null)
                {
                    spot.OnClicked();
                }
            }
        }
    }
}
