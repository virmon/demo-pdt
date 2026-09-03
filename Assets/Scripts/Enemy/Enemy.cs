using System;
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

    Unit targetUnit = null;
    GameManager gameManager;

    private void OnEnable()
    {
        AllEnemies.Add(this);
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        maxHp = hp;
    }

    private void Update()
    {
        Move();
        SearchUnit();
        Attack();
        Dead();
    }


    // Move
    private void Move()
    {
        if (gameManager.noActionFlag == true) return;
        if (targetUnit != null) return;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void SearchUnit()
    {
        if (gameManager.noActionFlag == true) return;
        float minDistance = float.MaxValue;
        Unit nearest = null;

        foreach (var unit in Unit.AllUnits)
        {
            if (unit == null) continue;
            Vector3 diff = transform.position - unit.transform.position;
            float halfX = unit.sizeX * 0.5f;
            float halfY = unit.sizeY * 0.5f;
            float dx = Math.Abs(diff.x) - halfX;
            float dy = Math.Abs(diff.y) - halfY;
            float d = Mathf.Max(dx, 0f) + Mathf.Max(dy, 0);

            if (d <= attackRange && d < minDistance)
            {
                minDistance = d;
                nearest = unit;
            }
        }
        targetUnit = nearest;
    }

    private float attackTime = 0f;

    // Attack the acquired target
    private void Attack()
    {
        if (gameManager.noActionFlag == true) return;
        if (targetUnit == null) return;
        attackTime += Time.deltaTime;

        if (attackTime >= attackInterval)
        {
            attackTime = 0;
            targetUnit.TakeDMG(attackPow);
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
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        AllEnemies.Remove(this);
    }
}
