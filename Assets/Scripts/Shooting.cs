using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed;

    public void Shoot(Vector2 direction, float rotationAngle = 0)
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.Init(shootPoint.position, direction * projectileSpeed, rotationAngle);
        projectile.SetParent(transform);
    }

    public void SetProjectile(Projectile projectile)
    {
        projectilePrefab = projectile;
    }
}