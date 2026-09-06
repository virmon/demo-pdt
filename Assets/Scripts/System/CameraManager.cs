using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public GameObject player;
    GameManager gameManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void LateUpdate()
    {
        if (gameManager.noActionFlag == true) return;
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, 0, transform.position.z);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}
