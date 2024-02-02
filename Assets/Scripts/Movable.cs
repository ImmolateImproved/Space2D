using UnityEngine;

public abstract class Movable : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Init(Vector2 position, Vector2 velocity, float rotationAngle = 0)
    {
        rb = GetComponent<Rigidbody2D>();
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, rotationAngle));
        rb.velocity = velocity;
    }
}