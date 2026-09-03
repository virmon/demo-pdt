using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 8f;
    
    Vector3 move;

    private void Update()
    {
        Move();
    }

    // Move
    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        transform.Translate(move * speed * Time.deltaTime);

        Vector3 currentPos = transform.position;
        currentPos.x = Math.Clamp(currentPos.x, -10f, 10f);
        currentPos.y = Math.Clamp(currentPos.y, -4f, 4f);
        transform.position = currentPos;
    }
}
