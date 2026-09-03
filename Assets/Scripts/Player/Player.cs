using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool downFlag = false;

    public GameObject[] unit;
    public GameObject[] spawnPoint;
    public Collider2D myCol;
    public GameObject[] hpIcon;
    public Animator animator;

    public float speed = 8f;
    public int hp = 3;

    Vector3 move;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        UnitButtonPushing();
        Move();
        ReturnMyCol();
        Down();
        ReturnDown();
    }

    // Move
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        if (gameManager.noActionFlag == true) return;

        transform.Translate(move * speed * Time.deltaTime);

        Vector3 currentPos = transform.position;
        currentPos.x = Math.Clamp(currentPos.x, -10f, 10f);
        currentPos.y = Math.Clamp(currentPos.y, -4f, 4f);
        transform.position = currentPos;
    }

    // spawn unit on button input
    [NonSerialized] public bool isUnitPushing = false;
    private float unitPushingTime = 0f;
    public void OnSpawn(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (gameManager.noActionFlag == true) return;
            if (downFlag) return;

            isUnitPushing = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isUnitPushing = false;
        }
    }

    private void UnitButtonPushing()
    {
        if (isUnitPushing)
        {
            unitPushingTime += Time.deltaTime;
            if (unitPushingTime > 1f)
            {
                unitPushingTime = 0;
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    float dy = Mathf.Abs(transform.position.y - spawnPoint[i].transform.position.y);
                    if (dy < 0.8f)
                    {
                        Instantiate(unit[0], spawnPoint[i].transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    // Take damage
    public void TakeDMG()
    {
        if (gameManager.noActionFlag == true) return;
        hp--;
        myCol.enabled = false; // disable hitbox
        animator.SetBool("dmg", true);
        if (hp == 2) hpIcon[0].SetActive(false);
        else if (hp == 1) hpIcon[1].SetActive(false);
        else if (hp == 0) hpIcon[2].SetActive(false);
    }

    // Recover from damage
    private float returnMyColTime = 0f;
    private void ReturnMyCol()
    {
        if (downFlag == true) return;

        if (myCol.enabled == false)
        {
            returnMyColTime += Time.deltaTime;
            if (returnMyColTime > 2f) // invincibility window
            {
                returnMyColTime = 0f;
                myCol.enabled = true;
                animator.SetBool("dmg", false);
            }
        }
    }

    // Become downed
    private void Down()
    {
        if (gameManager.noActionFlag == true) return;
        if (hp <= 0 && downFlag == false)
        {
            downFlag = true;
            animator.SetBool("down", true);
            animator.SetBool("dmg", false);
        }

    }

    // Recover from being downed
    private float returnDownTime = 0f;
    private void ReturnDown()
    {
        if (downFlag)
        {
            returnDownTime += Time.deltaTime;
            if (returnDownTime > 5f)
            {
                returnDownTime = 0f;
                hp = 3;
                hpIcon[0].SetActive(true);
                hpIcon[1].SetActive(true);
                hpIcon[2].SetActive(true);
                downFlag = false;
                animator.SetBool("down", false);
            }
        }
    }
}
