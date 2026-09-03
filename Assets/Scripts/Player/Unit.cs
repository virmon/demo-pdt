using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static List<Unit> AllUnits = new List<Unit>();

    private int maxHp;

    public int hp;
    public float sizeX = 0f;
    public float sizeY = 0f;
    public float attackInterval = 0.5f;
    public float attackRange = 2f;
    public int attackPow = 1;
    public float moveSpeed = 0.5f;
    public Transform hpGauge;

    Enemy targetEnemy = null;
    GameManager gameManager;

    public enum UnitType
    {
        Unit00,
        Unit01,
        Unit02,
        Base
    }
    public UnitType unitType;

    private void OnEnable()
    {
        AllUnits.Add(this);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        maxHp = hp;
    }

    private void Update()
    {
        Move();
        SearchEnemy();
        Attack();
        Dead();
    }

    // Move
    private void Move()
    {
        if (gameManager.noActionFlag == true) return;
        if (targetEnemy != null) return;
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    // Acquire a target within the attack range
    private void SearchEnemy()
    {
        if (gameManager.noActionFlag == true) return;
        float minDistance = float.MaxValue;
        Enemy nearest = null;

        foreach (var enemy in Enemy.AllEnemies)
        {
            if (enemy == null) continue;
            Vector3 diff = transform.position - enemy.transform.position;
            float halfX = enemy.sizeX * 0.5f;
            float halfY = enemy.sizeY * 0.5f;
            float dx = Math.Abs(diff.x) - halfX;
            float dy = Math.Abs(diff.y) - halfY;
            float d = Mathf.Max(dx, 0f) + Mathf.Max(dy, 0);

            if (d <= attackRange && d < minDistance)
            {
                minDistance = d;
                nearest = enemy;
            }
        }
        targetEnemy = nearest;
    }

    private float attackTime = 0f;

    // Attack the acquired target
    private void Attack()
    {
        // stops all attack
        if (gameManager.noActionFlag == true) return;
        if (targetEnemy == null) return;
        attackTime += Time.deltaTime;

        if (attackTime >= attackInterval)
        {
            attackTime = 0;
            targetEnemy.TakeDMG(attackPow);
        }
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
            if (unitType == UnitType.Base)
            {
                gameManager.GameOver();
            }
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        AllUnits.Remove(this);
    }

}
