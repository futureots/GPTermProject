using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : Singleton<GameManager>
{
    public int point = 0;
    public Cake cake;

    //적 오브젝트 풀
    public ObjectPool<Enemy> enemyPool;
    public Transform pool;
    

    [ContextMenuItem("SetSpawnPoint","SetSpawnPointList")]
    public List<GameObject> SpawnPoint;
    public Enemy enemyPrefab;

    private void Start()
    {
        enemyPool = new ObjectPool<Enemy>(
            createFunc: SpawnEnemy,
            actionOnDestroy: DestroyEnemy,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
            );
        StartCoroutine(SpawnEnemyCoroutine());
    }

    // 적 생성
    Enemy SpawnEnemy()
    {
        var instance = Instantiate(enemyPrefab) as Enemy;
        instance.transform.SetParent(pool);
        instance.targetTr = cake.transform;
        return instance;
    }
    void DestroyEnemy(Enemy _enemy)
    {
        Destroy(_enemy.gameObject);
    }
    IEnumerator SpawnEnemyCoroutine()
    {
        var spawnPool = new List<GameObject>();
        spawnPool.AddRange(SpawnPoint);
        while (true)
        {
            yield return new WaitForSeconds(1);
            var enemy = enemyPool.Get();
            if (spawnPool.Count < 3)
            {
                spawnPool.AddRange(SpawnPoint);
            }
            var rand = Random.Range(0, spawnPool.Count);

            var point = spawnPool[rand];
            spawnPool.RemoveAt(rand);
           // Debug.Log("Spawn : " + SpawnPoint[rand].transform.position);
            enemy.agent.Warp(point.transform.position);
            enemy.Init();
            //Debug.Log("Enemy : " + enemy.transform.position);
        }
    }

    
    public void SetSpawnPointList()
    {
        SpawnPoint = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            SpawnPoint.Add(transform.GetChild(i).gameObject);
        }
    }
}
