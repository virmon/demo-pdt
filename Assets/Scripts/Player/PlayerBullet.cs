using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerBullet : MonoBehaviour
{
    private float speed = 12f;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite.DOFade(1, 0.3f).SetLink(gameObject);
        Destroy(gameObject, 0.6f);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDMG(1);
            Destroy(gameObject);
        }
    }
}
