using UnityEngine;

public class Unit : MonoBehaviour
{
    public float moveSpeed = 0.5f;

    private void Update()
    {
        Move();
    }
    

    // Move
    private void Move()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
