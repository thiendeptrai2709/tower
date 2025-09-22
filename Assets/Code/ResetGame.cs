using UnityEngine;

public class ResetData : MonoBehaviour
{
    // Hàm này gán vào nút Reset trong Inspector
    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Đã xóa toàn bộ dữ liệu game!");
    }
}
