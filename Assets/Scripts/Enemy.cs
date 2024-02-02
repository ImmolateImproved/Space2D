using System.Collections;
using UnityEngine;

public class Enemy : Movable
{
    private Shooting shooting;
    [SerializeField] private float shootingRate;
    [field: SerializeField] public DamageType ShipType { get; private set; }

    private void Awake()
    {
        shooting = GetComponent<Shooting>();

        if (shooting)
        {
            StartCoroutine(ShootingRoutine());
        }
    }

    private IEnumerator ShootingRoutine()
    {
        var wfs = new WaitForSeconds(shootingRate);

        yield return null;//need to wait 1 frame because the position is not initialized yet

        while (true)
        {
            shooting.Shoot(Vector2.down, 180);

            yield return wfs;
        }
    }

    public void TakeDamage(DamageType damageType)
    {
        if (damageType != ShipType) return;

        Destroy(gameObject);
    }
}