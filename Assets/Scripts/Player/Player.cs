using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject[] unit;
    public GameObject[] spawnPoint;

    public float speed = 8f;

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
}
