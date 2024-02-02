using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private DamageType shipType;
    [SerializeField] private ShipTypeMapping[] shipTypesMap;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        shooting = GetComponent<Shooting>();

        var shipProp = GetShipProperties(shipType);
        SetShipProperties(shipProp);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting.Shoot(Vector2.up);
        }
    }

    private void SetShipProperties(ShipTypeMapping shipTypeMap)
    {
        shooting.SetProjectile(shipTypeMap.projectilePrefab);
        sr.sprite = shipTypeMap.sprite;
        shipType = shipTypeMap.damageType;
    }

    private ShipTypeMapping GetShipProperties(DamageType damageType)
    {
        foreach (var shipType in shipTypesMap)
        {
            if (shipType.damageType == damageType)
            {
                return shipType;
            }
        }

        return null;
    }

    private ShipTypeMapping GetRandomShipProperties()
    {
        var randIndex = Random.Range(0, shipTypesMap.Length);

        return shipTypesMap[randIndex];
    }

    private void OnTypeChangerHit(DamageTypeChanger typeChanger)
    {
        var shipType = GetShipProperties(typeChanger.DamageType);

        SetShipProperties(shipType);

        Destroy(typeChanger.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //var typeChanger = collision.GetComponent<DamageTypeChanger>();

        //if (typeChanger)
        //{
        //    OnTypeChangerHit(typeChanger);

        //    return;
        //}

        var enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            if (enemy.ShipType != shipType)
            {
                LevelLoader.RestartLevel();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (enemy)
        {
            var shipProp = GetRandomShipProperties();
            SetShipProperties(shipProp);
        }
    }
}