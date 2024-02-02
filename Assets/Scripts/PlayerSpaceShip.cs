using UnityEngine;

[System.Serializable]
public class ShipTypeMapping
{
    public DamageType damageType;
    public Projectile projectilePrefab;
    public Sprite sprite;
}

public class PlayerSpaceShip : MonoBehaviour
{
    private SpriteRenderer sr;
    private Shooting shooting;

    [SerializeField] private ShipTypeMapping[] shipTypes;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        shooting = GetComponent<Shooting>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting.Shoot(Vector2.up);
        }
    }

    private ShipTypeMapping GetShipType(DamageType damageType)
    {
        foreach (var shipType in shipTypes)
        {
            if (shipType.damageType == damageType)
            {
                return shipType;
            }
        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var typeChanger = collision.GetComponent<DamageTypeChanger>();

        if (typeChanger)
        {
            var shipType = GetShipType(typeChanger.DamageType);

            shooting.SetProjectile(shipType.projectilePrefab);
            sr.sprite = shipType.sprite;

            Destroy(collision.gameObject);
        }
    }
}