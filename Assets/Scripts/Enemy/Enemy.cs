using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.5f;

    private void Update()
    {
        Move();
    }
    

    // Move
    private void Move()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
