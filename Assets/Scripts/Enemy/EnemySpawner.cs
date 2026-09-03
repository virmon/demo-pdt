using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 12f;
    public GameObject[] enemy;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        SpawnLoop();
    }

    private float loopTime = 0f;
    private float randomTime = 0f;

    private void SpawnLoop()
    {
        if (gameManager.noActionFlag == true) return;
        
        loopTime += Time.deltaTime;
        if (loopTime > spawnInterval + randomTime)
        {
            loopTime = 0f;
            randomTime = Random.Range(0, 3f);

            GameObject e = enemy[Random.Range(0, enemy.Length)];
            Instantiate(e, transform.position, Quaternion.identity);
        }
    }
}
