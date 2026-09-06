using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject boss;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        if (gameManager.bossSpawnFlag == true)
        {
            gameManager.bossSpawnFlag = false;
            Instantiate(boss, transform.position, Quaternion.identity);
        }
    }
}
