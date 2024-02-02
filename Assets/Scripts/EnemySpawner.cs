using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RoadManager roadManager;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private DamageTypeChanger[] damageTypeChangers;

    [SerializeField] private float randomPositionOffset;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float enemyStartYPos;
    [SerializeField] private float projectileStartYPos;
    [SerializeField] private float enemyMoveSpeed;

    private Enemy[] currentEnemyLine;

    private void Awake()
    {
        currentEnemyLine = new Enemy[enemyPrefabs.Length];
        enemyPrefabs.CopyTo(currentEnemyLine, 0);

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wfs = new WaitForSeconds(spawnInterval);

        while (true)
        {
            ReshuffleEnemyLine();

            for (int i = 0; i < currentEnemyLine.Length; i++)
            {
                SpawnEnemy(i);
            }

            SpawnDamageTypeChanger();

            yield return wfs;
        }
    }

    private void SpawnEnemy(int column)
    {
        var enemy = Instantiate(currentEnemyLine[column]);

        var randPosOffset = Random.Range(0, randomPositionOffset);

        var enemyPos = new Vector2
        {
            x = (-roadManager.offset * 2) + (roadManager.offset * (column + 1)),
            y = enemyStartYPos + randPosOffset
        };

        enemy.Init(enemyPos, Vector2.down * enemyMoveSpeed);
    }

    private void ReshuffleEnemyLine()
    {
        for (int i = currentEnemyLine.Length - 1; i > 0; i--)
        {
            var randEnemyIndex = Random.Range(0, i + 1);

            var temp = currentEnemyLine[i];
            currentEnemyLine[i] = currentEnemyLine[randEnemyIndex];
            currentEnemyLine[randEnemyIndex] = temp;
        }
    }

    private void SpawnDamageTypeChanger()
    {
        var enemyType = currentEnemyLine[0].ShipType;
        var typeChanger = GetTypeChangerByDamageType(enemyType);
        var damageTypeChanger = Instantiate(typeChanger);

        var randomRoad = Random.Range(0, roadManager.roadCount);

        var position = new Vector2
        {
            x = (-roadManager.offset * 2) + (roadManager.offset * (randomRoad + 1)),
            y = projectileStartYPos
        };

        damageTypeChanger.Init(position, Vector2.down * enemyMoveSpeed);
    }

    private DamageTypeChanger GetTypeChangerByDamageType(DamageType damageType)
    {
        foreach (var item in damageTypeChangers)
        {
            if (item.DamageType == damageType)
            {
                return item;
            }
        }

        return null;
    }
}