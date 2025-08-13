using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject panel; // panel UI chọn player
    public Button[] playerButtons; // 3 nút chọn player
    public GameObject[] playerPrefabs; // 3 prefab player
    public int[] playerCosts; // Giá vàng cho từng player

    private PositionSpot currentSpot;

    private void Start()
    {
        panel.SetActive(false);

        for (int i = 0; i < playerButtons.Length; i++)
        {
            int index = i;
            playerButtons[i].onClick.AddListener(() => OnPlayerSelected(index));
        }
    }

    public void ShowPanel(PositionSpot spot)
    {
        if (spot.hasPlayer)
        {
            Debug.Log("Vị trí này đã có player rồi");
            return;
        }
        currentSpot = spot;
        panel.SetActive(true);
    }

    private void OnPlayerSelected(int index)
    {
        if (currentSpot == null)
            return;

        // Kiểm tra vàng trước khi spawn
        int cost = playerCosts[index];
        if (!GoldManager.Instance.SpendGold(cost))
        {
            Debug.Log("Không đủ vàng");
            panel.SetActive(false);
            return;
        }

        Instantiate(playerPrefabs[index], currentSpot.spawnPosition, Quaternion.identity);
        currentSpot.hasPlayer = true;
        panel.SetActive(false);
    }
}
