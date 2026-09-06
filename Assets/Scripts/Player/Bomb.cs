using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDMG(10);

            if (collision.GetComponent<Enemy>().enemyType != Enemy.EnemyType.Base)
            {
                collision.transform.DOMoveX(2, 0.1f).SetRelative().SetLink(collision.gameObject);
            }
        }
    }
}
