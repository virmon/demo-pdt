using UnityEngine;
using DG.Tweening;

public class EnemyBossBullet : MonoBehaviour
{
    Transform playerPos;
    Player player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.GetComponent<Transform>();
        player = playerObj.GetComponent<Player>();

        float randomX = Random.Range(-3f, 3f);
        float randomY = Random.Range(-1f, 1f);

        transform.DOJump(new Vector3(playerPos.position.x + randomX, playerPos.position.y + randomY, 0), 2.5f, 1, 2f).SetLink(gameObject);
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDMG();
        }
    }
}
