using UnityEngine;

public class Projectile : Movable
{
    [field: SerializeField] public DamageType DamageType { get; private set; }
    public Transform Parent { get; private set; }

    public void SetParent(Transform parent)
    {
        Parent = parent;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Parent == collision.transform) return;

        var enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(DamageType);
            Destroy(gameObject);
        }
    }
}