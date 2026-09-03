using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static List<Enemy> AllEnemies = new List<Enemy>();

    private int maxHp;

    public int hp;
    public float sizeX = 0f;
    public float sizeY = 0f;
    public float attackInterval = 0.5f;
    public float attackRange = 2f;
    public int attackPow = 1;
    public float moveSpeed = 0.5f;
    public Transform hpGauge;

    private void OnEnable()
    {
        AllEnemies.Add(this);
    }

    private void Start()
    {
        maxHp = hp;
    }

    private void Update()
    {
        Move();
        Dead();
    }


    // Move
    private void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // Unit takes damage
    public void TakeDMG(int num)
    {
        hp -= num;
        hpGauge.transform.localScale = new Vector3((float)hp / maxHp, 1, 1);
    }

    // Unit dies
    private void Dead()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        AllEnemies.Remove(this);
    }
}
